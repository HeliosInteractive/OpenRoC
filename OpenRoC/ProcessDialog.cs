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

            HandleCreated += OnProcessDialogHandleCreated;
        }

        public ProcessDialog(ProcessOptions opts)
        {
            InitializeComponent();
            Options = opts;
            SetupDataBindings();

            HandleCreated += OnProcessDialogHandleCreated;
        }

        private void OnProcessDialogHandleCreated(object sender, System.EventArgs e)
        {
            if (!(Owner is MainDialog))
            {
                Log.e("Owner of Process Window is not the right type.");
                return;
            }

            MainDialog main_dialog = Owner as MainDialog;

            main_dialog.SetStatusBarText(ProcessOptionPathControl, "Path to process executable. Only executables (*.exe) are valid.");
            main_dialog.SetStatusBarText(ProcessOptionWorkingDirectoryControl, "Working directory of the chosen process executable");

            main_dialog.SetStatusBarText(ProcessOptionCrashedIfNotRunningControl, "Assume process is crashed if it's not running (no effect when monitoring is disabled).");
            main_dialog.SetStatusBarText(ProcessOptionCrashedIfUnresponsiveControl, "Assume crash if main Window is unresponsive (no effect when monitoring is disabled).");
            main_dialog.SetStatusBarText(ProcessOptionDoubleCheckEnabledControl, "Enable crash double check.");
            main_dialog.SetStatusBarText(ProcessOptionDoubleCheckDurationControl, "Crash double check duration. After a crash, double check before killing the process");
            main_dialog.SetStatusBarText(ProcessOptionGracePeriodEnabledControl, "Enable a grace period between relaunches of the process.");
            main_dialog.SetStatusBarText(ProcessOptionGracePeriodDurationControl, "Grace period duration. After a crash, a restart happens when grace period ends.");

            main_dialog.SetStatusBarText(ProcessOptionPreLaunchScriptEnabledControl, "Enable before launch script execution.");
            main_dialog.SetStatusBarText(ProcessOptionPreLaunchScriptPathControl, "Execute and wait for this script before starting the process.");

            main_dialog.SetStatusBarText(ProcessOptionAggressiveCleanupEnabledControl, "Enable aggressive cleanup after process crashes.");
            main_dialog.SetStatusBarText(ProcessOptionAggressiveCleanupByNameControl, "Perform aggressive cleanup (taskkill.exe) by process name.");
            main_dialog.SetStatusBarText(ProcessOptionAggressiveCleanupByPIDControl, "Perform aggressive cleanup (taskkill.exe) by process PID.");
            main_dialog.SetStatusBarText(ProcessOptionPostCrashScriptEnabledControl, "Enable after crash script execution.");
            main_dialog.SetStatusBarText(ProcessOptionPostCrashScriptPathControl, "Execute and wait for this script after process crashed.");

            main_dialog.SetStatusBarText(ProcessOptionScreenshotEnabledControl, "Take a screen-shot of the main display when process crashes.");
            main_dialog.SetStatusBarText(ProcessOptionAlwaysOnTopEnabledControl, "Keep main Window on-top (aggressive, conflicts with other always-on-top Windows).");
            main_dialog.SetStatusBarText(ProcessOptionCommandLineEnabledControl, "Enable passing command line to the process.");
            main_dialog.SetStatusBarText(ProcessOptionCommandLineControl, "Command line to pass to the process executable.");
            main_dialog.SetStatusBarText(ProcessOptionEnvironmentVariablesEnabledControl, "Enable merging environment variables.");
            main_dialog.SetStatusBarText(ProcessOptionEnvironmentVariablesControl, "Environment variable list. Format: var1=val1;var2=val2;...");

            main_dialog.SetStatusBarText(StartupStateStoppedControl, "Stop the process when it is added to the process list for the first time.");
            main_dialog.SetStatusBarText(StartupStateRunningControl, "Run the process when it is added to the process list for the first time.");
            main_dialog.SetStatusBarText(StartupStateDisabledControl, "Do not monitor the process when it is added to the process list for the first time.");
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

            SyncStartupStateRadioGroup();
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
            SyncStartupStateRadioGroup();
        }

        private void SyncStartupStateRadioGroup()
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
