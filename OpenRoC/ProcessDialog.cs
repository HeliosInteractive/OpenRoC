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
            ProcessOptionPathControl.DataBindings.Add(new Binding("Text", Options, "Path"));
            ProcessOptionWorkingDirectoryControl.DataBindings.Add(new Binding("Text", Options, "WorkingDirectory"));

            ProcessOptionCrashedIfNotRunningControl.DataBindings.Add(new Binding("Checked", Options, "CrashedIfNotRunning"));
            ProcessOptionCrashedIfUnresponsiveControl.DataBindings.Add(new Binding("Checked", Options, "CrashedIfUnresponsive"));
            ProcessOptionDoubleCheckEnabledControl.DataBindings.Add(new Binding("Checked", Options, "DoubleCheckEnabled"));
            ProcessOptionDoubleCheckDurationControl.DataBindings.Add(new Binding("Text", Options, "DoubleCheckDuration"));
            ProcessOptionGracePeriodEnabledControl.DataBindings.Add(new Binding("Checked", Options, "GracePeriodEnabled"));
            ProcessOptionGracePeriodDurationControl.DataBindings.Add(new Binding("Text", Options, "GracePeriodDuration"));

            ProcessOptionPreLaunchScriptEnabledControl.DataBindings.Add(new Binding("Checked", Options, "PreLaunchScriptEnabled"));
            ProcessOptionPreLaunchScriptPathControl.DataBindings.Add(new Binding("Text", Options, "PreLaunchScriptPath"));

            ProcessOptionAggressiveCleanupEnabledControl.DataBindings.Add(new Binding("Checked", Options, "AggressiveCleanupEnabled"));
            ProcessOptionAggressiveCleanupByNameControl.DataBindings.Add(new Binding("Checked", Options, "AggressiveCleanupByName"));
            ProcessOptionAggressiveCleanupByPIDControl.DataBindings.Add(new Binding("Checked", Options, "AggressiveCleanupByPID"));
            ProcessOptionPostCrashScriptEnabledControl.DataBindings.Add(new Binding("Checked", Options, "PostCrashScriptEnabled"));
            ProcessOptionPostCrashScriptPathControl.DataBindings.Add(new Binding("Text", Options, "PostCrashScriptPath"));

            ProcessOptionScreenshotEnabledControl.DataBindings.Add(new Binding("Checked", Options, "ScreenShotEnabled"));
            ProcessOptionAlwaysOnTopEnabledControl.DataBindings.Add(new Binding("Checked", Options, "AlwaysOnTopEnabled"));
            ProcessOptionCommandLineEnabledControl.DataBindings.Add(new Binding("Checked", Options, "CommandLineEnabled"));
            ProcessOptionCommandLineControl.DataBindings.Add(new Binding("Text", Options, "CommandLine"));
            ProcessOptionEnvironmentVariablesEnabledControl.DataBindings.Add(new Binding("Checked", Options, "EnvironmentVariablesEnabled"));
            ProcessOptionEnvironmentVariablesControl.DataBindings.Add(new Binding("Text", Options, "EnvironmentVariables"));
        }

        private void OnProcessOptionsSaveButtonClick(object sender, System.EventArgs e)
        {
            MainDialog main = Owner as MainDialog;

            if (main != null)
            {
                if (main.ProcessManager.Contains(Options.Path))
                    main.ProcessManager.Swap(Options);
                else
                    main.ProcessManager.Add(Options);

                main.UpdateProcessList();
            }

            Close();
        }

        private void OnProcessOptionsCancelButtonClick(object sender, System.EventArgs e)
        {
            Close();
        }

        private void OnStartupStateRadioGroupCheckedChanged(object sender, System.EventArgs e)
        {
            if (StartupStateStoppedControl.Checked)
                Options.InitialStateEnumValue = ProcessRunner.Status.Stopped;
            else if (StartupStateRunningControl.Checked)
                Options.InitialStateEnumValue = ProcessRunner.Status.Running;
            else if (StartupStateDisabledControl.Checked)
                Options.InitialStateEnumValue = ProcessRunner.Status.Disabled;
        }
    }
}
