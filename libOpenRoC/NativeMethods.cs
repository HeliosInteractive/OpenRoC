namespace liboroc
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        [Flags]
        public enum SetWindowPosFlags : uint
        {
            SWP_NOMOVE      = 0x0002,
            SWP_NOSIZE      = 0x0001,
            SWP_SHOWWINDOW  = 0x0040
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
    }
}
