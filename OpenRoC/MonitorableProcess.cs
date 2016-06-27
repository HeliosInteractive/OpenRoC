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
