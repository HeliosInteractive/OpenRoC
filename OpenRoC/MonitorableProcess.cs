namespace oroc
{
    using System;
    using System.Diagnostics;

    public class MonitorableProcess : IDisposable
    {
        public enum Status
        {
            Stopped,
            Running,
            Disabled
        }

        public Status State { get; private set; }
        public Process Process { get; private set; }
        public ProcessOptions ProcessOptions { get; private set; }

        public MonitorableProcess(ProcessOptions opts)
        {
            ProcessOptions = opts;
        }

        public void UpdateOptions(ProcessOptions opts)
        {
            if (ReferenceEquals(ProcessOptions, opts))
                return;

            // TODO stop and swap here
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

            if (ProcessOptions.CommandLineEnabled)
                sinfo.Arguments = ProcessOptions.CommandLine;

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

            Process.Start();

            State = Status.Running;
        }

        public void Stop()
        {
            if (Process == null && Process.HasExited)
                return;

            State = Status.Stopped;

            Process.EnableRaisingEvents = false;

            Process.CloseMainWindow();
            Process.WaitForExit(1000);

            if (!Process.HasExited)
                Process.Kill();

            Process.Dispose();
            Process = null;

            if (ProcessOptions.PostCrashScriptEnabled)
                ProcessHelper.ExecuteScript(ProcessOptions.PostCrashScriptPath);
        }

        public void Monitor()
        {
            // TODO
        }

        #region IDisposable Support
        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    if (Process != null)
                        Process.Dispose();
                }

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
