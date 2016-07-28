﻿namespace oroc
{
    using Nancy;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using System.IO;
    using System.Xml;
    using System.Drawing;
    using System.Xml.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml.Serialization;

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

        public static Response ToJsonResponse(this object input)
        {
            var response = (Response)ToJson(input);
            response.ContentType = "application/json";
            return response;
        }
    }
}
