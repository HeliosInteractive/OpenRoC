namespace oroc
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    public sealed class Settings
    {
        private static volatile object mutex = new object();
        private static volatile Settings instance;
        private Application application;
        private XElement openrocRoot;
        private XElement optionsRoot;
        private volatile bool dirty = false;

        public class Application
        {
            public bool singleInsntace = true;
            public bool startMinimized = false;
            public bool sensuInterfaceEnabled = false;
            public string sensuInterfaceHost = "127.0.0.1";
            public uint sensuInterfacePort = 3030;
            public uint sensuInterfaceTTL = 30;
        }

        public bool IsSingleInsntaceEnabled
        {
            get { return application.singleInsntace; }
            set
            {
                if (value == application.singleInsntace)
                    return;

                application.singleInsntace = value;
                dirty = true;
            }
        }

        public bool IsStartMinimizedEnabled
        {
            get { return application.startMinimized; }
            set
            {
                if (value == application.startMinimized)
                    return;

                application.startMinimized = value;
                dirty = true;
            }
        }

        public bool IsSensuInterfaceEnabled
        {
            get { return application.sensuInterfaceEnabled; }
            set
            {
                if (value == application.sensuInterfaceEnabled)
                    return;

                application.sensuInterfaceEnabled = value;
                dirty = true;
            }
        }

        public string SensuInterfaceHost
        {
            get { return application.sensuInterfaceHost; }
            set
            {
                if (value == application.sensuInterfaceHost)
                    return;

                application.sensuInterfaceHost = value;
                dirty = true;
            }
        }

        public uint SensuInterfacePort
        {
            get { return application.sensuInterfacePort; }
            set
            {
                if (value == application.sensuInterfacePort)
                    return;

                application.sensuInterfacePort = value;
                dirty = true;
            }
        }

        public uint SensuInterfaceTTL
        {
            get { return application.sensuInterfaceTTL; }
            set
            {
                if (value == application.sensuInterfaceTTL)
                    return;

                application.sensuInterfaceTTL = value;

                if (application.sensuInterfaceTTL < 2)
                    application.sensuInterfaceTTL = 2;

                dirty = true;
            }
        }

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (mutex)
                    {
                        if (instance == null)
                        {
                            instance = new Settings();
                            instance.Setup();
                            instance.Save();
                        }
                    }
                }

                return instance;
            }
        }

        private void Setup()
        {
            FileInfo config_file = new FileInfo(Path.Combine(
                Program.Directory, Properties.Resources.SettingsFileName));

            if (!config_file.Exists)
            {
                Log.w("Config file does not exist. It will be created at {0}", config_file.FullName);
                File.WriteAllText(config_file.FullName, Properties.Resources.SettingsBaseXml);
            }

            try
            {
                openrocRoot = XElement.Load(config_file.FullName);
                Log.d("Succesfully loaded config file {0}", config_file.FullName);
                dirty = false;
            }
            catch (Exception ex)
            {
                openrocRoot = XElement.Parse(Properties.Resources.SettingsBaseXml);
                Log.w("Failed to load config file. Defaults will be used: Reason: {0}", ex.Message);
                dirty = true;
            }

            if (openrocRoot == null || openrocRoot.Name != Properties.Resources.SettingsRootNode)
            {
                openrocRoot = new XElement(Properties.Resources.SettingsRootNode);
                Log.w("Config root name is different than {0}", Properties.Resources.SettingsRootNode);
                dirty = true;
            }

            optionsRoot = openrocRoot.Element(Properties.Resources.SettingsOptionNode);

            if (optionsRoot == null)
            {
                Log.w("Options root name is empty. A node will be created for it.");
                optionsRoot = new XElement(Properties.Resources.SettingsOptionNode);
                openrocRoot.Add(optionsRoot);
                dirty = true;
            }

            application = Read<Application>(Properties.Resources.SettingsApplicationNode);
            Write(Properties.Resources.SettingsApplicationNode, application);

        }

        public void Write<T>(string node, T value) where T : new()
        {
            lock (this)
            {
                try
                {
                    if (optionsRoot.Element(node) == null)
                        optionsRoot.Add(new XElement(node));

                    optionsRoot.Element(node).ReplaceAll(
                        XElement.Parse(value.ToXmlNodeString()).Elements());

                    dirty = true;
                }
                catch (Exception ex)
                {
                    Log.e("Failed to write XML string for node: {0}. Reason: {1}", node, ex.Message);
                    dirty = false;
                }
            }
        }

        public T Read<T>(string node) where T : new()
        {
            lock (this)
            {
                try
                {
                    if (optionsRoot.Element(node) != null)
                        return Extensions.FromXmlNodeString<T>(
                            optionsRoot.Element(node).ToString(), node);
                }
                catch (Exception ex)
                {
                    Log.e("Failed to read XML string from node: {0}. Reason: {1}", node, ex.Message);
                }

                return new T();
            }
        }

        public void Save()
        {
            if (!dirty)
                return;

            lock (this)
            {
                Write(Properties.Resources.SettingsApplicationNode, application);

                openrocRoot.Save(Path.Combine(
                    Program.Directory,
                    Properties.Resources.SettingsFileName));

                dirty = false;
            }
        }
    }
}
