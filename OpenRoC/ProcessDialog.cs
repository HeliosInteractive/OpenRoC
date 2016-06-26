namespace oroc
{
    using System.Windows.Forms;

    public partial class ProcessDialog : Form
    {
        public ProcessOptions Options;

        public ProcessDialog()
        {
            InitializeComponent();
            Options = new ProcessOptions();
            SetupDataBindings();
        }

        public ProcessDialog(ProcessOptions opts)
        {
            InitializeComponent();
            Options = opts;
            SetupDataBindings();
        }

        private void SetupDataBindings()
        {
            ProcessExecutablePathTextBox.DataBindings.Add(new Binding("Text", Options, "Path"));
            ProcessWorkingDirectoryTextBox.DataBindings.Add(new Binding("Text", Options, "WorkingDirectory"));

            AssumeCrashIfNotRunningCheckBox.DataBindings.Add(new Binding("Checked", Options, "CrashedIfNotRunning"));
            AssumeCrashIfUnresponsiveCheckBox.DataBindings.Add(new Binding("Checked", Options, "CrashedIfUnresponsive"));
            PostCrashCheckEnabledCheckBox.DataBindings.Add(new Binding("Checked", Options, "DoubleCheckEnabled"));
            PostCrashCheckEnabledTextBox.DataBindings.Add(new Binding("Text", Options, "DoubleCheckDuration"));
            PostCrashWaitEnabledCheckBox.DataBindings.Add(new Binding("Checked", Options, "GracePeriodEnabled"));
            PostCrashWaitEnabledTextBox.DataBindings.Add(new Binding("Text", Options, "GracePeriodDuration"));

            ProcessPreStartCommandEnabledCheckBox.DataBindings.Add(new Binding("Checked", Options, "PreLaunchScriptEnabled"));
            ProcessPreStartCommandTextBox.DataBindings.Add(new Binding("Text", Options, "PreLaunchScriptPath"));

            ProcessPostCrashCleanupEnabledCheckBox.DataBindings.Add(new Binding("Checked", Options, "AggressiveCleanupEnabled"));
            ProcessPostCrashCommandEnabledCheckBox.DataBindings.Add(new Binding("Checked", Options, "PostCrashScriptEnabled"));
            ProcessPostCrashCommandTextBox.DataBindings.Add(new Binding("Text", Options, "PostCrashScriptPath"));

            ScreenshotEnabledOnCrashCheckBox.DataBindings.Add(new Binding("Checked", Options, "ScreenShotEnabled"));
            KeepAlwaysOnTopCheckBox.DataBindings.Add(new Binding("Checked", Options, "AlwaysOnTopEnabled"));
            PassCommandLineCheckBox.DataBindings.Add(new Binding("Checked", Options, "CommandLineEnabled"));
            PassCommandLineTextBox.DataBindings.Add(new Binding("Text", Options, "CommandLine"));
            MergeVariablesCheckBox.DataBindings.Add(new Binding("Checked", Options, "EnvironmentVariablesEnabled"));
            MergeVariablesTextBox.DataBindings.Add(new Binding("Text", Options, "EnvironmentVariables"));
        }
    }
}
