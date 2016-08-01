namespace liboroc
{
    using System;
    using System.Threading;
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

        private class VisitTask
        {
            public ManualResetEventSlim WaitHandle;
            public IVisitor Visitor;
            public int Pid;

            public void execute(object threadContext)
            {
                execute(Pid);
                WaitHandle.Set();
            }

            private void execute(int pid)
            {
                string query = string.Format("Select * From Win32_Process Where ParentProcessID={0}", pid);
                using (ManagementObjectSearcher processSearcher = new ManagementObjectSearcher(query))
                using (ManagementObjectCollection processCollection = processSearcher.Get())
                {
                    try
                    {
                        using (Process proc = Process.GetProcessById(pid))
                        {
                            Visitor.Visit(proc);
                        }
                    }
                    catch { /* there is nothing we can do about it */ }

                    if (processCollection != null)
                    {
                        foreach (ManagementObject mo in processCollection)
                        {
                            execute(Convert.ToInt32(mo["ProcessID"]));
                        }
                    }
                }
            }
        }

        public void Accept(IVisitor visitor, int pid)
        {
            using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
            {
                VisitTask processVisitTask = new VisitTask
                {
                    WaitHandle = waitHandle,
                    Visitor = visitor,
                    Pid = pid
                };

                ThreadPool.QueueUserWorkItem(processVisitTask.execute);
                waitHandle.Wait();
            }
        }
    }
}
