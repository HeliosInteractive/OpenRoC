namespace oroc
{
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    public class ProcessManager : IDisposable
    {
        private Dictionary<string, ProcessRunner> ProcessMap
            = new Dictionary<string, ProcessRunner>();

        public List<ProcessRunner> ProcessList { get; set; }
            = new List<ProcessRunner>();

        [XmlIgnore]
        public Action<string> OnProcessAdded;

        [XmlIgnore]
        public Action<string> OnProcessEdited;

        [XmlIgnore]
        public Action<string> OnProcessDeleted;

        public void Add(ProcessOptions opts)
        {
            if (string.IsNullOrWhiteSpace(opts.Path))
                return;

            ProcessRunner proc = new ProcessRunner(opts);

            ProcessList.Add(proc);
            ProcessMap.Add(opts.Path, proc);

            if (OnProcessAdded != null)
                OnProcessAdded(opts.Path);
        }

        public void Delete(string path)
        {
            if (ProcessMap.ContainsKey(path))
            {
                ProcessRunner proc = ProcessMap[path];

                proc.Dispose();
                ProcessMap.Remove(path);
                ProcessList.Remove(proc);

                if (OnProcessDeleted != null)
                    OnProcessDeleted(path);
            }
        }

        public bool Contains(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            return ProcessMap.ContainsKey(path);
        }

        public ProcessRunner Get(string path)
        {
            return ProcessMap[path];
        }

        public void Swap(ProcessOptions opts)
        {
            Get(opts.Path).SwapOptions(opts);

            if (OnProcessEdited != null)
                OnProcessEdited(opts.Path);
        }

        public void Setup()
        {
            if (ProcessMap.Count > 0)
            {
                foreach (var pair in ProcessMap)
                    pair.Value.Dispose();

                ProcessMap.Clear();
            }

            ProcessList.ForEach(runner =>
            {
                ProcessMap.Add(runner.ProcessOptions.Path, runner);
            });
        }

        #region IDisposable Support

        [XmlIgnore]
        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    foreach (var pair in ProcessMap)
                    {
                        pair.Value.Stop();
                        pair.Value.Dispose();
                    }
                }

                ProcessList = null;
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
