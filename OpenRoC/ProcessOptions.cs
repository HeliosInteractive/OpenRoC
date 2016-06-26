namespace oroc
{
    public class ProcessOptions : INotifyPropertyChangedAuto
    {
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
        private string environmentVariables;

        public string Path
        {
            get { return path; }
            set { path = value; NotifyPropertyChanged(); }
        }

        public string WorkingDirectory
        {
            get { return workingDirectory; }
            set { workingDirectory = value; NotifyPropertyChanged(); }
        }

        public bool CrashedIfNotRunning
        {
            get { return crashedIfNotRunning; }
            set { crashedIfNotRunning = value; NotifyPropertyChanged(); }
        }

        public bool CrashedIfUnresponsive
        {
            get { return crashedIfUnresponsive; }
            set { crashedIfUnresponsive = value; NotifyPropertyChanged(); }
        }

        public bool DoubleCheckEnabled
        {
            get { return doubleCheckEnabled; }
            set { doubleCheckEnabled = value; NotifyPropertyChanged(); }
        }

        public uint DoubleCheckDuration
        {
            get { return doubleCheckDuration; }
            set { doubleCheckDuration = value; NotifyPropertyChanged(); }
        }

        public bool GracePeriodEnabled
        {
            get { return gracePeriodEnabled; }
            set { gracePeriodEnabled = value; NotifyPropertyChanged(); }
        }

        public uint GracePeriodDuration
        {
            get { return gracePeriodDuration; }
            set { gracePeriodDuration = value; NotifyPropertyChanged(); }
        }

        public bool PreLaunchScriptEnabled
        {
            get { return preLaunchScriptEnabled; }
            set { preLaunchScriptEnabled = value; NotifyPropertyChanged(); }
        }

        public string PreLaunchScriptPath
        {
            get { return preLaunchScriptPath; }
            set { preLaunchScriptPath = value; NotifyPropertyChanged(); }
        }

        public bool AggressiveCleanupEnabled
        {
            get { return aggressiveCleanupEnabled; }
            set { aggressiveCleanupEnabled = value; NotifyPropertyChanged(); }
        }

        public bool PostCrashScriptEnabled
        {
            get { return postCrashScriptEnabled; }
            set { postCrashScriptEnabled = value; NotifyPropertyChanged(); }
        }

        public string PostCrashScriptPath
        {
            get { return postCrashScriptPath; }
            set { postCrashScriptPath = value; NotifyPropertyChanged(); }
        }

        public bool ScreenShotEnabled
        {
            get { return screenShotEnabled; }
            set { screenShotEnabled = value; NotifyPropertyChanged(); }
        }

        public bool AlwaysOnTopEnabled
        {
            get { return alwaysOnTopEnabled; }
            set { alwaysOnTopEnabled = value; NotifyPropertyChanged(); }
        }

        public bool CommandLineEnabled
        {
            get { return commandLineEnabled; }
            set { commandLineEnabled = value; NotifyPropertyChanged(); }
        }

        public string CommandLine
        {
            get { return commandLine; }
            set { commandLine = value; NotifyPropertyChanged(); }
        }

        public bool EnvironmentVariablesEnabled
        {
            get { return environmentVariablesEnabled; }
            set { environmentVariablesEnabled = value; NotifyPropertyChanged(); }
        }

        public string EnvironmentVariables
        {
            get { return environmentVariables; }
            set { environmentVariables = value; NotifyPropertyChanged(); }
        }
    }
}
