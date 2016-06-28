namespace oroc
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

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

        // http://stackoverflow.com/a/3839419/388751
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string ToXmlNodeString<T>(this T self)
        {
            XmlSerializerNamespaces serializer_namespace = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlWriterSettings serializer_settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            XmlSerializer serializer = new XmlSerializer(typeof(T), string.Empty);

            using (StringWriter string_writer = new StringWriter())
            using (XmlWriter xml_writer = XmlWriter.Create(string_writer, serializer_settings))
            {
                serializer.Serialize(xml_writer, self, serializer_namespace);
                return string_writer.ToString();
            }
        }
    }
}
