namespace oroc
{
    using liboroc;

    using System;
    using System.IO;
    using System.Windows.Forms;

    public partial class ProcessDialog : Form
    {
        public ProcessOptions Options;
        private OpenFileDialog filePicker;
        private FolderBrowserDialog folderPicker;

        public ProcessDialog()
        {
            InitializeComponent();
            Options = new ProcessOptions();

            filePicker = new OpenFileDialog();
            folderPicker = new FolderBrowserDialog();

            HandleCreated += OnProcessDialogHandleCreated;
        }

        public ProcessDialog(ProcessOptions opts)
        {
            InitializeComponent();
            Options = opts;

            HandleCreated += OnProcessDialogHandleCreated;
        }

        private void OnProcessDialogHandleCreated(object sender, EventArgs e)
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

            SetupDataBindings();
            SyncCheckedStates();
        }

        private void SetupDataBindings()
        {
            ProcessOptionPathControl.SetupDataBind(Options, nameof(Options.Path));
            ProcessOptionWorkingDirectoryControl.SetupDataBind(Options, nameof(Options.WorkingDirectory));

            ProcessOptionCrashedIfNotRunningControl.SetupDataBind(Options, nameof(Options.CrashedIfNotRunning));
            ProcessOptionCrashedIfUnresponsiveControl.SetupDataBind(Options, nameof(Options.CrashedIfUnresponsive));
            ProcessOptionDoubleCheckEnabledControl.SetupDataBind(Options, nameof(Options.DoubleCheckEnabled));
            ProcessOptionDoubleCheckDurationControl.SetupDataBind(Options, nameof(Options.DoubleCheckDuration));
            ProcessOptionGracePeriodEnabledControl.SetupDataBind(Options, nameof(Options.GracePeriodEnabled));
            ProcessOptionGracePeriodDurationControl.SetupDataBind(Options, nameof(Options.GracePeriodDuration));

            ProcessOptionPreLaunchScriptEnabledControl.SetupDataBind(Options, nameof(Options.PreLaunchScriptEnabled));
            ProcessOptionPreLaunchScriptPathControl.SetupDataBind(Options, nameof(Options.PreLaunchScriptPath));

            ProcessOptionAggressiveCleanupEnabledControl.SetupDataBind(Options, nameof(Options.AggressiveCleanupEnabled));
            ProcessOptionPostCrashScriptEnabledControl.SetupDataBind(Options, nameof(Options.PostCrashScriptEnabled));
            ProcessOptionPostCrashScriptPathControl.SetupDataBind(Options, nameof(Options.PostCrashScriptPath));

            ProcessOptionScreenshotEnabledControl.SetupDataBind(Options, nameof(Options.ScreenShotEnabled));
            ProcessOptionAlwaysOnTopEnabledControl.SetupDataBind(Options, nameof(Options.AlwaysOnTopEnabled));
            ProcessOptionCommandLineEnabledControl.SetupDataBind(Options, nameof(Options.CommandLineEnabled));
            ProcessOptionCommandLineControl.SetupDataBind(Options, nameof(Options.CommandLine));
            ProcessOptionEnvironmentVariablesEnabledControl.SetupDataBind(Options, nameof(Options.EnvironmentVariablesEnabled));
            ProcessOptionEnvironmentVariablesControl.SetupDataBind(Options, nameof(Options.EnvironmentVariables));
        }

        private void OnProcessOptionsSaveButtonClick(object sender, EventArgs e)
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

        private void OnProcessOptionsCancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnStartupStateRadioGroupCheckedChanged(object sender, EventArgs e)
        {
            SyncCheckedStates();
        }

        private void SyncCheckedStates()
        {
            if (StartupStateStoppedControl.Checked)
                Options.InitialStateEnumValue = ProcessRunner.Status.Stopped;
            else if (StartupStateRunningControl.Checked)
                Options.InitialStateEnumValue = ProcessRunner.Status.Running;
            else if (StartupStateDisabledControl.Checked)
                Options.InitialStateEnumValue = ProcessRunner.Status.Disabled;

            OnProcessOptionDoubleCheckEnabledControlCheckedChanged(this, null);
            OnProcessOptionGracePeriodEnabledControlCheckedChanged(this, null);
            OnProcessOptionPreLaunchScriptEnabledControlCheckedChanged(this, null);
            OnProcessOptionPostCrashScriptEnabledControlCheckedChanged(this, null);
            OnProcessOptionCommandLineEnabledControlCheckedChanged(this, null);
            OnProcessOptionEnvironmentVariablesEnabledControlCheckedChanged(this, null);
        }

        private void OnProcessOptionDoubleCheckEnabledControlCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = ProcessOptionDoubleCheckEnabledControl;

            if (ProcessOptionDoubleCheckDurationControl.Enabled != checkbox.Checked)
                ProcessOptionDoubleCheckDurationControl.Enabled = checkbox.Checked;
        }

        private void OnProcessOptionGracePeriodEnabledControlCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = ProcessOptionGracePeriodEnabledControl;

            if (ProcessOptionGracePeriodDurationControl.Enabled != checkbox.Checked)
                ProcessOptionGracePeriodDurationControl.Enabled = checkbox.Checked;
        }

        private void OnProcessOptionPreLaunchScriptEnabledControlCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = ProcessOptionPreLaunchScriptEnabledControl;

            if (ProcessOptionPreLaunchScriptPathControl.Enabled != checkbox.Checked)
                ProcessOptionPreLaunchScriptPathControl.Enabled = checkbox.Checked;

            if (ProcessOptionPreLaunchScriptButton.Enabled != checkbox.Checked)
                ProcessOptionPreLaunchScriptButton.Enabled = checkbox.Checked;
        }

        private void OnProcessOptionPostCrashScriptEnabledControlCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = ProcessOptionPostCrashScriptEnabledControl;

            if (ProcessOptionPostCrashScriptPathControl.Enabled != checkbox.Checked)
                ProcessOptionPostCrashScriptPathControl.Enabled = checkbox.Checked;

            if (ProcessOptionPostCrashScriptButton.Enabled != checkbox.Checked)
                ProcessOptionPostCrashScriptButton.Enabled = checkbox.Checked;
        }

        private void OnProcessOptionCommandLineEnabledControlCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = ProcessOptionCommandLineEnabledControl;

            if (ProcessOptionCommandLineControl.Enabled != checkbox.Checked)
                ProcessOptionCommandLineControl.Enabled = checkbox.Checked;
        }

        private void OnProcessOptionEnvironmentVariablesEnabledControlCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = ProcessOptionEnvironmentVariablesEnabledControl;

            if (ProcessOptionEnvironmentVariablesControl.Enabled != checkbox.Checked)
                ProcessOptionEnvironmentVariablesControl.Enabled = checkbox.Checked;
        }

        private void OnSelectWorkingDirectoryClick(object sender, EventArgs e)
        {
            folderPicker.Description = "Please select a folder to continue";

            if (folderPicker.ShowDialog() == DialogResult.OK)
            {
                Options.WorkingDirectory = folderPicker.SelectedPath;
            }
        }

        private void OnSelectExecutablePathClick(object sender, EventArgs e)
        {
            filePicker.Filter = "Windows Executable (*.exe)|*.exe";
            filePicker.Title = "Please select an executable to continue";

            if (filePicker.ShowDialog() == DialogResult.OK)
            {
                Options.Path = filePicker.FileName;
                Options.WorkingDirectory = Path.GetDirectoryName(Options.Path);
            }
        }

        private void DisposeAddedComponents()
        {
            folderPicker?.Dispose();
            filePicker?.Dispose();

            folderPicker = null;
            filePicker = null;
        }
    }
}
