namespace oroc
{
    using System;
    using System.IO;
    using System.Timers;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using Signal = System.Threading.ManualResetEventSlim;

    public class ProcessRunner : IDisposable
    {
        Status currentState;
        Status previousState;
        Signal startSignal;
        Signal checkSignal;
        Signal resetTimer;
        Timer gracePeriodTimer;
        Timer doubleCheckTimer;
        ProcessOptions options;

        public enum Status
        {
            Invalid,
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
            get { return currentState; }
            set
            {
                if (value == State)
                    return;

                previousState = currentState;
                currentState = value;
                ResetTimers();
            }
        }

        [XmlIgnore]
        public Process Process { get; private set; }

        [XmlIgnore]
        public Stopwatch Stopwatch { get; private set; }

        [XmlIgnore]
        public bool HasWindow { get; private set; } = false;

        public ProcessOptions ProcessOptions
        {
            get { return options; }
            set { SwapOptions(value); }
        }

        internal ProcessRunner()
        {
            Stopwatch = new Stopwatch();
            currentState = Status.Stopped;
            previousState = Status.Invalid;
            resetTimer = new Signal(false);
            startSignal = new Signal(false);
            checkSignal = new Signal(false);
            gracePeriodTimer = new Timer { AutoReset = false };
            doubleCheckTimer = new Timer { AutoReset = false };
            gracePeriodTimer.Elapsed += OnGracePeriodTimeElapsed;
            doubleCheckTimer.Elapsed += OnDoubleCheckTimeElapsed;
        }

        public ProcessRunner(ProcessOptions opts) : this()
        {
            options = opts.Clone() as ProcessOptions;
        }

        public void SwapOptions(ProcessOptions opts)
        {
            if (ReferenceEquals(ProcessOptions, opts))
                return;

            Status before_swap = State;

            if (State == Status.Running)
                Stop();

            options = opts;

            if (before_swap == Status.Running)
                Start();
        }

        public void Start()
        {
            if (Process != null)
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
                previousState = Status.Invalid;
                currentState = Status.Running;
                ResetTimers();
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

            if (!Process.HasExited)
            {
                Process.CloseMainWindow();
                Process.WaitForExit(1000);

                if (!Process.HasExited)
                    Process.Kill();
            }

            if (ProcessOptions.AggressiveCleanupEnabled)
            {
                if (ProcessOptions.AggressiveCleanupByName)
                    ProcessHelper.TaskKill(Path.GetFileName(ProcessOptions.Path));

                if (ProcessOptions.AggressiveCleanupByPID)
                    ProcessHelper.TaskKill(Process);
            }

            Process.Dispose();
            Process = null;

            previousState = Status.Invalid;
            currentState = Status.Stopped;
            ResetTimers();

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

            if (ShouldStart)
            {
                startSignal.Reset();
                Start();
            }

            if (previousState == Status.Running && Process != null)
            {
                if (currentState != Status.Running)
                {
                    Status previousStateSnapshot = previousState;
                    Status currentStateSnapshot = currentState;

                    Stop();

                    previousState = previousStateSnapshot;
                    currentState = currentStateSnapshot;
                }
            }
            else
            if (previousState == Status.Stopped && Process == null)
            {
                if (currentState == Status.Running)
                    Start();
            }
            else
            if (previousState == Status.Disabled && Process == null)
            {
                if (currentState == Status.Running)
                    Start();
            }

            if (currentState != Status.Disabled && HasWindow)
            {
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

        private void ResetTimers()
        {
            resetTimer.Set();
            gracePeriodTimer.Stop();
            doubleCheckTimer.Stop();
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

        private void OnDoubleCheckTimeElapsed(object sender, ElapsedEventArgs e)
        {
            checkSignal.Set();
        }

        private void OnGracePeriodTimeElapsed(object sender, ElapsedEventArgs e)
        {
            startSignal.Set();
        }

        #endregion

        #region IDisposable Support

        [XmlIgnore]
        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    ResetTimers();
                    Stop();

                    if (gracePeriodTimer != null)
                        gracePeriodTimer.Dispose();

                    if (doubleCheckTimer != null)
                        doubleCheckTimer.Dispose();

                    if (resetTimer != null)
                        resetTimer.Dispose();

                    if (startSignal != null)
                        startSignal.Dispose();

                    if (checkSignal != null)
                        checkSignal.Dispose();
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
