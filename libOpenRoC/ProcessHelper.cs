namespace liboroc
{
    using System;
    using System.Linq;
    using System.Management;
    using System.Diagnostics;
    using System.ComponentModel;

    public static class ProcessHelper
    {
        public static void ExecuteScript(string script_path, string command_line = null, bool headless = false)
        {
            if (string.IsNullOrWhiteSpace(script_path))
                return;

            using (Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = script_path,
                    Arguments = command_line,
                    CreateNoWindow = headless,
                    UseShellExecute = true,
                }
            })
            {
                process.Start();
                process.WaitForExit();
            }
        }

        public static void KillProcess(int pid)
        {
            string query = string.Format("Select * From Win32_Process Where ParentProcessID={0}", pid);

            using (ManagementObjectSearcher processSearcher = new ManagementObjectSearcher(query))
            using (ManagementObjectCollection processCollection = processSearcher.Get())
            {
                try
                {
                    Process proc = Process.GetProcessById(pid);
                    if (!proc.HasExited) proc.Kill();
                }
                catch (ArgumentException) { /* zombie process */ }
                catch (Win32Exception) { /* unable to kill the process */ }

                if (processCollection != null)
                {
                    foreach (ManagementObject mo in processCollection)
                    {
                        KillProcess(Convert.ToInt32(mo["ProcessID"]));
                    }
                }
            }
        }

        public static void KillProcess(string name)
        {
            Process.GetProcessesByName(name).ToList().ForEach(proc =>
            {
                try { KillProcess(proc.Id); }
                catch (InvalidOperationException) { /* zombie process */ }
            });
        }
    }
}
