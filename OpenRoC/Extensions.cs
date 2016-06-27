namespace oroc
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    public static class Extensions
    {
        public static bool IsDirectory(this string self)
        {
            try { return File.GetAttributes(self).HasFlag(FileAttributes.Directory); }
            catch (Exception) { return false; }
        }

        public static bool IsFile(this string self)
        {
            try { return !File.GetAttributes(self).HasFlag(FileAttributes.Directory); }
            catch (Exception) { return false; }
        }

        public static bool IsExecutable(this string self)
        {
            return self.IsFile() && self.EndsWith(".exe");
        }

        public static string GetStatusString(this ProcessRunner self)
        {
            return string.Format("{0} for {1:hh\\:mm\\:ss}", self.State, self.Stopwatch.Elapsed);
        }

        public static void SetDoubleBuffered(this Control control, bool enable)
        {
            PropertyInfo doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            if (doubleBufferPropertyInfo != null) doubleBufferPropertyInfo.SetValue(control, enable, null);
        }
    }
}
