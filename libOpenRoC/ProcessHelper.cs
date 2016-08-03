namespace liboroc
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

        public static void Shutdown(int pid)
        {
            ProcessObserver.Instance.Accept((process) =>
            {
                if (!process.HasExited)
                {
                    process.Kill();
                    process.WaitForExit((int)TimeSpan.FromSeconds(1).TotalMilliseconds);
                }
            }
            , pid);
        }

        public static void BringToFront(int pid, bool aggressive)
        {
            ProcessObserver.Instance.Accept((process) =>
            {
                if (!process.HasExited && process.MainWindowHandle != IntPtr.Zero)
                {
                    if (NativeMethods.GetForegroundWindow() != process.MainWindowHandle)
                    {
                        NativeMethods.SwitchToThisWindow(process.MainWindowHandle, true);
                        NativeMethods.SetForegroundWindow(process.MainWindowHandle);

                        if (aggressive)
                        {
                            NativeMethods.SetWindowPos(
                                process.MainWindowHandle,
                                new IntPtr(-1), //HWND_TOPMOST
                                0, 0, 0, 0,
                                NativeMethods.SetWindowPosFlags.SWP_NOSIZE |
                                NativeMethods.SetWindowPosFlags.SWP_NOMOVE |
                                NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
                        }
                    }
                }
            }
            , pid);
        }
    }
}
