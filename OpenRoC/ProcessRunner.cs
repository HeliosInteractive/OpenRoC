namespace oroc
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

                Log.d("State changed from {0} to {1} for {2}",
                    previousState, currentState, ProcessPath);

                NotifyPropertyChanged("State");
            }
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

            Log.d("A new ProcessRunner is constructed for {0}", opts.Path);
        }

        private void SwapOptions(ProcessOptions opts)
        {
            Stop();
            options = opts;
            State = opts.InitialStateEnumValue;

            Log.d("Options swaped for {0}", ProcessPath);
            NotifyPropertyChanged("ProcessOptions");
        }

        public void RestoreState()
        {
            if (previousState == Status.Invalid)
                State = Status.Stopped;
            else State = previousState;

            Log.d("State is restored for {0}", ProcessPath);
        }

        public void Start()
        {
            Log.d("About to start {0}", ProcessPath);

            if (Process != null)
            {
                Log.d("Another instance found for {0}.", ProcessPath);
                Stop();
            }

            if (options.PreLaunchScriptEnabled)
            {
                Log.d("Executin pre-launch script of {0}.", ProcessPath);
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
                Log.d("Waiting for Window of {0}", ProcessPath);
                Process.WaitForInputIdle();
                HasWindow = true;
            }
            catch (Exception)
            {
                Log.w("No Window for {0}. Is this a console process?", ProcessPath);
                HasWindow = false;
            }

            if (!HasWindow || Process.Responding)
            {
                State = Status.Invalid;
                State = Status.Running;
            }
            else
            {
                Log.e("Unable to start {0}. It is neither responding nor has a Window",
                    ProcessPath);
                Stop();
            }
        }

        public void Stop()
        {
            Log.d("About to stop {0}", ProcessPath);

            if (Process == null)
            {
                Log.d("Instance is already null {0}", ProcessPath);

                if (!IsDisposed && State != Status.Stopped)
                {
                    Log.d("Force-stopping {0}", ProcessPath);
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

            if (!Process.HasExited)
            {
                Log.d("Attempting to close main Window of {0}", ProcessPath);

                Process.CloseMainWindow();
                Process.WaitForExit(1000);

                if (!Process.HasExited)
                {
                    Log.d("Window did not exit after 1 second {0}", ProcessPath);
                    Process.Kill();
                }
            }

            if (options.AggressiveCleanupEnabled)
            {
                if (options.AggressiveCleanupByName)
                {
                    Log.d("Performing an aggressive clean-up by name {0}. This will not work if you are not admin.",
                        ProcessPath);
                    ProcessHelper.TaskKill(Path.GetFileName(ProcessPath));
                }

                if (options.AggressiveCleanupByPID)
                {
                    Log.d("Performing an aggressive clean-up by PID {0}. This will not work if you are not admin.",
                        ProcessPath);
                    ProcessHelper.TaskKill(Process);
                }
            }

            Log.d("Disposing {0}", ProcessPath);
            Process.Dispose();
            Process = null;

            if (!IsDisposed)
            {
                State = Status.Invalid;
                State = Status.Stopped;
            }

            if (options.PostCrashScriptEnabled)
            {
                Log.d("Executin post-crash script of {0}.", ProcessPath);
                ProcessHelper.ExecuteScript(options.PostCrashScriptPath);
            }
        }

        public void Monitor()
        {
            if (resetTimer.IsSet)
            {
                Log.d("State timer for {0} is reset.", ProcessPath);
                Stopwatch.Restart();
                resetTimer.Reset();
            }

            if (ShouldStart)
            {
                Log.d("Process should start but it's not running {0}.", ProcessPath);
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

            if (currentState != Status.Disabled && HasWindow)
            {
                Process.Refresh();

                if (options.CrashedIfUnresponsive && !Process.Responding)
                {
                    Log.d("Window is not responding {0}.", ProcessPath);

                    if (options.DoubleCheckEnabled)
                    {
                        Log.d("Will double check again in {0} seconds: {1}.", options.DoubleCheckDuration, ProcessPath);

                        if (checkSignal.IsSet)
                        {
                            if (!Process.Responding)
                            {
                                Log.d("Window is still not responding {0}.", ProcessPath);
                                startSignal.Set();
                            }
                            else
                            {
                                Log.d("Window is alive again {0}.", ProcessPath);
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
                        Log.d("Double check is disabled. Process will restart {0}.", ProcessPath);
                        startSignal.Set();
                    }

                    if (startSignal.IsSet && currentState == Status.Stopped)
                    {
                        Log.d("Start signal is masked by GUI {0}.", ProcessPath);
                        Stop();
                    }
                }
                else if (options.AlwaysOnTopEnabled)
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
            Log.d("All timers for {0} are reset.", ProcessPath);
            resetTimer.Set();
            gracePeriodTimer.Stop();
            doubleCheckTimer.Stop();
        }

        private bool ShouldStart
        {
            get { return (startSignal.IsSet || (Process == null && State == Status.Running && options.CrashedIfNotRunning && !gracePeriodTimer.Enabled)); }
        }

        #region Event callbacks

        private void OnProcessStopped(object sender, EventArgs e)
        {
            Log.w("Process is crashed {0}.", ProcessPath);

            Stop();

            if (options.GracePeriodEnabled)
            {
                if (!gracePeriodTimer.Enabled)
                {
                    gracePeriodTimer.Interval = TimeSpan.FromSeconds(options.GracePeriodDuration).TotalMilliseconds;
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

        #region INotifyPropertyChanged support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            Log.d("Prcoess property {0} changed in {1}.", propertyName, ProcessPath);

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDisposable Support

        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            Log.d("Process is disposed {0}", ProcessPath);

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
