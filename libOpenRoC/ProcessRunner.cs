namespace liboroc
{
    using System;
    using System.IO;
    using System.Timers;
    using System.Diagnostics;
    using System.ComponentModel;
    using Signal = System.Threading.ManualResetEventSlim;

    public class ProcessRunner : IDisposable, INotifyPropertyChanged
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

        public Status State
        {
            get { return currentState; }
            set
            {
                if (value == State)
                    return;

                options.InitialStateEnumValue = value;
                previousState = currentState;
                currentState = value;
                ResetTimers();

                NotifyPropertyChanged("State");
            }
        }

        public string StateString
        {
            get { return string.Format("{0} for {1:hh\\:mm\\:ss}", State, Stopwatch.Elapsed); }
        }

        public Process Process { get; private set; }

        public Stopwatch Stopwatch { get; private set; }

        public bool HasWindow { get; private set; } = false;

        public string ProcessPath { get { return options.Path; } }

        public ProcessOptions ProcessOptions
        {
            get { return options.Clone() as ProcessOptions; }
            set { SwapOptions(value.Clone() as ProcessOptions); }
        }

        public ProcessRunner(ProcessOptions opts)
        {
            Stopwatch = new Stopwatch();
            currentState = opts.InitialStateEnumValue;
            previousState = Status.Invalid;
            resetTimer = new Signal(true);
            startSignal = new Signal(false);
            checkSignal = new Signal(false);
            options = opts.Clone() as ProcessOptions;
            gracePeriodTimer = new Timer { AutoReset = false };
            doubleCheckTimer = new Timer { AutoReset = false };
            gracePeriodTimer.Elapsed += OnGracePeriodTimeElapsed;
            doubleCheckTimer.Elapsed += OnDoubleCheckTimeElapsed;
            SetupOptions();
        }

        private void SwapOptions(ProcessOptions opts)
        {
            Stop();
            options = opts;
            State = opts.InitialStateEnumValue;

            SetupOptions();
            NotifyPropertyChanged("ProcessOptions");
        }

        private void SetupOptions()
        {
            if (currentState == Status.Invalid)
            {
                currentState = Status.Stopped;

                if (previousState == Status.Stopped)
                    previousState = Status.Invalid;
            }

            if (options.InitialStateEnumValue == Status.Running)
                startSignal.Set();
        }

        public void RestoreState()
        {
            if (previousState == Status.Invalid)
                State = Status.Stopped;
            else State = previousState;
        }

        public void Start()
        {
            if (Process != null)
            {
                Stop();
            }

            if (options.PreLaunchScriptEnabled)
            {
                ProcessHelper.ExecuteScript(options.PreLaunchScriptPath);
            }

            ProcessStartInfo sinfo = new ProcessStartInfo
            {
                WorkingDirectory = options.WorkingDirectory,
                FileName = options.Path,
                UseShellExecute = false
            };

            if (options.CommandLineEnabled &&
                !string.IsNullOrWhiteSpace(options.CommandLine))
            {
                sinfo.Arguments = options.CommandLine;
            }

            if (options.EnvironmentVariablesEnabled &&
                options.EnvironmentVariablesDictionary.Count > 0)
            {
                foreach (var pair in options.EnvironmentVariablesDictionary)
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

            try
            {
                Process.WaitForInputIdle();
                HasWindow = true;
            }
            catch (Exception)
            {
                HasWindow = false;
            }

            if (!HasWindow || Process.Responding)
            {
                State = Status.Invalid;
                State = Status.Running;
            }
            else
            {
                Stop();
            }
        }

        public void Stop()
        {
            if (Process == null)
            {
                if (!IsDisposed && State != Status.Stopped)
                {
                    State = Status.Stopped;
                }

                return;
            }

            HasWindow = false;
            gracePeriodTimer.Stop();
            doubleCheckTimer.Stop();

            Process.Refresh();

            Process.EnableRaisingEvents = false;
            Process.Disposed -= OnProcessStopped;
            Process.Exited -= OnProcessStopped;

            int pid = 0;
            var prc = string.Empty;

            if (!Process.HasExited)
            {
                pid = Process.Id;
                prc = Process.ProcessName;

                Process.CloseMainWindow();
                Process.WaitForExit(1000);

                if (!Process.HasExited)
                {
                    ProcessHelper.KillProcess(Process.ProcessName);
                }
            }

            if (options.AggressiveCleanupEnabled)
            {
                if (options.AggressiveCleanupByName)
                {
                    ProcessHelper.KillProcess(Path.GetFileName(ProcessPath));

                    if (!string.IsNullOrWhiteSpace(prc))
                        ProcessHelper.KillProcess(prc);
                }

                if (options.AggressiveCleanupByPID && pid != 0)
                {
                    ProcessHelper.KillProcess(pid);
                }
            }

            Process.Dispose();
            Process = null;

            if (!IsDisposed)
            {
                State = Status.Invalid;
                State = Status.Stopped;
            }

            if (options.PostCrashScriptEnabled)
            {
                ProcessHelper.ExecuteScript(options.PostCrashScriptPath);
            }
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
                if (startSignal.IsSet)
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

                    State = previousStateSnapshot;
                    State = currentStateSnapshot;
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

            if (currentState != Status.Disabled && HasWindow && Process != null)
            {
                Process.Refresh();

                if (options.CrashedIfUnresponsive && !Process.Responding)
                {
                    if (options.DoubleCheckEnabled)
                    {
                        if (checkSignal.IsSet)
                        {
                            if (!Process.Responding)
                            {
                                startSignal.Set();
                            }
                            else
                            {
                                startSignal.Reset();
                            }

                            checkSignal.Reset();
                        }
                        else if (!doubleCheckTimer.Enabled)
                        {
                            doubleCheckTimer.Interval = TimeSpan.FromSeconds(options.DoubleCheckDuration).TotalMilliseconds;
                            doubleCheckTimer.Start();
                        }
                    }
                    else
                    {
                        startSignal.Set();
                    }

                    if (startSignal.IsSet)
                    {
                        Stop();
                    }
                }

                if (options.AlwaysOnTopEnabled && Process != null && Process.Responding)
                {
                    BringToFront(true);
                }
            }
        }

        public void BringToFront(bool aggressive = false)
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

                if (aggressive)
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
            get { return (Process == null) && (startSignal.IsSet || (State == Status.Running && options.CrashedIfNotRunning && !gracePeriodTimer.Enabled)); }
        }

        #region Event callbacks

        private void OnProcessStopped(object sender, EventArgs e)
        {
            Stop();

            if (options.GracePeriodEnabled)
            {
                if (!gracePeriodTimer.Enabled)
                {
                    gracePeriodTimer.Interval = TimeSpan.FromSeconds(options.GracePeriodDuration).TotalMilliseconds;
                    gracePeriodTimer.Start();
                }
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

        #region INotifyPropertyChanged support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDisposable Support

        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

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
                startSignal = null;
                checkSignal = null;
                resetTimer = null;
                Process = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
