namespace oroc
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ProcessManager : IDisposable
    {
        private readonly Dictionary<string, MonitorableProcess> Processes;

        public ProcessManager()
        {
            Processes = new Dictionary<string, MonitorableProcess>();
        }

        public bool Add(ProcessOptions opts)
        {
            if (Processes.ContainsKey(opts.Path))
                return false;

            Processes.Add(opts.Path, new MonitorableProcess(opts));
            return true;
        }

        public void Delete(string key)
        {
            if (Processes.ContainsKey(key))
            {
                Processes[key].Dispose();
                Processes.Remove(key);
            }
        }

        public List<MonitorableProcess> ProcessList
        {
            get { return Processes.Values.ToList(); }
        }

        #region IDisposable Support
        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    foreach (var pair in Processes)
                        pair.Value.Dispose();
                }

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
