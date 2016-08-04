namespace liboroc
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;

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

        public static string ToColonDelimitedString(this Dictionary<string, string> self)
        {
            if (self.Count == 0)
                return null;

            return string.Join(";", self
                .Select(x => string.Format("{0}={1}", x.Key, x.Value))
                .ToArray());
        }

        public static bool FromColonDelimitedString(this Dictionary<string, string> self, string colon_delimited)
        {
            if (self.Count > 0)
                self.Clear();

            if (string.IsNullOrWhiteSpace(colon_delimited))
                return false;

            colon_delimited
                .Split(';')
                .Where(x => x.Contains('='))
                .Select(x => x.Split('='))
                .ToList()
                .ForEach(pair => self.Add(pair.First(), pair.Last()));

            return true;
        }

        public static string GetStateString(this ProcessRunner self)
        {
            return string.Format("{0} for {1:hh\\:mm\\:ss}", self.State, self.Stopwatch.Elapsed);
        }

        public static void WaitUntilUnresponsive(this ProcessRunner self)
        {
            while (self.Process.Responding)
            {
                self.Process.Refresh();
                Thread.Sleep(TimeSpan.FromMilliseconds(1));
            }
        }
    }
}
