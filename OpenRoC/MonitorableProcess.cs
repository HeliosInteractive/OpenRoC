namespace oroc
{
    using System;
    using System.Diagnostics;

    public class MonitorableProcess : IDisposable
    {
        bool resetTimer = false;
        bool isDisabled = false;
        Status current_state = Status.Stopped;
        Status pending_state = Status.Disabled;

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
                if (isDisabled)
                    return Status.Disabled;
                else
                    return current_state;
            }

            private set
            {
                if (value == Status.Disabled)
                {
                    isDisabled = true;
                    resetTimer = true;
                }
                else if (value != current_state)
                {
                    current_state = value;
                    resetTimer = true;
                }
            }
        }

        public Process Process { get; private set; }
        public Stopwatch Stopwatch { get; private set; }
        public ProcessOptions ProcessOptions { get; private set; }

        public MonitorableProcess(ProcessOptions opts)
        {
            ProcessOptions = opts;
            State = Status.Stopped;
            Stopwatch = new Stopwatch();
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

            Debug.Assert(Process == null);

            OnProcessPreLaunch();

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

            if (Process.Responding)
            {
                OnProcessStarted(this, null);
                State = Status.Running;
            }
            else Stop();
        }

        public void Stop()
        {
            if (Process == null)
                return;

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
            OnProcessPostCrash();
        }

        public void Disable()
        {
            if (State != Status.Disabled)
                State = Status.Disabled;
        }

        public void Enable()
        {
            isDisabled = false;
        }

        private void OnProcessStopped(object sender, EventArgs e)
        {
            if (State == Status.Stopped)
                return;

            Stop();
        }

        private void OnProcessStarted(object sender, EventArgs e)
        {
            if (State == Status.Running)
                return;
        }

        private void OnProcessPreLaunch()
        {
            if (ProcessOptions.PreLaunchScriptEnabled)
                ProcessHelper.ExecuteScript(ProcessOptions.PreLaunchScriptPath);
        }

        private void OnProcessPostCrash()
        {
            if (ProcessOptions.PostCrashScriptEnabled)
                ProcessHelper.ExecuteScript(ProcessOptions.PostCrashScriptPath);
        }

        public void Monitor()
        {
            if (resetTimer)
            {
                Stopwatch.Restart();
                resetTimer = false;
            }

            if (isDisabled && current_state == Status.Running)
            {
                Debug.Assert(current_state != Status.Disabled);
                Debug.Assert(pending_state == Status.Disabled);

                // GUI requested "disable"
                pending_state = current_state;
                Stop();
            }
            else if (!isDisabled && pending_state != Status.Disabled)
            {
                Debug.Assert(current_state == Status.Stopped);
                Debug.Assert(pending_state != Status.Stopped);

                // GUI requested "enable"
                Start();
                pending_state = Status.Disabled;
            }
        }

        #region IDisposable Support
        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                    Stop();

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
