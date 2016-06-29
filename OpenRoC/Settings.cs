namespace oroc
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    class Settings
    {
        private XElement openrocRoot;
        private XElement optionsRoot;

        public Settings()
        {
            FileInfo config_file = new FileInfo(
                Path.Combine(Program.Directory, Properties.Resources.SettingsFileName));

            if (!config_file.Exists)
                File.WriteAllText(config_file.FullName, Properties.Resources.SettingsBaseXml);

            try { openrocRoot = XElement.Load(config_file.FullName); }
            catch (Exception) { openrocRoot = XElement.Parse(Properties.Resources.SettingsBaseXml); }

            if (openrocRoot == null || openrocRoot.Name != Properties.Resources.SettingsRootNode)
                openrocRoot = new XElement(Properties.Resources.SettingsRootNode);

            optionsRoot = openrocRoot.Element(Properties.Resources.SettingsOptionNode);

            if (optionsRoot == null)
            {
                optionsRoot = new XElement(Properties.Resources.SettingsOptionNode);
                openrocRoot.Add(optionsRoot);
            }
        }

        public void Write<T>(string node, T value) where T : new()
        {
            if (optionsRoot.Element(node) == null)
                optionsRoot.Add(new XElement(node));

            optionsRoot.Element(node).ReplaceAll(XElement.Parse(value.ToXmlNodeString()).FirstNode);
        }

        public T Read<T>(string node) where T : new()
        {
            if (optionsRoot.Element(node) != null)
                return Extensions.FromXmlNodeString<T>(
                    optionsRoot.Element(node).ToString());
            else return new T();
        }

        public void Save()
        {
            openrocRoot.Save(Path.Combine(
                Program.Directory,
                Properties.Resources.SettingsFileName));
        }
    }
}
