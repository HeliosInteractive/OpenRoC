namespace oroc
{
    using System;
    using System.IO;
    using System.Timers;
    using System.Diagnostics;
    using Signal = System.Threading.ManualResetEventSlim;

    public class ProcessRunner : IDisposable
    {
        bool isDisabled = false;
        Status currentState = Status.Stopped;
        Status pendingState = Status.Disabled;
        Signal startSignal = new Signal(false);
        Signal checkSignal = new Signal(false);
        Signal resetTimer = new Signal(false);
        Timer gracePeriodTimer = new Timer();
        Timer doubleCheckTimer = new Timer();

        public enum Status
        {
            Stopped,
            Running,
            Disabled
        }

        public enum FocusMode
        {
            Normal,
            Aggressive
        }

        public Status State
        {
            get
            {
                if (isDisabled)
                    return Status.Disabled;
                else
                    return currentState;
            }

            private set
            {
                if (value == Status.Disabled)
                {
                    IsDisabled = true;
                }
                else if (value != currentState)
                {
                    currentState = value;
                    resetTimer.Set();
                }
            }
        }

        public bool IsDisabled
        {
            get
            {
                return isDisabled;
            }

            set
            {
                if (IsDisabled == value)
                    return;

                if (value)
                {
                    isDisabled = true;
                    resetTimer.Set();

                    gracePeriodTimer.Stop();
                    doubleCheckTimer.Stop();
                }
                else
                {
                    isDisabled = false;
                }
            }
        }

        public Process Process { get; private set; }
        public Stopwatch Stopwatch { get; private set; }
        public bool HasWindow { get; private set; } = false;
        public ProcessOptions ProcessOptions { get; private set; }

        public ProcessRunner(ProcessOptions opts)
        {
            ProcessOptions = opts;
            State = Status.Stopped;
            Stopwatch = new Stopwatch();

            gracePeriodTimer.AutoReset = false;
            gracePeriodTimer.Elapsed += GracePeriodTimeElapsed;

            doubleCheckTimer.AutoReset = false;
            doubleCheckTimer.Elapsed += DoubleCheckTimeElapsed;
        }

        public void SwapOptions(ProcessOptions opts)
        {
            if (ReferenceEquals(ProcessOptions, opts))
                return;

            Status before_swap = State;

            if (State == Status.Running)
                Stop();

            ProcessOptions = opts;

            if (before_swap == Status.Running)
                Start();
        }

        public void Start()
        {
            if (State == Status.Running)
                Stop();

            if (ProcessOptions.PreLaunchScriptEnabled)
                ProcessHelper.ExecuteScript(ProcessOptions.PreLaunchScriptPath);

            ProcessStartInfo sinfo = new ProcessStartInfo
            {
                WorkingDirectory = ProcessOptions.WorkingDirectory,
                FileName = ProcessOptions.Path,
                UseShellExecute = false
            };

            if (ProcessOptions.CommandLineEnabled &&
                !string.IsNullOrWhiteSpace(ProcessOptions.CommandLine))
            {
                sinfo.Arguments = ProcessOptions.CommandLine;
            }

            if (ProcessOptions.EnvironmentVariablesEnabled &&
                ProcessOptions.EnvironmentVariablesDictionary.Count > 0)
            {
                foreach (var pair in ProcessOptions.EnvironmentVariablesDictionary)
                    sinfo.EnvironmentVariables.Add(pair.Key, pair.Value);
            }

            Process = new Process
            {
                StartInfo = sinfo,
                EnableRaisingEvents = true
            };

            Process.Disposed += OnProcessStopped;
            Process.Exited += OnProcessStopped;

            Process.Start();
            Process.Refresh();

            try { Process.WaitForInputIdle(); HasWindow = true; }
            catch (Exception) { HasWindow = false; }

            if (!HasWindow || Process.Responding)
            {
                State = Status.Running;
                IsDisabled = false;
            }
            else Stop();
        }

        public void Stop()
        {
            if (Process == null)
                return;

            HasWindow = false;
            gracePeriodTimer.Stop();
            doubleCheckTimer.Stop();

            Process.Refresh();

            Process.EnableRaisingEvents = false;
            Process.Disposed -= OnProcessStopped;
            Process.Exited -= OnProcessStopped;

            if (Process.HasExited)
            {
                if (State == Status.Running)
                    State = Status.Stopped;
            }
            else
            {
                Process.CloseMainWindow();
                Process.WaitForExit(1000);

                if (!Process.HasExited)
                    Process.Kill();
            }

            if (ProcessOptions.AggressiveCleanupEnabled)
            {
                if (ProcessOptions.AggressiveCleanupByName)
                    ProcessHelper.ExecuteScript("taskkill", string.Format("/F /T /IM \"{0}\"", Path.GetFileName(ProcessOptions.Path)), true);

                if (ProcessOptions.AggressiveCleanupByPID)
                {
                    long pid = 0;
                    try { pid = Process.Id; }
                    catch(Exception) { pid = 0; }

                    if (pid > 0)
                        ProcessHelper.ExecuteScript("taskkill", string.Format("/F /T /PID {0}", pid), true);
                }
            }

            Process.Dispose();
            Process = null;

            State = Status.Stopped;

            if (ProcessOptions.PostCrashScriptEnabled)
                ProcessHelper.ExecuteScript(ProcessOptions.PostCrashScriptPath);
        }

        public void Monitor()
        {
            if (resetTimer.IsSet)
            {
                Stopwatch.Restart();
                resetTimer.Reset();
            }

            if (IsDisabled && currentState == Status.Running)
            {
                Debug.Assert(currentState != Status.Disabled);
                Debug.Assert(pendingState == Status.Disabled);

                pendingState = currentState;
                Stop();
            }
            else if (!IsDisabled)
            {
                if (pendingState != Status.Disabled)
                {
                    Debug.Assert(currentState == Status.Stopped);
                    Debug.Assert(pendingState != Status.Stopped);

                    pendingState = Status.Disabled;
                    Start();
                }

                if (ShouldStart)
                {
                    Debug.Assert(State == Status.Stopped);
                    Debug.Assert(Process == null);

                    startSignal.Reset();
                    Start();
                }

                if (HasWindow)
                {
                    Debug.Assert(State == Status.Running);
                    Debug.Assert(Process != null);

                    Process.Refresh();

                    if (ProcessOptions.CrashedIfUnresponsive && !Process.Responding)
                    {
                        if (ProcessOptions.DoubleCheckEnabled)
                        {
                            if (checkSignal.IsSet)
                            {
                                if (!Process.Responding)
                                    startSignal.Set();
                                else
                                    startSignal.Reset();

                                checkSignal.Reset();
                            }
                            else if (!doubleCheckTimer.Enabled)
                            {
                                doubleCheckTimer.Interval = TimeSpan.FromSeconds(ProcessOptions.DoubleCheckDuration).TotalMilliseconds;
                                doubleCheckTimer.Start();
                            }
                        }
                        else
                        {
                            startSignal.Set();
                        }

                        if (startSignal.IsSet && currentState == Status.Stopped)
                            Stop();
                    }
                    else if (ProcessOptions.AlwaysOnTopEnabled)
                    {
                        BringToFront(FocusMode.Aggressive);
                    }
                }
            }
        }

        public void BringToFront(FocusMode mode)
        {
            if (Process == null || !HasWindow)
                return;

            Process.Refresh();

            if (Process.HasExited || !Process.Responding)
                return;

            if (NativeMethods.GetForegroundWindow() != Process.MainWindowHandle)
            {
                NativeMethods.SwitchToThisWindow(Process.MainWindowHandle, true);
                NativeMethods.SetForegroundWindow(Process.MainWindowHandle);

                if (mode == FocusMode.Aggressive)
                {
                    NativeMethods.SetWindowPos(
                        Process.MainWindowHandle,
                        NativeMethods.HWND_TOPMOST,
                        0, 0, 0, 0,
                        NativeMethods.SetWindowPosFlags.SWP_NOSIZE |
                        NativeMethods.SetWindowPosFlags.SWP_NOMOVE |
                        NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
                }
            }
        }

        private bool ShouldStart
        {
            get { return (startSignal.IsSet || (Process == null && ProcessOptions.CrashedIfNotRunning && !gracePeriodTimer.Enabled)); }
        }

        #region Event callbacks

        private void OnProcessStopped(object sender, EventArgs e)
        {
            Stop();

            if (ProcessOptions.GracePeriodEnabled)
            {
                if (!gracePeriodTimer.Enabled)
                {
                    gracePeriodTimer.Interval = TimeSpan.FromSeconds(ProcessOptions.GracePeriodDuration).TotalMilliseconds;
                    gracePeriodTimer.Start();
                }
            }
            else
            {
                startSignal.Set();
            }
        }

        private void DoubleCheckTimeElapsed(object sender, ElapsedEventArgs e)
        {
            checkSignal.Set();
        }

        private void GracePeriodTimeElapsed(object sender, ElapsedEventArgs e)
        {
            startSignal.Set();
        }

        #endregion

        #region IDisposable Support
        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    if (gracePeriodTimer != null)
                        gracePeriodTimer.Dispose();

                    if (doubleCheckTimer != null)
                        doubleCheckTimer.Dispose();

                    if (startSignal != null)
                        startSignal.Dispose();

                    if (checkSignal != null)
                        checkSignal.Dispose();

                    if (resetTimer != null)
                        resetTimer.Dispose();

                    Stop();
                }

                gracePeriodTimer = null;
                doubleCheckTimer = null;
                ProcessOptions = null;
                startSignal = null;
                checkSignal = null;
                resetTimer = null;
                Process = null;

                IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
