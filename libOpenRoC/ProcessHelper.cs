namespace liboroc
{
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
    }
}
