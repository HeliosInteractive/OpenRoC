namespace oroc
{
    using System;
    using System.Timers;
    using System.Diagnostics;

    public class ProcessRunner : IDisposable
    {
        bool is_disabled = false;
        bool reset_timer = false;
        bool should_start = false;
        bool double_check = false;
        Status current_state = Status.Stopped;
        Status pending_state = Status.Disabled;
        Timer grace_period_timer = new Timer();
        Timer double_check_timer = new Timer();

        public enum Status
        {
            Stopped,
            Running,
            Disabled
        }

        public Status State
        {
            get
            {
                if (is_disabled)
                    return Status.Disabled;
                else
                    return current_state;
            }

            private set
            {
                if (value == Status.Disabled)
                {
                    IsDisabled = true;
                }
                else if (value != current_state)
                {
                    current_state = value;
                    reset_timer = true;
                }
            }
        }

        public bool IsDisabled
        {
            get
            {
                return is_disabled;
            }

            set
            {
                if (IsDisabled == value)
                    return;

                if (value)
                {
                    is_disabled = true;
                    reset_timer = true;

                    grace_period_timer.Stop();
                    double_check_timer.Stop();
                }
                else
                {
                    is_disabled = false;
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

            grace_period_timer.AutoReset = false;
            grace_period_timer.Elapsed += GracePeriodTimeElapsed;

            double_check_timer.AutoReset = false;
            double_check_timer.Elapsed += DoubleCheckTimeElapsed;
        }

        public void UpdateOptions(ProcessOptions opts)
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
            grace_period_timer.Stop();
            double_check_timer.Stop();

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

            Process.Dispose();
            Process = null;

            State = Status.Stopped;

            if (ProcessOptions.PostCrashScriptEnabled)
                ProcessHelper.ExecuteScript(ProcessOptions.PostCrashScriptPath);
        }

        public void Monitor()
        {
            if (reset_timer)
            {
                Stopwatch.Restart();
                reset_timer = false;
            }

            if (IsDisabled && current_state == Status.Running)
            {
                Debug.Assert(current_state != Status.Disabled);
                Debug.Assert(pending_state == Status.Disabled);

                // GUI requested "disable"
                pending_state = current_state;
                Stop();
            }
            else if (!IsDisabled && pending_state != Status.Disabled)
            {
                Debug.Assert(current_state == Status.Stopped);
                Debug.Assert(pending_state != Status.Stopped);

                // GUI requested "enable"
                Start();
                pending_state = Status.Disabled;
            }
            
            if (!IsDisabled)
            {
                if (ShouldStart)
                {
                    Debug.Assert(State == Status.Stopped);
                    Debug.Assert(Process == null);

                    should_start = false;
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
                            if (double_check)
                            {
                                should_start = !Process.Responding;
                                double_check = false;
                            }
                            else if (!double_check_timer.Enabled)
                            {
                                double_check_timer.Interval = TimeSpan.FromSeconds(ProcessOptions.DoubleCheckDuration).TotalMilliseconds;
                                double_check_timer.Start();
                            }
                        }
                        else
                        {
                            should_start = true;
                        }

                        if (should_start && current_state == Status.Stopped)
                            Stop();
                    }
                    else if (ProcessOptions.AlwaysOnTopEnabled)
                    {
                        BringToFront();
                    }
                }
            }
        }

        private bool ShouldStart
        {
            get { return (should_start || (Process == null && ProcessOptions.CrashedIfNotRunning && !grace_period_timer.Enabled)); }
        }

        public void BringToFront()
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
                NativeMethods.SetWindowPos(
                    Process.MainWindowHandle,
                    NativeMethods.HWND_TOPMOST,
                    0, 0, 0, 0,
                    NativeMethods.SetWindowPosFlags.SWP_NOSIZE |
                    NativeMethods.SetWindowPosFlags.SWP_NOMOVE |
                    NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
        }

        #region Event callbacks

        private void OnProcessStopped(object sender, EventArgs e)
        {
            Stop();

            if (ProcessOptions.GracePeriodEnabled)
            {
                if (!grace_period_timer.Enabled)
                {
                    grace_period_timer.Interval = TimeSpan.FromSeconds(ProcessOptions.GracePeriodDuration).TotalMilliseconds;
                    grace_period_timer.Start();
                }
            }
            else
            {
                should_start = true;
            }
        }

        private void DoubleCheckTimeElapsed(object sender, ElapsedEventArgs e)
        {
            double_check = true;
        }

        private void GracePeriodTimeElapsed(object sender, ElapsedEventArgs e)
        {
            should_start = true;
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
                    if (grace_period_timer != null)
                        grace_period_timer.Dispose();

                    if (double_check_timer != null)
                        double_check_timer.Dispose();

                    Stop();
                }

                grace_period_timer = null;
                double_check_timer = null;
                ProcessOptions = null;
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
