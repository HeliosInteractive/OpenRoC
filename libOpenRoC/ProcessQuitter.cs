namespace liboroc
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Management;
    using System.Diagnostics;

    public static class ProcessQuitter
    {
        static public void Shutdown(int pid)
        {
            // take a look at: http://referencesource.microsoft.com/#System.Management/managementbaseobject.cs,233
            // and also the case that is described here: http://stackoverflow.com/a/11896367/388751
            // thread is necessary otherwise WMI objects in ManagementObjectSearcher leak!
            Thread quitterThread = new Thread(new ThreadStart(() => { shutdown(pid); }));
            quitterThread.Start();
            quitterThread.Join();
        }

        static public void Shutdown(string name)
        {
            Process.GetProcessesByName(name)
                .ToList()
                .ForEach(proc =>
                {
                    Shutdown(proc.Id);
                    proc.Dispose();
                });
        }

        static private void shutdown(int pid)
        {
            string query = string.Format("Select * From Win32_Process Where ParentProcessID={0}", pid);
            using (ManagementObjectSearcher processSearcher = new ManagementObjectSearcher(query))
            using (ManagementObjectCollection processCollection = processSearcher.Get())
            using (Process proc = Process.GetProcessById(pid))
            {
                if (!proc.HasExited)
                {
                    proc.Kill();
                    proc.WaitForExit((int)TimeSpan.FromSeconds(1).TotalMilliseconds);
                }

                if (processCollection != null)
                {
                    foreach (ManagementObject mo in processCollection)
                    {
                        shutdown(Convert.ToInt32(mo["ProcessID"]));
                    }
                }

            }
        }
    }
}
