namespace oroc
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    class Settings
    {
        private XElement openrocRoot;
        private XElement optionsRoot;
        private bool dirty = false;

        public Settings()
        {
            FileInfo config_file = new FileInfo(Path.Combine(
                Program.Directory, Properties.Resources.SettingsFileName));

            if (!config_file.Exists)
                File.WriteAllText(config_file.FullName, Properties.Resources.SettingsBaseXml);

            try
            {
                openrocRoot = XElement.Load(config_file.FullName);
                dirty = false;
            }
            catch (Exception)
            {
                openrocRoot = XElement.Parse(Properties.Resources.SettingsBaseXml);
                dirty = true;
            }

            if (openrocRoot == null || openrocRoot.Name != Properties.Resources.SettingsRootNode)
            {
                openrocRoot = new XElement(Properties.Resources.SettingsRootNode);
                dirty = true;
            }

            optionsRoot = openrocRoot.Element(Properties.Resources.SettingsOptionNode);

            if (optionsRoot == null)
            {
                optionsRoot = new XElement(Properties.Resources.SettingsOptionNode);
                openrocRoot.Add(optionsRoot);
                dirty = true;
            }
        }

        public void Write<T>(string node, T value) where T : new()
        {
            if (optionsRoot.Element(node) == null)
                optionsRoot.Add(new XElement(node));

            optionsRoot.Element(node).ReplaceAll(
                XElement.Parse(value.ToXmlNodeString()).Elements());

            dirty = true;
        }

        public T Read<T>(string node) where T : new()
        {
            if (optionsRoot.Element(node) != null)
                return Extensions.FromXmlNodeString<T>(
                    optionsRoot.Element(node).ToString(), node);
            else return new T();
        }

        public void Save()
        {
            if (!dirty)
                return;

            openrocRoot.Save(Path.Combine(
                Program.Directory,
                Properties.Resources.SettingsFileName));

            dirty = false;
        }
    }
}
