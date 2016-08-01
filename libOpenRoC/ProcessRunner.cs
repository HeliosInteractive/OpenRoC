namespace liboroc
{
    using System;
    using System.Timers;
    using System.Diagnostics;
    using System.ComponentModel;
    using Signal = System.Threading.ManualResetEventSlim;

    public class ProcessRunner : IDisposable, INotifyPropertyChanged
    {
        Status currentState;
        Status previousState;
        Signal crashSignal;
        Signal startSignal;
        Signal checkSignal;
        Signal resetTimer;
        Timer gracePeriodTimer;
        Timer doubleCheckTimer;
        ProcessOptions options;

        public Action StateChanged;
        public Action OptionsChanged;
        public Action ProcessChanged;
        public Action ProcessCrashed;

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

                NotifyPropertyChanged(nameof(State));
            }
        }

        public Process Process { get; private set; }

        public Stopwatch Stopwatch { get; private set; }

        public bool HasWindow { get; private set; } = false;

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
            crashSignal = new Signal(false);
            options = opts.Clone() as ProcessOptions;
            gracePeriodTimer = new Timer { AutoReset = false, Enabled = false };
            doubleCheckTimer = new Timer { AutoReset = false, Enabled = false };
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

            NotifyPropertyChanged(nameof(ProcessOptions));
        }

        private void SetupOptions()
        {
            if (currentState == Status.Invalid)
            {
                currentState = Status.Stopped;
            }

            if (options.InitialStateEnumValue == Status.Running)
                startSignal.Set();

            if (options.GracePeriodEnabled)
                gracePeriodTimer.Interval = TimeSpan.FromSeconds(options.GracePeriodDuration).TotalMilliseconds;

            if (options.DoubleCheckEnabled)
                doubleCheckTimer.Interval = TimeSpan.FromSeconds(options.DoubleCheckDuration).TotalMilliseconds;

            ResetTimers();
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

                NotifyPropertyChanged(nameof(Process));
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

            int process_id = 0;
            var process_exit_timeout = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;

            if (!Process.HasExited)
            {
                process_id = Process.Id;

                Process.CloseMainWindow();
                Process.WaitForExit(process_exit_timeout);

                if (options.AggressiveCleanupEnabled)
                {
                    ProcessHelper.Shutdown(process_id);
                }

                if (!Process.HasExited)
                {
                    Process.Kill();
                    Process.WaitForExit(process_exit_timeout);
                }
            }

            Process.Dispose();
            Process = null;

            NotifyPropertyChanged(nameof(Process));

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
            if (crashSignal.IsSet)
            {
                ProcessCrashed?.Invoke();

                Stop();

                if (options.GracePeriodEnabled)
                {
                    if (!gracePeriodTimer.Enabled)
                    {
                        gracePeriodTimer.Start();
                    }
                }
                else
                {
                    startSignal.Set();
                }

                crashSignal.Reset();
            }

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

                            checkSignal.Reset();
                        }
                        else if (!doubleCheckTimer.Enabled)
                        {
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
            if (Process == null)
                return;

            ProcessHelper.BringToFront(Process.Id, aggressive);
        }

        private void ResetTimers()
        {
            resetTimer.Set();
            gracePeriodTimer.Stop();
            doubleCheckTimer.Stop();
        }

        private bool ShouldStart
        {
            get { return (Process == null) &&
                    (startSignal.IsSet ||
                    (State == Status.Running &&
                    options.CrashedIfNotRunning &&
                    !gracePeriodTimer.Enabled)); }
        }

        #region Event callbacks

        private void OnProcessStopped(object sender, EventArgs e)
        {
            crashSignal.Set();
            gracePeriodTimer.Stop();
        }

        private void OnDoubleCheckTimeElapsed(object sender, ElapsedEventArgs e)
        {
            checkSignal.Set();
            doubleCheckTimer.Stop();
        }

        private void OnGracePeriodTimeElapsed(object sender, ElapsedEventArgs e)
        {
            startSignal.Set();
            gracePeriodTimer.Stop();
        }

        #endregion

        #region INotifyPropertyChanged support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(State))
                StateChanged?.Invoke();

            else if (propertyName == nameof(ProcessOptions))
                OptionsChanged?.Invoke();

            else if (propertyName == nameof(Process))
                ProcessChanged?.Invoke();
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

                    gracePeriodTimer?.Dispose();
                    doubleCheckTimer?.Dispose();
                    crashSignal?.Dispose();
                    startSignal?.Dispose();
                    checkSignal?.Dispose();
                    resetTimer?.Dispose();
                }

                gracePeriodTimer = null;
                doubleCheckTimer = null;
                crashSignal = null;
                startSignal = null;
                checkSignal = null;
                resetTimer = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
