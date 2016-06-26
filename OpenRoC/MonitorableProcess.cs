namespace oroc
{
    using System;
    using System.Diagnostics;
    using System.IO;
    public class MonitorableProcess : IDisposable
    {
        public enum Status
        {
            Stopped,
            Running,
            Disabled
        }

        private Process process;
        private ProcessOptions processOptions;
        public Status State { get; private set; }
        public string Path { get { return processOptions.Path; } }

        public MonitorableProcess(ProcessOptions opts)
        {
            processOptions = opts;
        }

        public ProcessOptions GetOptions()
        {
            return processOptions;
        }

        #region IDisposable Support
        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    if (process != null)
                        process.Dispose();
                }

                processOptions = null;
                process = null;

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
