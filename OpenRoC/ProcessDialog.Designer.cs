namespace oroc
{
    partial class ProcessDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessDialog));
            this.MonitorThisProcessGroup = new System.Windows.Forms.GroupBox();
            this.SelectWorkingDirectory = new System.Windows.Forms.Button();
            this.SelectExecutablePath = new System.Windows.Forms.Button();
            this.ProcessWorkingDirectoryLabel = new System.Windows.Forms.Label();
            this.ProcessExecutablePathLabel = new System.Windows.Forms.Label();
            this.ProcessOptionWorkingDirectoryControl = new System.Windows.Forms.TextBox();
            this.ProcessOptionPathControl = new System.Windows.Forms.TextBox();
            this.ProcessCrashAssumptionsGroup = new System.Windows.Forms.GroupBox();
            this.PostCrashWaitEnabledLabel = new System.Windows.Forms.Label();
            this.PostCrashCheckEnabledLabel = new System.Windows.Forms.Label();
            this.ProcessOptionGracePeriodDurationControl = new System.Windows.Forms.TextBox();
            this.ProcessOptionDoubleCheckDurationControl = new System.Windows.Forms.TextBox();
            this.ProcessOptionGracePeriodEnabledControl = new System.Windows.Forms.CheckBox();
            this.ProcessOptionDoubleCheckEnabledControl = new System.Windows.Forms.CheckBox();
            this.ProcessOptionCrashedIfUnresponsiveControl = new System.Windows.Forms.CheckBox();
            this.ProcessOptionCrashedIfNotRunningControl = new System.Windows.Forms.CheckBox();
            this.ProcessPreStartGroup = new System.Windows.Forms.GroupBox();
            this.ProcessPreStartCommandButton = new System.Windows.Forms.Button();
            this.ProcessOptionPreLaunchScriptPathControl = new System.Windows.Forms.TextBox();
            this.ProcessOptionPreLaunchScriptEnabledControl = new System.Windows.Forms.CheckBox();
            this.PostCrashGroup = new System.Windows.Forms.GroupBox();
            this.ProcessOptionAggressiveCleanupByPIDControl = new System.Windows.Forms.CheckBox();
            this.ProcessOptionAggressiveCleanupByNameControl = new System.Windows.Forms.CheckBox();
            this.ProcessOptionAggressiveCleanupEnabledControl = new System.Windows.Forms.CheckBox();
            this.ProcessPostCrashCommandButton = new System.Windows.Forms.Button();
            this.ProcessOptionPostCrashScriptPathControl = new System.Windows.Forms.TextBox();
            this.ProcessOptionPostCrashScriptEnabledControl = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.StartupStateDisabledControl = new System.Windows.Forms.RadioButton();
            this.StartupStateRunningControl = new System.Windows.Forms.RadioButton();
            this.StartupStateStoppedControl = new System.Windows.Forms.RadioButton();
            this.StartupStateLabel = new System.Windows.Forms.Label();
            this.ProcessOptionsCancelButton = new System.Windows.Forms.Button();
            this.ProcessOptionsSaveButton = new System.Windows.Forms.Button();
            this.EnvironmentVariableFormatLabel = new System.Windows.Forms.Label();
            this.OpenScreenshotDirectoryButton = new System.Windows.Forms.Button();
            this.ProcessOptionEnvironmentVariablesControl = new System.Windows.Forms.TextBox();
            this.ProcessOptionEnvironmentVariablesEnabledControl = new System.Windows.Forms.CheckBox();
            this.ProcessOptionCommandLineControl = new System.Windows.Forms.TextBox();
            this.ProcessOptionCommandLineEnabledControl = new System.Windows.Forms.CheckBox();
            this.ProcessOptionAlwaysOnTopEnabledControl = new System.Windows.Forms.CheckBox();
            this.ProcessOptionScreenshotEnabledControl = new System.Windows.Forms.CheckBox();
            this.MonitorThisProcessGroup.SuspendLayout();
            this.ProcessCrashAssumptionsGroup.SuspendLayout();
            this.ProcessPreStartGroup.SuspendLayout();
            this.PostCrashGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MonitorThisProcessGroup
            // 
            this.MonitorThisProcessGroup.Controls.Add(this.SelectWorkingDirectory);
            this.MonitorThisProcessGroup.Controls.Add(this.SelectExecutablePath);
            this.MonitorThisProcessGroup.Controls.Add(this.ProcessWorkingDirectoryLabel);
            this.MonitorThisProcessGroup.Controls.Add(this.ProcessExecutablePathLabel);
            this.MonitorThisProcessGroup.Controls.Add(this.ProcessOptionWorkingDirectoryControl);
            this.MonitorThisProcessGroup.Controls.Add(this.ProcessOptionPathControl);
            this.MonitorThisProcessGroup.Location = new System.Drawing.Point(12, 6);
            this.MonitorThisProcessGroup.Name = "MonitorThisProcessGroup";
            this.MonitorThisProcessGroup.Size = new System.Drawing.Size(460, 116);
            this.MonitorThisProcessGroup.TabIndex = 0;
            this.MonitorThisProcessGroup.TabStop = false;
            this.MonitorThisProcessGroup.Text = "Monitor this process";
            // 
            // SelectWorkingDirectory
            // 
            this.SelectWorkingDirectory.Location = new System.Drawing.Point(156, 79);
            this.SelectWorkingDirectory.Name = "SelectWorkingDirectory";
            this.SelectWorkingDirectory.Size = new System.Drawing.Size(148, 23);
            this.SelectWorkingDirectory.TabIndex = 5;
            this.SelectWorkingDirectory.Text = "Select Working Directory";
            this.SelectWorkingDirectory.UseVisualStyleBackColor = true;
            // 
            // SelectExecutablePath
            // 
            this.SelectExecutablePath.Location = new System.Drawing.Point(312, 79);
            this.SelectExecutablePath.Name = "SelectExecutablePath";
            this.SelectExecutablePath.Size = new System.Drawing.Size(140, 23);
            this.SelectExecutablePath.TabIndex = 4;
            this.SelectExecutablePath.Text = "Select Executable Path";
            this.SelectExecutablePath.UseVisualStyleBackColor = true;
            // 
            // ProcessWorkingDirectoryLabel
            // 
            this.ProcessWorkingDirectoryLabel.AutoSize = true;
            this.ProcessWorkingDirectoryLabel.Location = new System.Drawing.Point(7, 53);
            this.ProcessWorkingDirectoryLabel.Name = "ProcessWorkingDirectoryLabel";
            this.ProcessWorkingDirectoryLabel.Size = new System.Drawing.Size(92, 13);
            this.ProcessWorkingDirectoryLabel.TabIndex = 3;
            this.ProcessWorkingDirectoryLabel.Text = "Working Directory";
            // 
            // ProcessExecutablePathLabel
            // 
            this.ProcessExecutablePathLabel.AutoSize = true;
            this.ProcessExecutablePathLabel.Location = new System.Drawing.Point(7, 24);
            this.ProcessExecutablePathLabel.Name = "ProcessExecutablePathLabel";
            this.ProcessExecutablePathLabel.Size = new System.Drawing.Size(85, 13);
            this.ProcessExecutablePathLabel.TabIndex = 2;
            this.ProcessExecutablePathLabel.Text = "Executable Path";
            // 
            // ProcessOptionWorkingDirectoryControl
            // 
            this.ProcessOptionWorkingDirectoryControl.Location = new System.Drawing.Point(102, 50);
            this.ProcessOptionWorkingDirectoryControl.Name = "ProcessOptionWorkingDirectoryControl";
            this.ProcessOptionWorkingDirectoryControl.Size = new System.Drawing.Size(349, 20);
            this.ProcessOptionWorkingDirectoryControl.TabIndex = 1;
            // 
            // ProcessOptionPathControl
            // 
            this.ProcessOptionPathControl.Location = new System.Drawing.Point(102, 20);
            this.ProcessOptionPathControl.Name = "ProcessOptionPathControl";
            this.ProcessOptionPathControl.Size = new System.Drawing.Size(349, 20);
            this.ProcessOptionPathControl.TabIndex = 0;
            // 
            // ProcessCrashAssumptionsGroup
            // 
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.PostCrashWaitEnabledLabel);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.PostCrashCheckEnabledLabel);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.ProcessOptionGracePeriodDurationControl);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.ProcessOptionDoubleCheckDurationControl);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.ProcessOptionGracePeriodEnabledControl);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.ProcessOptionDoubleCheckEnabledControl);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.ProcessOptionCrashedIfUnresponsiveControl);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.ProcessOptionCrashedIfNotRunningControl);
            this.ProcessCrashAssumptionsGroup.Location = new System.Drawing.Point(12, 127);
            this.ProcessCrashAssumptionsGroup.Name = "ProcessCrashAssumptionsGroup";
            this.ProcessCrashAssumptionsGroup.Size = new System.Drawing.Size(460, 126);
            this.ProcessCrashAssumptionsGroup.TabIndex = 1;
            this.ProcessCrashAssumptionsGroup.TabStop = false;
            this.ProcessCrashAssumptionsGroup.Text = "Assume process has crashed or hung when";
            // 
            // PostCrashWaitEnabledLabel
            // 
            this.PostCrashWaitEnabledLabel.AutoSize = true;
            this.PostCrashWaitEnabledLabel.Location = new System.Drawing.Point(138, 93);
            this.PostCrashWaitEnabledLabel.Name = "PostCrashWaitEnabledLabel";
            this.PostCrashWaitEnabledLabel.Size = new System.Drawing.Size(247, 13);
            this.PostCrashWaitEnabledLabel.TabIndex = 7;
            this.PostCrashWaitEnabledLabel.Text = "seconds after a crash before attempting a relaunch";
            // 
            // PostCrashCheckEnabledLabel
            // 
            this.PostCrashCheckEnabledLabel.AutoSize = true;
            this.PostCrashCheckEnabledLabel.Location = new System.Drawing.Point(138, 70);
            this.PostCrashCheckEnabledLabel.Name = "PostCrashCheckEnabledLabel";
            this.PostCrashCheckEnabledLabel.Size = new System.Drawing.Size(224, 13);
            this.PostCrashCheckEnabledLabel.TabIndex = 6;
            this.PostCrashCheckEnabledLabel.Text = "seconds and double check if crashed or hung";
            // 
            // ProcessOptionGracePeriodDurationControl
            // 
            this.ProcessOptionGracePeriodDurationControl.Location = new System.Drawing.Point(57, 91);
            this.ProcessOptionGracePeriodDurationControl.Name = "ProcessOptionGracePeriodDurationControl";
            this.ProcessOptionGracePeriodDurationControl.Size = new System.Drawing.Size(75, 20);
            this.ProcessOptionGracePeriodDurationControl.TabIndex = 5;
            // 
            // ProcessOptionDoubleCheckDurationControl
            // 
            this.ProcessOptionDoubleCheckDurationControl.Location = new System.Drawing.Point(57, 67);
            this.ProcessOptionDoubleCheckDurationControl.Name = "ProcessOptionDoubleCheckDurationControl";
            this.ProcessOptionDoubleCheckDurationControl.Size = new System.Drawing.Size(75, 20);
            this.ProcessOptionDoubleCheckDurationControl.TabIndex = 3;
            // 
            // ProcessOptionGracePeriodEnabledControl
            // 
            this.ProcessOptionGracePeriodEnabledControl.AutoSize = true;
            this.ProcessOptionGracePeriodEnabledControl.Location = new System.Drawing.Point(10, 92);
            this.ProcessOptionGracePeriodEnabledControl.Name = "ProcessOptionGracePeriodEnabledControl";
            this.ProcessOptionGracePeriodEnabledControl.Size = new System.Drawing.Size(48, 17);
            this.ProcessOptionGracePeriodEnabledControl.TabIndex = 4;
            this.ProcessOptionGracePeriodEnabledControl.Text = "Wait";
            this.ProcessOptionGracePeriodEnabledControl.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionDoubleCheckEnabledControl
            // 
            this.ProcessOptionDoubleCheckEnabledControl.AutoSize = true;
            this.ProcessOptionDoubleCheckEnabledControl.Location = new System.Drawing.Point(10, 69);
            this.ProcessOptionDoubleCheckEnabledControl.Name = "ProcessOptionDoubleCheckEnabledControl";
            this.ProcessOptionDoubleCheckEnabledControl.Size = new System.Drawing.Size(48, 17);
            this.ProcessOptionDoubleCheckEnabledControl.TabIndex = 2;
            this.ProcessOptionDoubleCheckEnabledControl.Text = "Wait";
            this.ProcessOptionDoubleCheckEnabledControl.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionCrashedIfUnresponsiveControl
            // 
            this.ProcessOptionCrashedIfUnresponsiveControl.AutoSize = true;
            this.ProcessOptionCrashedIfUnresponsiveControl.Location = new System.Drawing.Point(10, 46);
            this.ProcessOptionCrashedIfUnresponsiveControl.Name = "ProcessOptionCrashedIfUnresponsiveControl";
            this.ProcessOptionCrashedIfUnresponsiveControl.Size = new System.Drawing.Size(332, 17);
            this.ProcessOptionCrashedIfUnresponsiveControl.TabIndex = 1;
            this.ProcessOptionCrashedIfUnresponsiveControl.Text = "It\'s not responding to Windows messages (unresponsive window)";
            this.ProcessOptionCrashedIfUnresponsiveControl.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionCrashedIfNotRunningControl
            // 
            this.ProcessOptionCrashedIfNotRunningControl.AutoSize = true;
            this.ProcessOptionCrashedIfNotRunningControl.Location = new System.Drawing.Point(10, 23);
            this.ProcessOptionCrashedIfNotRunningControl.Name = "ProcessOptionCrashedIfNotRunningControl";
            this.ProcessOptionCrashedIfNotRunningControl.Size = new System.Drawing.Size(232, 17);
            this.ProcessOptionCrashedIfNotRunningControl.TabIndex = 0;
            this.ProcessOptionCrashedIfNotRunningControl.Text = "It\'s not running (e.g after a Computer restart)";
            this.ProcessOptionCrashedIfNotRunningControl.UseVisualStyleBackColor = true;
            // 
            // ProcessPreStartGroup
            // 
            this.ProcessPreStartGroup.Controls.Add(this.ProcessPreStartCommandButton);
            this.ProcessPreStartGroup.Controls.Add(this.ProcessOptionPreLaunchScriptPathControl);
            this.ProcessPreStartGroup.Controls.Add(this.ProcessOptionPreLaunchScriptEnabledControl);
            this.ProcessPreStartGroup.Location = new System.Drawing.Point(12, 258);
            this.ProcessPreStartGroup.Name = "ProcessPreStartGroup";
            this.ProcessPreStartGroup.Size = new System.Drawing.Size(460, 82);
            this.ProcessPreStartGroup.TabIndex = 2;
            this.ProcessPreStartGroup.TabStop = false;
            this.ProcessPreStartGroup.Text = "Before process starts";
            // 
            // ProcessPreStartCommandButton
            // 
            this.ProcessPreStartCommandButton.Location = new System.Drawing.Point(417, 46);
            this.ProcessPreStartCommandButton.Name = "ProcessPreStartCommandButton";
            this.ProcessPreStartCommandButton.Size = new System.Drawing.Size(34, 23);
            this.ProcessPreStartCommandButton.TabIndex = 2;
            this.ProcessPreStartCommandButton.Text = "...";
            this.ProcessPreStartCommandButton.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionPreLaunchScriptPathControl
            // 
            this.ProcessOptionPreLaunchScriptPathControl.Location = new System.Drawing.Point(10, 47);
            this.ProcessOptionPreLaunchScriptPathControl.Name = "ProcessOptionPreLaunchScriptPathControl";
            this.ProcessOptionPreLaunchScriptPathControl.Size = new System.Drawing.Size(399, 20);
            this.ProcessOptionPreLaunchScriptPathControl.TabIndex = 1;
            // 
            // ProcessOptionPreLaunchScriptEnabledControl
            // 
            this.ProcessOptionPreLaunchScriptEnabledControl.AutoSize = true;
            this.ProcessOptionPreLaunchScriptEnabledControl.Location = new System.Drawing.Point(10, 23);
            this.ProcessOptionPreLaunchScriptEnabledControl.Name = "ProcessOptionPreLaunchScriptEnabledControl";
            this.ProcessOptionPreLaunchScriptEnabledControl.Size = new System.Drawing.Size(200, 17);
            this.ProcessOptionPreLaunchScriptEnabledControl.TabIndex = 0;
            this.ProcessOptionPreLaunchScriptEnabledControl.Text = "Execute a command (ShellExecute) :";
            this.ProcessOptionPreLaunchScriptEnabledControl.UseVisualStyleBackColor = true;
            // 
            // PostCrashGroup
            // 
            this.PostCrashGroup.Controls.Add(this.ProcessOptionAggressiveCleanupByPIDControl);
            this.PostCrashGroup.Controls.Add(this.ProcessOptionAggressiveCleanupByNameControl);
            this.PostCrashGroup.Controls.Add(this.ProcessOptionAggressiveCleanupEnabledControl);
            this.PostCrashGroup.Controls.Add(this.ProcessPostCrashCommandButton);
            this.PostCrashGroup.Controls.Add(this.ProcessOptionPostCrashScriptPathControl);
            this.PostCrashGroup.Controls.Add(this.ProcessOptionPostCrashScriptEnabledControl);
            this.PostCrashGroup.Location = new System.Drawing.Point(12, 346);
            this.PostCrashGroup.Name = "PostCrashGroup";
            this.PostCrashGroup.Size = new System.Drawing.Size(460, 105);
            this.PostCrashGroup.TabIndex = 3;
            this.PostCrashGroup.TabStop = false;
            this.PostCrashGroup.Text = "After process stops (crashes or hangs)";
            // 
            // ProcessOptionAggressiveCleanupByPIDControl
            // 
            this.ProcessOptionAggressiveCleanupByPIDControl.AutoSize = true;
            this.ProcessOptionAggressiveCleanupByPIDControl.Location = new System.Drawing.Point(357, 23);
            this.ProcessOptionAggressiveCleanupByPIDControl.Name = "ProcessOptionAggressiveCleanupByPIDControl";
            this.ProcessOptionAggressiveCleanupByPIDControl.Size = new System.Drawing.Size(59, 17);
            this.ProcessOptionAggressiveCleanupByPIDControl.TabIndex = 2;
            this.ProcessOptionAggressiveCleanupByPIDControl.Text = "By PID";
            this.ProcessOptionAggressiveCleanupByPIDControl.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionAggressiveCleanupByNameControl
            // 
            this.ProcessOptionAggressiveCleanupByNameControl.AutoSize = true;
            this.ProcessOptionAggressiveCleanupByNameControl.Location = new System.Drawing.Point(282, 23);
            this.ProcessOptionAggressiveCleanupByNameControl.Name = "ProcessOptionAggressiveCleanupByNameControl";
            this.ProcessOptionAggressiveCleanupByNameControl.Size = new System.Drawing.Size(69, 17);
            this.ProcessOptionAggressiveCleanupByNameControl.TabIndex = 1;
            this.ProcessOptionAggressiveCleanupByNameControl.Text = "By Name";
            this.ProcessOptionAggressiveCleanupByNameControl.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionAggressiveCleanupEnabledControl
            // 
            this.ProcessOptionAggressiveCleanupEnabledControl.AutoSize = true;
            this.ProcessOptionAggressiveCleanupEnabledControl.Location = new System.Drawing.Point(10, 23);
            this.ProcessOptionAggressiveCleanupEnabledControl.Name = "ProcessOptionAggressiveCleanupEnabledControl";
            this.ProcessOptionAggressiveCleanupEnabledControl.Size = new System.Drawing.Size(263, 17);
            this.ProcessOptionAggressiveCleanupEnabledControl.TabIndex = 0;
            this.ProcessOptionAggressiveCleanupEnabledControl.Text = "Perform a cleanup ( taskkill to close the process ) :";
            this.ProcessOptionAggressiveCleanupEnabledControl.UseVisualStyleBackColor = true;
            // 
            // ProcessPostCrashCommandButton
            // 
            this.ProcessPostCrashCommandButton.Location = new System.Drawing.Point(417, 69);
            this.ProcessPostCrashCommandButton.Name = "ProcessPostCrashCommandButton";
            this.ProcessPostCrashCommandButton.Size = new System.Drawing.Size(34, 23);
            this.ProcessPostCrashCommandButton.TabIndex = 5;
            this.ProcessPostCrashCommandButton.Text = "...";
            this.ProcessPostCrashCommandButton.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionPostCrashScriptPathControl
            // 
            this.ProcessOptionPostCrashScriptPathControl.Location = new System.Drawing.Point(10, 70);
            this.ProcessOptionPostCrashScriptPathControl.Name = "ProcessOptionPostCrashScriptPathControl";
            this.ProcessOptionPostCrashScriptPathControl.Size = new System.Drawing.Size(399, 20);
            this.ProcessOptionPostCrashScriptPathControl.TabIndex = 4;
            // 
            // ProcessOptionPostCrashScriptEnabledControl
            // 
            this.ProcessOptionPostCrashScriptEnabledControl.AutoSize = true;
            this.ProcessOptionPostCrashScriptEnabledControl.Location = new System.Drawing.Point(10, 46);
            this.ProcessOptionPostCrashScriptEnabledControl.Name = "ProcessOptionPostCrashScriptEnabledControl";
            this.ProcessOptionPostCrashScriptEnabledControl.Size = new System.Drawing.Size(200, 17);
            this.ProcessOptionPostCrashScriptEnabledControl.TabIndex = 3;
            this.ProcessOptionPostCrashScriptEnabledControl.Text = "Execute a command (ShellExecute) :";
            this.ProcessOptionPostCrashScriptEnabledControl.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StartupStateDisabledControl);
            this.groupBox1.Controls.Add(this.StartupStateRunningControl);
            this.groupBox1.Controls.Add(this.StartupStateStoppedControl);
            this.groupBox1.Controls.Add(this.StartupStateLabel);
            this.groupBox1.Controls.Add(this.ProcessOptionsCancelButton);
            this.groupBox1.Controls.Add(this.ProcessOptionsSaveButton);
            this.groupBox1.Controls.Add(this.EnvironmentVariableFormatLabel);
            this.groupBox1.Controls.Add(this.OpenScreenshotDirectoryButton);
            this.groupBox1.Controls.Add(this.ProcessOptionEnvironmentVariablesControl);
            this.groupBox1.Controls.Add(this.ProcessOptionEnvironmentVariablesEnabledControl);
            this.groupBox1.Controls.Add(this.ProcessOptionCommandLineControl);
            this.groupBox1.Controls.Add(this.ProcessOptionCommandLineEnabledControl);
            this.groupBox1.Controls.Add(this.ProcessOptionAlwaysOnTopEnabledControl);
            this.groupBox1.Controls.Add(this.ProcessOptionScreenshotEnabledControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 457);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 243);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Miscellaneous options";
            // 
            // StartupStateDisabledControl
            // 
            this.StartupStateDisabledControl.AutoSize = true;
            this.StartupStateDisabledControl.Location = new System.Drawing.Point(300, 182);
            this.StartupStateDisabledControl.Name = "StartupStateDisabledControl";
            this.StartupStateDisabledControl.Size = new System.Drawing.Size(66, 17);
            this.StartupStateDisabledControl.TabIndex = 9;
            this.StartupStateDisabledControl.Text = "Disabled";
            this.StartupStateDisabledControl.UseVisualStyleBackColor = true;
            // 
            // StartupStateRunningControl
            // 
            this.StartupStateRunningControl.AutoSize = true;
            this.StartupStateRunningControl.Location = new System.Drawing.Point(228, 182);
            this.StartupStateRunningControl.Name = "StartupStateRunningControl";
            this.StartupStateRunningControl.Size = new System.Drawing.Size(65, 17);
            this.StartupStateRunningControl.TabIndex = 8;
            this.StartupStateRunningControl.Text = "Running";
            this.StartupStateRunningControl.UseVisualStyleBackColor = true;
            // 
            // StartupStateStoppedControl
            // 
            this.StartupStateStoppedControl.AutoSize = true;
            this.StartupStateStoppedControl.Checked = true;
            this.StartupStateStoppedControl.Location = new System.Drawing.Point(156, 182);
            this.StartupStateStoppedControl.Name = "StartupStateStoppedControl";
            this.StartupStateStoppedControl.Size = new System.Drawing.Size(65, 17);
            this.StartupStateStoppedControl.TabIndex = 7;
            this.StartupStateStoppedControl.TabStop = true;
            this.StartupStateStoppedControl.Text = "Stopped";
            this.StartupStateStoppedControl.UseVisualStyleBackColor = true;
            this.StartupStateStoppedControl.CheckedChanged += new System.EventHandler(this.OnStartupStateRadioGroupCheckedChanged);
            // 
            // StartupStateLabel
            // 
            this.StartupStateLabel.AutoSize = true;
            this.StartupStateLabel.Location = new System.Drawing.Point(7, 182);
            this.StartupStateLabel.Name = "StartupStateLabel";
            this.StartupStateLabel.Size = new System.Drawing.Size(141, 13);
            this.StartupStateLabel.TabIndex = 13;
            this.StartupStateLabel.Text = "Initially add this process as : ";
            // 
            // ProcessOptionsCancelButton
            // 
            this.ProcessOptionsCancelButton.Location = new System.Drawing.Point(294, 209);
            this.ProcessOptionsCancelButton.Name = "ProcessOptionsCancelButton";
            this.ProcessOptionsCancelButton.Size = new System.Drawing.Size(75, 23);
            this.ProcessOptionsCancelButton.TabIndex = 11;
            this.ProcessOptionsCancelButton.Text = "Cancel";
            this.ProcessOptionsCancelButton.UseVisualStyleBackColor = true;
            this.ProcessOptionsCancelButton.Click += new System.EventHandler(this.OnProcessOptionsCancelButtonClick);
            // 
            // ProcessOptionsSaveButton
            // 
            this.ProcessOptionsSaveButton.Location = new System.Drawing.Point(375, 209);
            this.ProcessOptionsSaveButton.Name = "ProcessOptionsSaveButton";
            this.ProcessOptionsSaveButton.Size = new System.Drawing.Size(75, 23);
            this.ProcessOptionsSaveButton.TabIndex = 10;
            this.ProcessOptionsSaveButton.Text = "Save";
            this.ProcessOptionsSaveButton.UseVisualStyleBackColor = true;
            this.ProcessOptionsSaveButton.Click += new System.EventHandler(this.OnProcessOptionsSaveButtonClick);
            // 
            // EnvironmentVariableFormatLabel
            // 
            this.EnvironmentVariableFormatLabel.AutoSize = true;
            this.EnvironmentVariableFormatLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnvironmentVariableFormatLabel.Location = new System.Drawing.Point(302, 94);
            this.EnvironmentVariableFormatLabel.Name = "EnvironmentVariableFormatLabel";
            this.EnvironmentVariableFormatLabel.Size = new System.Drawing.Size(145, 13);
            this.EnvironmentVariableFormatLabel.TabIndex = 10;
            this.EnvironmentVariableFormatLabel.Text = "var1=value1;var2=value2";
            // 
            // OpenScreenshotDirectoryButton
            // 
            this.OpenScreenshotDirectoryButton.Location = new System.Drawing.Point(291, 17);
            this.OpenScreenshotDirectoryButton.Name = "OpenScreenshotDirectoryButton";
            this.OpenScreenshotDirectoryButton.Size = new System.Drawing.Size(159, 23);
            this.OpenScreenshotDirectoryButton.TabIndex = 1;
            this.OpenScreenshotDirectoryButton.Text = "Open Screenshot Directory";
            this.OpenScreenshotDirectoryButton.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionEnvironmentVariablesControl
            // 
            this.ProcessOptionEnvironmentVariablesControl.Location = new System.Drawing.Point(10, 116);
            this.ProcessOptionEnvironmentVariablesControl.Multiline = true;
            this.ProcessOptionEnvironmentVariablesControl.Name = "ProcessOptionEnvironmentVariablesControl";
            this.ProcessOptionEnvironmentVariablesControl.Size = new System.Drawing.Size(440, 57);
            this.ProcessOptionEnvironmentVariablesControl.TabIndex = 6;
            // 
            // ProcessOptionEnvironmentVariablesEnabledControl
            // 
            this.ProcessOptionEnvironmentVariablesEnabledControl.AutoSize = true;
            this.ProcessOptionEnvironmentVariablesEnabledControl.Location = new System.Drawing.Point(10, 92);
            this.ProcessOptionEnvironmentVariablesEnabledControl.Name = "ProcessOptionEnvironmentVariablesEnabledControl";
            this.ProcessOptionEnvironmentVariablesEnabledControl.Size = new System.Drawing.Size(291, 17);
            this.ProcessOptionEnvironmentVariablesEnabledControl.TabIndex = 5;
            this.ProcessOptionEnvironmentVariablesEnabledControl.Text = "Merge listed variables with system environment variables";
            this.ProcessOptionEnvironmentVariablesEnabledControl.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionCommandLineControl
            // 
            this.ProcessOptionCommandLineControl.Location = new System.Drawing.Point(133, 67);
            this.ProcessOptionCommandLineControl.Name = "ProcessOptionCommandLineControl";
            this.ProcessOptionCommandLineControl.Size = new System.Drawing.Size(317, 20);
            this.ProcessOptionCommandLineControl.TabIndex = 4;
            // 
            // ProcessOptionCommandLineEnabledControl
            // 
            this.ProcessOptionCommandLineEnabledControl.AutoSize = true;
            this.ProcessOptionCommandLineEnabledControl.Location = new System.Drawing.Point(10, 69);
            this.ProcessOptionCommandLineEnabledControl.Name = "ProcessOptionCommandLineEnabledControl";
            this.ProcessOptionCommandLineEnabledControl.Size = new System.Drawing.Size(123, 17);
            this.ProcessOptionCommandLineEnabledControl.TabIndex = 3;
            this.ProcessOptionCommandLineEnabledControl.Text = "Pass command line :";
            this.ProcessOptionCommandLineEnabledControl.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionAlwaysOnTopEnabledControl
            // 
            this.ProcessOptionAlwaysOnTopEnabledControl.AutoSize = true;
            this.ProcessOptionAlwaysOnTopEnabledControl.Location = new System.Drawing.Point(10, 46);
            this.ProcessOptionAlwaysOnTopEnabledControl.Name = "ProcessOptionAlwaysOnTopEnabledControl";
            this.ProcessOptionAlwaysOnTopEnabledControl.Size = new System.Drawing.Size(410, 17);
            this.ProcessOptionAlwaysOnTopEnabledControl.TabIndex = 2;
            this.ProcessOptionAlwaysOnTopEnabledControl.Text = "Keep process always on top (this can conflict with other always-on-top processes)" +
    "";
            this.ProcessOptionAlwaysOnTopEnabledControl.UseVisualStyleBackColor = true;
            // 
            // ProcessOptionScreenshotEnabledControl
            // 
            this.ProcessOptionScreenshotEnabledControl.AutoSize = true;
            this.ProcessOptionScreenshotEnabledControl.Location = new System.Drawing.Point(10, 23);
            this.ProcessOptionScreenshotEnabledControl.Name = "ProcessOptionScreenshotEnabledControl";
            this.ProcessOptionScreenshotEnabledControl.Size = new System.Drawing.Size(249, 17);
            this.ProcessOptionScreenshotEnabledControl.TabIndex = 0;
            this.ProcessOptionScreenshotEnabledControl.Text = "Take a screenshot of the main display on crash";
            this.ProcessOptionScreenshotEnabledControl.UseVisualStyleBackColor = true;
            // 
            // ProcessDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 711);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PostCrashGroup);
            this.Controls.Add(this.ProcessPreStartGroup);
            this.Controls.Add(this.ProcessCrashAssumptionsGroup);
            this.Controls.Add(this.MonitorThisProcessGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProcessDialog";
            this.Text = "Process Options";
            this.MonitorThisProcessGroup.ResumeLayout(false);
            this.MonitorThisProcessGroup.PerformLayout();
            this.ProcessCrashAssumptionsGroup.ResumeLayout(false);
            this.ProcessCrashAssumptionsGroup.PerformLayout();
            this.ProcessPreStartGroup.ResumeLayout(false);
            this.ProcessPreStartGroup.PerformLayout();
            this.PostCrashGroup.ResumeLayout(false);
            this.PostCrashGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox MonitorThisProcessGroup;
        private System.Windows.Forms.TextBox ProcessOptionPathControl;
        private System.Windows.Forms.TextBox ProcessOptionWorkingDirectoryControl;
        private System.Windows.Forms.Label ProcessExecutablePathLabel;
        private System.Windows.Forms.Label ProcessWorkingDirectoryLabel;
        private System.Windows.Forms.Button SelectExecutablePath;
        private System.Windows.Forms.Button SelectWorkingDirectory;
        private System.Windows.Forms.GroupBox ProcessCrashAssumptionsGroup;
        private System.Windows.Forms.CheckBox ProcessOptionCrashedIfNotRunningControl;
        private System.Windows.Forms.CheckBox ProcessOptionCrashedIfUnresponsiveControl;
        private System.Windows.Forms.CheckBox ProcessOptionDoubleCheckEnabledControl;
        private System.Windows.Forms.CheckBox ProcessOptionGracePeriodEnabledControl;
        private System.Windows.Forms.TextBox ProcessOptionDoubleCheckDurationControl;
        private System.Windows.Forms.TextBox ProcessOptionGracePeriodDurationControl;
        private System.Windows.Forms.GroupBox ProcessPreStartGroup;
        private System.Windows.Forms.CheckBox ProcessOptionPreLaunchScriptEnabledControl;
        private System.Windows.Forms.Button ProcessPreStartCommandButton;
        private System.Windows.Forms.TextBox ProcessOptionPreLaunchScriptPathControl;
        private System.Windows.Forms.GroupBox PostCrashGroup;
        private System.Windows.Forms.Button ProcessPostCrashCommandButton;
        private System.Windows.Forms.TextBox ProcessOptionPostCrashScriptPathControl;
        private System.Windows.Forms.CheckBox ProcessOptionPostCrashScriptEnabledControl;
        private System.Windows.Forms.CheckBox ProcessOptionAggressiveCleanupEnabledControl;
        private System.Windows.Forms.Label PostCrashCheckEnabledLabel;
        private System.Windows.Forms.Label PostCrashWaitEnabledLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ProcessOptionScreenshotEnabledControl;
        private System.Windows.Forms.CheckBox ProcessOptionAlwaysOnTopEnabledControl;
        private System.Windows.Forms.CheckBox ProcessOptionCommandLineEnabledControl;
        private System.Windows.Forms.TextBox ProcessOptionCommandLineControl;
        private System.Windows.Forms.CheckBox ProcessOptionEnvironmentVariablesEnabledControl;
        private System.Windows.Forms.TextBox ProcessOptionEnvironmentVariablesControl;
        private System.Windows.Forms.Button OpenScreenshotDirectoryButton;
        private System.Windows.Forms.Label EnvironmentVariableFormatLabel;
        private System.Windows.Forms.Button ProcessOptionsSaveButton;
        private System.Windows.Forms.Button ProcessOptionsCancelButton;
        private System.Windows.Forms.CheckBox ProcessOptionAggressiveCleanupByNameControl;
        private System.Windows.Forms.CheckBox ProcessOptionAggressiveCleanupByPIDControl;
        private System.Windows.Forms.RadioButton StartupStateDisabledControl;
        private System.Windows.Forms.RadioButton StartupStateRunningControl;
        private System.Windows.Forms.RadioButton StartupStateStoppedControl;
        private System.Windows.Forms.Label StartupStateLabel;
    }
}