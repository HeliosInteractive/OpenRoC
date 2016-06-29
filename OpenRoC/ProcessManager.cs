namespace oroc
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Collections.Generic;

    public class ProcessManager : IDisposable, INotifyPropertyChanged
    {
        private Dictionary<string, ProcessRunner> ProcessMap
            = new Dictionary<string, ProcessRunner>();

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
                return;

            ProcessRunner proc = new ProcessRunner(opts);
            proc.PropertyChanged += OnProcessPropertyChanged;
            ProcessMap.Add(opts.Path, proc);
            NotifyPropertyChanged("ProcessMap");
        }

        public void Delete(string path)
        {
            if (ProcessMap.ContainsKey(path))
            {
                ProcessRunner proc = ProcessMap[path];

                proc.Dispose();
                ProcessMap.Remove(path);
                NotifyPropertyChanged("ProcessMap");
            }
        }

        public bool Contains(string path)
        {
            return !string.IsNullOrWhiteSpace(path) &&
                ProcessMap.ContainsKey(path);
        }

        public ProcessRunner Get(string path)
        {
            return ProcessMap[path];
        }

        public void Swap(ProcessOptions opts)
        {
            Get(opts.Path).ProcessOptions = opts;
            NotifyPropertyChanged("ProcessMap");
        }

        private void OnProcessPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "State")
                NotifyPropertyChanged("ProcessMap");
        }

        #region INotifyPropertyChanged support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
