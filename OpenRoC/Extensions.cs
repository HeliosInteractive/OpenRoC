namespace oroc
{
    using System;
    using System.IO;

    public static class Extensions
    {
        public static bool IsDirectory(this string self)
        {
            try { return File.GetAttributes(self).HasFlag(FileAttributes.Directory); }
            catch (Exception) { return false; }
        }

        public static bool IsFile(this string self)
        {
            try { return File.GetAttributes(self).HasFlag(FileAttributes.Normal); }
            catch (Exception) { return false; }
        }

        public static bool IsExecutable(this string self)
        {
            return self.IsFile() && self.EndsWith(".exe");
        }
    }
}
