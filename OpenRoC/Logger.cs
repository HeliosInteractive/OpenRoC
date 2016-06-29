namespace oroc
{
    using System;
    using log4net;
    using System.IO;
    using log4net.Core;
    using log4net.Config;
    using System.Drawing;
    using System.Xml.Linq;
    using log4net.Appender;
    using System.Windows.Forms;

    class Logger
    {
        public static readonly ILog Instance = LogManager.GetLogger(typeof(Logger));

        public static void Configure(Form owner, RichTextBox logBox)
        {
            FileInfo config_file = new FileInfo(
                Path.Combine(Program.Directory, Properties.Resources.SettingsFileName));

            if (!config_file.Exists)
                File.WriteAllText(config_file.FullName, Properties.Resources.SettingsBaseXml);

            XElement phoenix_root = null;

            try { phoenix_root = XElement.Load(config_file.FullName); }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(Logger))
                .ErrorFormat("Root element cannot be loaded: {0}", ex.Message);

                phoenix_root = XElement.Parse(Properties.Resources.SettingsBaseXml);
            }

            if (phoenix_root == null || phoenix_root.Name != Properties.Resources.SettingsRootNode)
            {
                Instance.Warn("Phoenix node not found. It will be created.");
                phoenix_root = new XElement(Properties.Resources.SettingsRootNode);
            }

            XElement log4net_root = phoenix_root.Element(Properties.Resources.SettingsLog4NetNode);

            if (log4net_root == null)
            {
                Instance.Error("Options node not found. It will be created.");
                log4net_root = new XElement(Properties.Resources.SettingsLog4NetNode);
                phoenix_root.Add(log4net_root);
            }

            XmlConfigurator.Configure(log4net_root.AsXmlElement());
            BasicConfigurator.Configure(new TextBoxAppender(logBox, owner));

            Instance.Debug("Logger configured.");
        }

        internal class TextBoxAppender : AppenderSkeleton
        {
            private RichTextBox textBox;

            public TextBoxAppender(RichTextBox box, Form box_owner)
            {
                textBox = box;
                Threshold = Level.All;
                box_owner.FormClosing += (s, e) => textBox = null;
            }

            protected override void Append(LoggingEvent loggingEvent)
            {
                if (textBox == null || textBox.Disposing || textBox.IsDisposed)
                    return;

                textBox.BeginInvoke((MethodInvoker)delegate
                {
                    textBox.AppendText(loggingEvent.RenderedMessage + Environment.NewLine, GetLevelColor(loggingEvent.Level));

                    if (!textBox.Visible)
                    {
                        textBox.SelectionStart = textBox.TextLength;
                        textBox.ScrollToCaret();
                    }
                });
            }

            static Color GetLevelColor(Level level)
            {
                if (level == Level.Error || level == Level.Critical || level == Level.Fatal)
                    return Color.Red;
                else if (level == Level.Debug || level == Level.Notice)
                    return Color.DarkSlateGray;
                else if (level == Level.Warn)
                    return Color.Blue;
                else
                    return Color.Black;
            }
        }
    }
}