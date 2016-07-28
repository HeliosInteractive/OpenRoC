namespace liboroc
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
            SetDefaults();
            environmentVariables = new Dictionary<string, string>();
        }

        public ProcessOptions(Dictionary<string, string> variables)
        {
            SetDefaults();
            environmentVariables = variables;
        }

        void SetDefaults()
        {
            path = string.Empty;
            workingDirectory = string.Empty;
            crashedIfNotRunning = true;
            crashedIfUnresponsive = true;
            doubleCheckEnabled = false;
            doubleCheckDuration = 0;
            gracePeriodEnabled = false;
            gracePeriodDuration = 0;
            preLaunchScriptEnabled = false;
            preLaunchScriptPath = string.Empty;
            aggressiveCleanupEnabled = true;
            postCrashScriptEnabled = false;
            postCrashScriptPath = string.Empty;
            screenShotEnabled = false;
            alwaysOnTopEnabled = false;
            commandLineEnabled = false;
            commandLine = string.Empty;
            environmentVariablesEnabled = false;

            if (environmentVariables != null)
                environmentVariables.Clear();

            initialState = ProcessRunner.Status.Stopped;
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
                NotifyPropertyChanged(nameof(Path));
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
                NotifyPropertyChanged(nameof(WorkingDirectory));
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
                NotifyPropertyChanged(nameof(CrashedIfNotRunning));
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
                NotifyPropertyChanged(nameof(CrashedIfUnresponsive));
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
                NotifyPropertyChanged(nameof(DoubleCheckEnabled));
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
                NotifyPropertyChanged(nameof(DoubleCheckDuration));
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
                NotifyPropertyChanged(nameof(GracePeriodEnabled));
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
                NotifyPropertyChanged(nameof(GracePeriodDuration));
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
                NotifyPropertyChanged(nameof(PreLaunchScriptEnabled));
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
                NotifyPropertyChanged(nameof(PreLaunchScriptPath));
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
                NotifyPropertyChanged(nameof(AggressiveCleanupEnabled));
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
                NotifyPropertyChanged(nameof(PostCrashScriptEnabled));
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
                NotifyPropertyChanged(nameof(PostCrashScriptPath));
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
                NotifyPropertyChanged(nameof(ScreenShotEnabled));
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
                NotifyPropertyChanged(nameof(AlwaysOnTopEnabled));
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
                NotifyPropertyChanged(nameof(CommandLineEnabled));
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
                NotifyPropertyChanged(nameof(CommandLine));
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
                NotifyPropertyChanged(nameof(EnvironmentVariablesEnabled));
            }
        }

        public string EnvironmentVariables
        {
            get { return environmentVariables.ToColonDelimitedString(); }
            set
            {
                if (environmentVariables.FromColonDelimitedString(value))
                    NotifyPropertyChanged(nameof(EnvironmentVariables));
            }
        }

        public string InitialState
        {
            get { return initialState.ToString(); }
            set
            {
                ProcessRunner.Status status;
                if (Enum.TryParse(value, true, out status))
                {
                    if (status == initialState)
                        return;

                    initialState = status;
                    NotifyPropertyChanged(nameof(InitialState));
                }
                else
                {
                    initialState = ProcessRunner.Status.Invalid;
                    NotifyPropertyChanged(nameof(InitialState));
                }
            }
        }

        [XmlIgnore]
        public ProcessRunner.Status InitialStateEnumValue
        {
            get { return initialState; }
            set
            {
                if (value == initialState)
                    return;

                initialState = value;
                NotifyPropertyChanged(nameof(InitialState));
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
            clone.initialState = InitialStateEnumValue;

            return clone;
        }

        #endregion

        #region INotifyPropertyChanged support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
