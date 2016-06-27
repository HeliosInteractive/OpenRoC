namespace oroc
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ProcessManager : IDisposable
    {
        private readonly Dictionary<string, ProcessRunner> Processes;

        public ProcessManager()
        {
            Processes = new Dictionary<string, ProcessRunner>();
        }

        public void Add(ProcessOptions opts)
        {
            if (string.IsNullOrWhiteSpace(opts.Path))
                return;

            ProcessRunner proc = new ProcessRunner(opts);
            Processes.Add(opts.Path, proc);
        }

        public void Delete(string path)
        {
            if (Processes.ContainsKey(path))
            {
                Processes[path].Stop();
                Processes[path].Dispose();
                Processes.Remove(path);
            }
        }

        public bool Contains(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            return Processes.ContainsKey(path);
        }

        public ProcessRunner Get(string path)
        {
            return Processes[path];
        }

        public void Update(ProcessOptions opts)
        {
            Get(opts.Path).UpdateOptions(opts);
        }

        public List<ProcessRunner> ProcessList
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
                    {
                        pair.Value.Stop();
                        pair.Value.Dispose();
                    }
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
