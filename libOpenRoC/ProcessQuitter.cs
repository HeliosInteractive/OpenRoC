namespace liboroc
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Management;
    using System.Diagnostics;

    public class ProcessQuitter
    {
        private static object mutex = new object();
        private static ProcessQuitter instance;

        public static ProcessQuitter Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(mutex)
                    {
                        if (instance == null)
                        {
                            instance = new ProcessQuitter();
                        }
                    }
                }

                return instance;
            }
        }

        private class QuitTask
        {
            public ManualResetEventSlim WaitHandle;
            public int ShutdownPendingProcessId;

            public void execute(object threadContext)
            {
                execute(ShutdownPendingProcessId);
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
                            if (!proc.HasExited)
                            {
                                proc.Kill();
                                proc.WaitForExit((int)TimeSpan.FromSeconds(1).TotalMilliseconds);
                            }
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

        public void Shutdown(int pid)
        {
            // take a look at: http://referencesource.microsoft.com/#System.Management/managementbaseobject.cs,233
            // and also the case that is described here: http://stackoverflow.com/a/11896367/388751
            // thread is necessary otherwise WMI objects in ManagementObjectSearcher leak!

            using (ManualResetEventSlim waitHandle = new ManualResetEventSlim(false))
            {
                QuitTask processQuitExecutor = new QuitTask
                {
                    WaitHandle = waitHandle,
                    ShutdownPendingProcessId = pid
                };

                ThreadPool.QueueUserWorkItem(processQuitExecutor.execute);
                waitHandle.Wait();
            }
        }

        public void Shutdown(string name)
        {
            Process.GetProcessesByName(name)
                .ToList()
                .ForEach(proc =>
                {
                    Shutdown(proc.Id);
                    proc.Dispose();
                });
        }
    }
}
