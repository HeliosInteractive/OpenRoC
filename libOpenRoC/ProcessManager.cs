namespace liboroc
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ProcessManager : IDisposable
    {
        private Dictionary<string, ProcessRunner> processMap
            = new Dictionary<string, ProcessRunner>();

        public Action ProcessesChanged;
        public Action<ProcessRunner> RunnerAdded;
        public Action<ProcessRunner> RunnerRemoved;

        public List<ProcessRunner> Runners
        {
            get { return processMap.Values.ToList(); }
        }

        public List<ProcessOptions> Options
        {
            get { return Runners.Select(x => x.ProcessOptions).ToList(); }
        }

        public void Add(ProcessOptions opts)
        {
            if (!Contains(opts.Path))
            {
                ProcessRunner proc = new ProcessRunner(opts);

                proc.StateChanged += OnProcessesChanged;
                proc.OptionsChanged += OnProcessesChanged;

                processMap.Add(opts.Path, proc);
                RunnerAdded?.Invoke(proc);

                OnProcessesChanged();
            }
        }

        public void Remove(string path)
        {
            if (Contains(path))
            {
                using (ProcessRunner proc = processMap[path])
                {
                    processMap.Remove(path);
                    RunnerRemoved?.Invoke(proc);
                }

                OnProcessesChanged();
            }
        }

        public bool Contains(string path)
        {
            return !string.IsNullOrWhiteSpace(path) &&
                processMap.ContainsKey(path);
        }

        public ProcessRunner Get(string path)
        {
            if (Contains(path))
                return processMap[path];
            else
                return null;
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
                    foreach (var pair in processMap)
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
