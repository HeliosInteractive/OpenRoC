namespace oroc
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    public class ProcessOptions : INotifyPropertyChanged, ICloneable
    {
        #region private data holders

        private string path;
        private string workingDirectory;
        private bool crashedIfNotRunning;
        private bool crashedIfUnresponsive;
        private bool doubleCheckEnabled;
        private uint doubleCheckDuration;
        private bool gracePeriodEnabled;
        private uint gracePeriodDuration;
        private bool preLaunchScriptEnabled;
        private string preLaunchScriptPath;
        private bool aggressiveCleanupEnabled;
        private bool aggressiveCleanupByName;
        private bool aggressiveCleanupByPID;
        private bool postCrashScriptEnabled;
        private string postCrashScriptPath;
        private bool screenShotEnabled;
        private bool alwaysOnTopEnabled;
        private bool commandLineEnabled;
        private string commandLine;
        private bool environmentVariablesEnabled;
        private readonly Dictionary<string, string> environmentVariables;
        private ProcessRunner.Status initialState;

        #endregion

        public ProcessOptions()
        {
            initialState = ProcessRunner.Status.Stopped;
            environmentVariables = new Dictionary<string, string>();
        }

        public ProcessOptions(Dictionary<string, string> variables)
        {
            initialState = ProcessRunner.Status.Stopped;
            environmentVariables = variables;
        }

        #region DataBind accessible properties 

        public string Path
        {
            get { return path; }
            set
            {
                if (path == value || !value.IsExecutable())
                    return;

                path = value;
                NotifyPropertyChanged("Path");
            }
        }

        public string WorkingDirectory
        {
            get { return workingDirectory; }
            set
            {
                if (workingDirectory == value || !value.IsDirectory())
                    return;

                workingDirectory = value;
                NotifyPropertyChanged("WorkingDirectory");
            }
        }

        public bool CrashedIfNotRunning
        {
            get { return crashedIfNotRunning; }
            set
            {
                if (crashedIfNotRunning == value)
                    return;

                crashedIfNotRunning = value;
                NotifyPropertyChanged("CrashedIfNotRunning");
            }
        }

        public bool CrashedIfUnresponsive
        {
            get { return crashedIfUnresponsive; }
            set
            {
                if (crashedIfUnresponsive == value)
                    return;

                crashedIfUnresponsive = value;
                NotifyPropertyChanged("CrashedIfUnresponsive");
            }
        }

        public bool DoubleCheckEnabled
        {
            get { return doubleCheckEnabled; }
            set
            {
                if (doubleCheckEnabled == value)
                    return;

                doubleCheckEnabled = value;
                NotifyPropertyChanged("DoubleCheckEnabled");
            }
        }

        public uint DoubleCheckDuration
        {
            get { return doubleCheckDuration; }
            set
            {
                if (doubleCheckDuration == value)
                    return;

                doubleCheckDuration = value;
                NotifyPropertyChanged("DoubleCheckDuration");
            }
        }

        public bool GracePeriodEnabled
        {
            get { return gracePeriodEnabled; }
            set
            {
                if (gracePeriodEnabled == value)
                    return;

                gracePeriodEnabled = value;
                NotifyPropertyChanged("GracePeriodEnabled");
            }
        }

        public uint GracePeriodDuration
        {
            get { return gracePeriodDuration; }
            set
            {
                if (gracePeriodDuration == value)
                    return;

                gracePeriodDuration = value;
                NotifyPropertyChanged("GracePeriodDuration");
            }
        }

        public bool PreLaunchScriptEnabled
        {
            get { return preLaunchScriptEnabled; }
            set
            {
                if (preLaunchScriptEnabled == value)
                    return;

                preLaunchScriptEnabled = value;
                NotifyPropertyChanged("PreLaunchScriptEnabled");
            }
        }

        public string PreLaunchScriptPath
        {
            get { return preLaunchScriptPath; }
            set
            {
                if (preLaunchScriptPath == value || !value.IsFile())
                    return;

                preLaunchScriptPath = value;
                NotifyPropertyChanged("PreLaunchScriptPath");
            }
        }

        public bool AggressiveCleanupEnabled
        {
            get { return aggressiveCleanupEnabled; }
            set
            {
                if (aggressiveCleanupEnabled == value)
                    return;

                aggressiveCleanupEnabled = value;
                NotifyPropertyChanged("AggressiveCleanupEnabled");
            }
        }

        public bool AggressiveCleanupByName
        {
            get { return aggressiveCleanupByName; }
            set
            {
                if (aggressiveCleanupByName == value)
                    return;

                aggressiveCleanupByName = value;
                NotifyPropertyChanged("AggressiveCleanupByName");
            }
        }

        public bool AggressiveCleanupByPID
        {
            get { return aggressiveCleanupByPID; }
            set
            {
                if (aggressiveCleanupByPID == value)
                    return;

                aggressiveCleanupByPID = value;
                NotifyPropertyChanged("AggressiveCleanupByPID");
            }
        }

        public bool PostCrashScriptEnabled
        {
            get { return postCrashScriptEnabled; }
            set
            {
                if (postCrashScriptEnabled == value)
                    return;

                postCrashScriptEnabled = value;
                NotifyPropertyChanged("PostCrashScriptEnabled");
            }
        }

        public string PostCrashScriptPath
        {
            get { return postCrashScriptPath; }
            set
            {
                if (postCrashScriptPath == value || !value.IsFile())
                    return;

                postCrashScriptPath = value;
                NotifyPropertyChanged("PostCrashScriptPath");
            }
        }

        public bool ScreenShotEnabled
        {
            get { return screenShotEnabled; }
            set
            {
                if (screenShotEnabled == value)
                    return;

                screenShotEnabled = value;
                NotifyPropertyChanged("ScreenShotEnabled");
            }
        }

        public bool AlwaysOnTopEnabled
        {
            get { return alwaysOnTopEnabled; }
            set
            {
                if (alwaysOnTopEnabled == value)
                    return;

                alwaysOnTopEnabled = value;
                NotifyPropertyChanged("AlwaysOnTopEnabled");
            }
        }

        public bool CommandLineEnabled
        {
            get { return commandLineEnabled; }
            set
            {
                if (commandLineEnabled == value)
                    return;

                commandLineEnabled = value;
                NotifyPropertyChanged("CommandLineEnabled");
            }
        }

        public string CommandLine
        {
            get { return commandLine; }
            set
            {
                if (commandLine == value)
                    return;

                commandLine = value;
                NotifyPropertyChanged("CommandLine");
            }
        }

        public bool EnvironmentVariablesEnabled
        {
            get { return environmentVariablesEnabled; }
            set
            {
                if (environmentVariablesEnabled == value)
                    return;

                environmentVariablesEnabled = value;
                NotifyPropertyChanged("EnvironmentVariablesEnabled");
            }
        }

        public string EnvironmentVariables
        {
            get { return environmentVariables.ToColonDelimitedString(); }
            set
            {
                if (environmentVariables.FromColonDelimitedString(value))
                    NotifyPropertyChanged("EnvironmentVariables");
            }
        }

        public string InitialState
        {
            get { return initialState.ToString(); }
            set
            {
                ProcessRunner.Status status;
                if (Enum.TryParse(value, true, out status) &&
                    status != initialState)
                {
                    initialState = status;
                    NotifyPropertyChanged("EnvironmentVariables");
                }
            }
        }

        [XmlIgnore]
        public Dictionary<string, string> EnvironmentVariablesDictionary
        {
            get { return environmentVariables; }
        }

        #endregion

        #region ICloneable support

        public object Clone()
        {
            ProcessOptions clone = new ProcessOptions(environmentVariables);

            clone.path = Path;
            clone.workingDirectory = WorkingDirectory;
            clone.crashedIfNotRunning = CrashedIfNotRunning;
            clone.crashedIfUnresponsive = CrashedIfUnresponsive;
            clone.doubleCheckEnabled = DoubleCheckEnabled;
            clone.doubleCheckDuration = DoubleCheckDuration;
            clone.gracePeriodEnabled = GracePeriodEnabled;
            clone.gracePeriodDuration = GracePeriodDuration;
            clone.preLaunchScriptEnabled = PreLaunchScriptEnabled;
            clone.preLaunchScriptPath = PreLaunchScriptPath;
            clone.aggressiveCleanupEnabled = AggressiveCleanupEnabled;
            clone.postCrashScriptEnabled = PostCrashScriptEnabled;
            clone.postCrashScriptPath = PostCrashScriptPath;
            clone.screenShotEnabled = ScreenShotEnabled;
            clone.alwaysOnTopEnabled = AlwaysOnTopEnabled;
            clone.commandLineEnabled = CommandLineEnabled;
            clone.commandLine = CommandLine;
            clone.environmentVariablesEnabled = EnvironmentVariablesEnabled;

            return clone;
        }

        #endregion

        #region INotifyPropertyChanged support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
