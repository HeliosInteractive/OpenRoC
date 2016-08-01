namespace oroc
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using System;
    using System.IO;
    using System.Xml;
    using System.Text;
    using System.Drawing;
    using System.Xml.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    using liboroc;

    internal static class Extensions
    {
        public static bool SetDoubleBuffered(this Control control, bool enable)
        {
            PropertyInfo doubleBufferPropertyInfo = control.GetType().GetProperty(
                "DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);

            if (doubleBufferPropertyInfo != null)
            {
                bool current = (bool)doubleBufferPropertyInfo.GetValue(control);

                if (current != enable)
                {
                    doubleBufferPropertyInfo.SetValue(control, enable, null);
                    return true;
                }
            }

            return false;
        }

        public static string ToXmlNodeString<T>(this T self)
        {
            XmlSerializerNamespaces serializer_namespace = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlWriterSettings serializer_settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            StringWriter string_writer = new StringWriter();
            using (XmlWriter xml_writer = XmlWriter.Create(string_writer, serializer_settings))
            {
                serializer.Serialize(xml_writer, self, serializer_namespace);
                return string_writer.ToString();
            }
        }

        public static T FromXmlNodeString<T>(string node, string root)
        {
            XmlReaderSettings serializer_settings = new XmlReaderSettings { ValidationType = ValidationType.None };
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(root));

            using (XmlReader xml_reader = XmlReader.Create(new StringReader(node), serializer_settings))
            {
                return (T)serializer.Deserialize(xml_reader);
            }
        }

        public static XmlElement AsXmlElement(this XElement el)
        {
            var doc = new XmlDocument();
            doc.Load(el.CreateReader());
            return doc.DocumentElement;
        }

        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static string ToJson(this object input)
        {
            var settings = new JsonSerializerSettings();

            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Converters.Add(new StringEnumConverter());

            return JsonConvert.SerializeObject(input, Newtonsoft.Json.Formatting.None, settings);
        }

        public static void SetupDataBind(this TextBox control, object instance, string prop)
        {
            control.SetupDataBind(nameof(control.Text), instance, prop);
        }

        public static void SetupDataBind(this CheckBox control, object instance, string prop)
        {
            control.SetupDataBind(nameof(control.Checked), instance, prop);
        }

        public static void SetupDataBind(this Control control, string dest, object instance, string src)
        {
            control.DataBindings.Add(new Binding(dest, instance, src));
        }

        public static void ExecuteOnMainThread(this Form form, Action task)
        {
            form.Invoke((MethodInvoker)delegate
            {
                try { task?.Invoke(); }
                catch (Exception ex) { Log.e("Main thread execution failed: {0}", ex.Message); }
            });
        }

        public static void ShiftLeft(this double[] array, double last_value = default(double))
        {
            int last_index = array.Length - 1;
            Array.Copy(array, 1, array, 0, last_index);
            array[last_index] = last_value;
        }

        public static byte[] ToSensuCheck(this ProcessRunner runner)
        {
            return Encoding.Default.GetBytes(ToJson(new
            {
                name = Path.GetFileName(runner.ProcessOptions.Path),
                output = runner.GetStateString(),
                status = (int)runner.State
            }));
        }
    }
}
