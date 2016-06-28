namespace oroc
{
    using System;
    using System.Diagnostics;

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

        public static void TaskKill(string name)
        {
            ExecuteScript("taskkill", string.Format("/F /T /IM \"{0}\"", name), true);
        }

        public static void TaskKill(Process process)
        {
            try { ExecuteScript("taskkill", string.Format("/F /T /PID {0}", process.Id), true); }
            catch (InvalidOperationException) { /* PID is invalid*/ }
        }
    }
}
