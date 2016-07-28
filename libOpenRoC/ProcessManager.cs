namespace liboroc
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ProcessManager : IDisposable
    {
        private Dictionary<string, ProcessRunner> ProcessMap
            = new Dictionary<string, ProcessRunner>();

        public Action ProcessesChanged;

        public List<ProcessRunner> ProcessRunnerList
        {
            get { return ProcessMap.Values.ToList(); }
        }

        public List<ProcessOptions> ProcessOptionList
        {
            get { return ProcessRunnerList.Select(x => x.ProcessOptions).ToList(); }
        }

        public void Add(ProcessOptions opts)
        {
            if (string.IsNullOrWhiteSpace(opts.Path))
            {
                return;
            }

            ProcessRunner proc = new ProcessRunner(opts);

            proc.StateChanged += OnProcessesChanged;
            proc.OptionsChanged += OnProcessesChanged;

            ProcessMap.Add(opts.Path, proc);

            OnProcessesChanged();
        }

        public void Delete(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            if (ProcessMap.ContainsKey(path))
            {
                ProcessRunner proc = ProcessMap[path];

                proc.Dispose();
                ProcessMap.Remove(path);

                OnProcessesChanged();
            }
            else
            {
                return;
            }
        }

        public bool Contains(string path)
        {
            return !string.IsNullOrWhiteSpace(path) &&
                ProcessMap.ContainsKey(path);
        }

        public ProcessRunner Get(string path)
        {
            if (Contains(path))
                return ProcessMap[path];
            else
                return null;
        }

        public void Swap(ProcessOptions opts)
        {
            Get(opts.Path).ProcessOptions = opts;
        }

        protected void OnProcessesChanged()
        {
            ProcessesChanged?.Invoke();
        }

        #region IDisposable Support
        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    foreach (var pair in ProcessMap)
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
