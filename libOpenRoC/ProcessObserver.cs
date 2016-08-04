namespace liboroc
{
    using System;
    using System.Management;
    using System.Diagnostics;

    public class ProcessObserver
    {
        public interface IVisitor
        {
            void Visit(Process process);
        }

        private static object mutex = new object();
        private static ProcessObserver instance;

        public static ProcessObserver Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (mutex)
                    {
                        if (instance == null)
                        {
                            instance = new ProcessObserver();
                        }
                    }
                }

                return instance;
            }
        }

        public void Accept(Action<Process> visitor, int pid)
        {
            using (ExecutorService service = new ExecutorService())
            {
                service.Accept(() => { execute(visitor, pid); });
            }
        }

        private void execute(Action<Process> visitor, int pid)
        {
            string query = string.Format("Select * From Win32_Process Where ParentProcessID={0}", pid);
            using (ManagementObjectSearcher processSearcher = new ManagementObjectSearcher(query))
            using (ManagementObjectCollection processCollection = processSearcher.Get())
            {
                try
                {
                    using (Process proc = Process.GetProcessById(pid))
                    {
                        visitor?.Invoke(proc);
                    }
                }
                catch { /* there is nothing we can do about it */ }

                if (processCollection != null)
                {
                    foreach (ManagementObject mo in processCollection)
                    {
                        execute(visitor, Convert.ToInt32(mo["ProcessID"]));
                    }
                }
            }
        }
    }
}
