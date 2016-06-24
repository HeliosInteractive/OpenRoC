namespace oroc
{
    partial class AddProcessDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProcessDialog));
            this.MonitorThisProcessGroup = new System.Windows.Forms.GroupBox();
            this.ProcessExecutablePathTextBox = new System.Windows.Forms.TextBox();
            this.ProcessWorkingDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.ProcessExecutablePathLabel = new System.Windows.Forms.Label();
            this.ProcessWorkingDirectoryLabel = new System.Windows.Forms.Label();
            this.SelectExecutablePath = new System.Windows.Forms.Button();
            this.SelectWorkingDirectory = new System.Windows.Forms.Button();
            this.ProcessCrashAssumptionsGroup = new System.Windows.Forms.GroupBox();
            this.AssumeCrashIfNotRunningCheckBox = new System.Windows.Forms.CheckBox();
            this.AssumeCrashIfUnresponsiveCheckBox = new System.Windows.Forms.CheckBox();
            this.PostCrashCheckEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.PostCrashWaitEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ProcessPreStartGroup = new System.Windows.Forms.GroupBox();
            this.ProcessPreStartCommandEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.ProcessPreStartCommandTextBox = new System.Windows.Forms.TextBox();
            this.ProcessPreStartCommandButton = new System.Windows.Forms.Button();
            this.PostCrashGroup = new System.Windows.Forms.GroupBox();
            this.ProcessPostCrashCommandButton = new System.Windows.Forms.Button();
            this.ProcessPostCrashCommandTextBox = new System.Windows.Forms.TextBox();
            this.ProcessPostCrashCommandEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.ProcessPostCrashCleanupEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.PostCrashCheckEnabledLabel = new System.Windows.Forms.Label();
            this.PostCrashWaitEnabledLabel = new System.Windows.Forms.Label();
            this.MonitorThisProcessGroup.SuspendLayout();
            this.ProcessCrashAssumptionsGroup.SuspendLayout();
            this.ProcessPreStartGroup.SuspendLayout();
            this.PostCrashGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // MonitorThisProcessGroup
            // 
            this.MonitorThisProcessGroup.Controls.Add(this.SelectWorkingDirectory);
            this.MonitorThisProcessGroup.Controls.Add(this.SelectExecutablePath);
            this.MonitorThisProcessGroup.Controls.Add(this.ProcessWorkingDirectoryLabel);
            this.MonitorThisProcessGroup.Controls.Add(this.ProcessExecutablePathLabel);
            this.MonitorThisProcessGroup.Controls.Add(this.ProcessWorkingDirectoryTextBox);
            this.MonitorThisProcessGroup.Controls.Add(this.ProcessExecutablePathTextBox);
            this.MonitorThisProcessGroup.Location = new System.Drawing.Point(12, 6);
            this.MonitorThisProcessGroup.Name = "MonitorThisProcessGroup";
            this.MonitorThisProcessGroup.Size = new System.Drawing.Size(460, 116);
            this.MonitorThisProcessGroup.TabIndex = 0;
            this.MonitorThisProcessGroup.TabStop = false;
            this.MonitorThisProcessGroup.Text = "Monitor this process";
            // 
            // ProcessExecutablePathTextBox
            // 
            this.ProcessExecutablePathTextBox.Location = new System.Drawing.Point(102, 20);
            this.ProcessExecutablePathTextBox.Name = "ProcessExecutablePathTextBox";
            this.ProcessExecutablePathTextBox.Size = new System.Drawing.Size(349, 20);
            this.ProcessExecutablePathTextBox.TabIndex = 0;
            // 
            // ProcessWorkingDirectoryTextBox
            // 
            this.ProcessWorkingDirectoryTextBox.Location = new System.Drawing.Point(102, 50);
            this.ProcessWorkingDirectoryTextBox.Name = "ProcessWorkingDirectoryTextBox";
            this.ProcessWorkingDirectoryTextBox.Size = new System.Drawing.Size(349, 20);
            this.ProcessWorkingDirectoryTextBox.TabIndex = 1;
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
            // ProcessWorkingDirectoryLabel
            // 
            this.ProcessWorkingDirectoryLabel.AutoSize = true;
            this.ProcessWorkingDirectoryLabel.Location = new System.Drawing.Point(7, 53);
            this.ProcessWorkingDirectoryLabel.Name = "ProcessWorkingDirectoryLabel";
            this.ProcessWorkingDirectoryLabel.Size = new System.Drawing.Size(92, 13);
            this.ProcessWorkingDirectoryLabel.TabIndex = 3;
            this.ProcessWorkingDirectoryLabel.Text = "Working Directory";
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
            // SelectWorkingDirectory
            // 
            this.SelectWorkingDirectory.Location = new System.Drawing.Point(156, 79);
            this.SelectWorkingDirectory.Name = "SelectWorkingDirectory";
            this.SelectWorkingDirectory.Size = new System.Drawing.Size(148, 23);
            this.SelectWorkingDirectory.TabIndex = 5;
            this.SelectWorkingDirectory.Text = "Select Working Directory";
            this.SelectWorkingDirectory.UseVisualStyleBackColor = true;
            // 
            // ProcessCrashAssumptionsGroup
            // 
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.PostCrashWaitEnabledLabel);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.PostCrashCheckEnabledLabel);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.textBox2);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.textBox1);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.PostCrashWaitEnabledCheckBox);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.PostCrashCheckEnabledCheckBox);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.AssumeCrashIfUnresponsiveCheckBox);
            this.ProcessCrashAssumptionsGroup.Controls.Add(this.AssumeCrashIfNotRunningCheckBox);
            this.ProcessCrashAssumptionsGroup.Location = new System.Drawing.Point(12, 127);
            this.ProcessCrashAssumptionsGroup.Name = "ProcessCrashAssumptionsGroup";
            this.ProcessCrashAssumptionsGroup.Size = new System.Drawing.Size(460, 126);
            this.ProcessCrashAssumptionsGroup.TabIndex = 1;
            this.ProcessCrashAssumptionsGroup.TabStop = false;
            this.ProcessCrashAssumptionsGroup.Text = "Assume process has crashed or hung when";
            // 
            // AssumeCrashIfNotRunningCheckBox
            // 
            this.AssumeCrashIfNotRunningCheckBox.AutoSize = true;
            this.AssumeCrashIfNotRunningCheckBox.Location = new System.Drawing.Point(10, 23);
            this.AssumeCrashIfNotRunningCheckBox.Name = "AssumeCrashIfNotRunningCheckBox";
            this.AssumeCrashIfNotRunningCheckBox.Size = new System.Drawing.Size(232, 17);
            this.AssumeCrashIfNotRunningCheckBox.TabIndex = 0;
            this.AssumeCrashIfNotRunningCheckBox.Text = "It\'s not running (e.g after a Computer restart)";
            this.AssumeCrashIfNotRunningCheckBox.UseVisualStyleBackColor = true;
            // 
            // AssumeCrashIfUnresponsiveCheckBox
            // 
            this.AssumeCrashIfUnresponsiveCheckBox.AutoSize = true;
            this.AssumeCrashIfUnresponsiveCheckBox.Location = new System.Drawing.Point(10, 46);
            this.AssumeCrashIfUnresponsiveCheckBox.Name = "AssumeCrashIfUnresponsiveCheckBox";
            this.AssumeCrashIfUnresponsiveCheckBox.Size = new System.Drawing.Size(332, 17);
            this.AssumeCrashIfUnresponsiveCheckBox.TabIndex = 1;
            this.AssumeCrashIfUnresponsiveCheckBox.Text = "It\'s not responding to Windows messages (unresponsive window)";
            this.AssumeCrashIfUnresponsiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // PostCrashCheckEnabledCheckBox
            // 
            this.PostCrashCheckEnabledCheckBox.AutoSize = true;
            this.PostCrashCheckEnabledCheckBox.Location = new System.Drawing.Point(10, 69);
            this.PostCrashCheckEnabledCheckBox.Name = "PostCrashCheckEnabledCheckBox";
            this.PostCrashCheckEnabledCheckBox.Size = new System.Drawing.Size(48, 17);
            this.PostCrashCheckEnabledCheckBox.TabIndex = 2;
            this.PostCrashCheckEnabledCheckBox.Text = "Wait";
            this.PostCrashCheckEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // PostCrashWaitEnabledCheckBox
            // 
            this.PostCrashWaitEnabledCheckBox.AutoSize = true;
            this.PostCrashWaitEnabledCheckBox.Location = new System.Drawing.Point(10, 92);
            this.PostCrashWaitEnabledCheckBox.Name = "PostCrashWaitEnabledCheckBox";
            this.PostCrashWaitEnabledCheckBox.Size = new System.Drawing.Size(48, 17);
            this.PostCrashWaitEnabledCheckBox.TabIndex = 4;
            this.PostCrashWaitEnabledCheckBox.Text = "Wait";
            this.PostCrashWaitEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 67);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(75, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(57, 91);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(75, 20);
            this.textBox2.TabIndex = 5;
            // 
            // ProcessPreStartGroup
            // 
            this.ProcessPreStartGroup.Controls.Add(this.ProcessPreStartCommandButton);
            this.ProcessPreStartGroup.Controls.Add(this.ProcessPreStartCommandTextBox);
            this.ProcessPreStartGroup.Controls.Add(this.ProcessPreStartCommandEnabledCheckBox);
            this.ProcessPreStartGroup.Location = new System.Drawing.Point(12, 258);
            this.ProcessPreStartGroup.Name = "ProcessPreStartGroup";
            this.ProcessPreStartGroup.Size = new System.Drawing.Size(460, 82);
            this.ProcessPreStartGroup.TabIndex = 2;
            this.ProcessPreStartGroup.TabStop = false;
            this.ProcessPreStartGroup.Text = "Before process starts";
            // 
            // ProcessPreStartCommandEnabledCheckBox
            // 
            this.ProcessPreStartCommandEnabledCheckBox.AutoSize = true;
            this.ProcessPreStartCommandEnabledCheckBox.Location = new System.Drawing.Point(10, 23);
            this.ProcessPreStartCommandEnabledCheckBox.Name = "ProcessPreStartCommandEnabledCheckBox";
            this.ProcessPreStartCommandEnabledCheckBox.Size = new System.Drawing.Size(200, 17);
            this.ProcessPreStartCommandEnabledCheckBox.TabIndex = 0;
            this.ProcessPreStartCommandEnabledCheckBox.Text = "Execute a command (ShellExecute) :";
            this.ProcessPreStartCommandEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // ProcessPreStartCommandTextBox
            // 
            this.ProcessPreStartCommandTextBox.Location = new System.Drawing.Point(10, 47);
            this.ProcessPreStartCommandTextBox.Name = "ProcessPreStartCommandTextBox";
            this.ProcessPreStartCommandTextBox.Size = new System.Drawing.Size(399, 20);
            this.ProcessPreStartCommandTextBox.TabIndex = 1;
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
            // PostCrashGroup
            // 
            this.PostCrashGroup.Controls.Add(this.ProcessPostCrashCleanupEnabledCheckBox);
            this.PostCrashGroup.Controls.Add(this.ProcessPostCrashCommandButton);
            this.PostCrashGroup.Controls.Add(this.ProcessPostCrashCommandTextBox);
            this.PostCrashGroup.Controls.Add(this.ProcessPostCrashCommandEnabledCheckBox);
            this.PostCrashGroup.Location = new System.Drawing.Point(12, 346);
            this.PostCrashGroup.Name = "PostCrashGroup";
            this.PostCrashGroup.Size = new System.Drawing.Size(460, 105);
            this.PostCrashGroup.TabIndex = 3;
            this.PostCrashGroup.TabStop = false;
            this.PostCrashGroup.Text = "After process stops (crashes or hangs)";
            // 
            // ProcessPostCrashCommandButton
            // 
            this.ProcessPostCrashCommandButton.Location = new System.Drawing.Point(417, 69);
            this.ProcessPostCrashCommandButton.Name = "ProcessPostCrashCommandButton";
            this.ProcessPostCrashCommandButton.Size = new System.Drawing.Size(34, 23);
            this.ProcessPostCrashCommandButton.TabIndex = 3;
            this.ProcessPostCrashCommandButton.Text = "...";
            this.ProcessPostCrashCommandButton.UseVisualStyleBackColor = true;
            // 
            // ProcessPostCrashCommandTextBox
            // 
            this.ProcessPostCrashCommandTextBox.Location = new System.Drawing.Point(10, 70);
            this.ProcessPostCrashCommandTextBox.Name = "ProcessPostCrashCommandTextBox";
            this.ProcessPostCrashCommandTextBox.Size = new System.Drawing.Size(399, 20);
            this.ProcessPostCrashCommandTextBox.TabIndex = 2;
            // 
            // ProcessPostCrashCommandEnabledCheckBox
            // 
            this.ProcessPostCrashCommandEnabledCheckBox.AutoSize = true;
            this.ProcessPostCrashCommandEnabledCheckBox.Location = new System.Drawing.Point(10, 46);
            this.ProcessPostCrashCommandEnabledCheckBox.Name = "ProcessPostCrashCommandEnabledCheckBox";
            this.ProcessPostCrashCommandEnabledCheckBox.Size = new System.Drawing.Size(200, 17);
            this.ProcessPostCrashCommandEnabledCheckBox.TabIndex = 1;
            this.ProcessPostCrashCommandEnabledCheckBox.Text = "Execute a command (ShellExecute) :";
            this.ProcessPostCrashCommandEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // ProcessPostCrashCleanupEnabledCheckBox
            // 
            this.ProcessPostCrashCleanupEnabledCheckBox.AutoSize = true;
            this.ProcessPostCrashCleanupEnabledCheckBox.Location = new System.Drawing.Point(10, 23);
            this.ProcessPostCrashCleanupEnabledCheckBox.Name = "ProcessPostCrashCleanupEnabledCheckBox";
            this.ProcessPostCrashCleanupEnabledCheckBox.Size = new System.Drawing.Size(265, 17);
            this.ProcessPostCrashCleanupEnabledCheckBox.TabIndex = 0;
            this.ProcessPostCrashCleanupEnabledCheckBox.Text = "Perform a cleanup (aggressively close the process)";
            this.ProcessPostCrashCleanupEnabledCheckBox.UseVisualStyleBackColor = true;
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
            // PostCrashWaitEnabledLabel
            // 
            this.PostCrashWaitEnabledLabel.AutoSize = true;
            this.PostCrashWaitEnabledLabel.Location = new System.Drawing.Point(138, 93);
            this.PostCrashWaitEnabledLabel.Name = "PostCrashWaitEnabledLabel";
            this.PostCrashWaitEnabledLabel.Size = new System.Drawing.Size(247, 13);
            this.PostCrashWaitEnabledLabel.TabIndex = 7;
            this.PostCrashWaitEnabledLabel.Text = "seconds after a crash before attempting a relaunch";
            // 
            // AddProcessDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.PostCrashGroup);
            this.Controls.Add(this.ProcessPreStartGroup);
            this.Controls.Add(this.ProcessCrashAssumptionsGroup);
            this.Controls.Add(this.MonitorThisProcessGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddProcessDialog";
            this.Text = "Add New Process";
            this.MonitorThisProcessGroup.ResumeLayout(false);
            this.MonitorThisProcessGroup.PerformLayout();
            this.ProcessCrashAssumptionsGroup.ResumeLayout(false);
            this.ProcessCrashAssumptionsGroup.PerformLayout();
            this.ProcessPreStartGroup.ResumeLayout(false);
            this.ProcessPreStartGroup.PerformLayout();
            this.PostCrashGroup.ResumeLayout(false);
            this.PostCrashGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox MonitorThisProcessGroup;
        private System.Windows.Forms.TextBox ProcessExecutablePathTextBox;
        private System.Windows.Forms.TextBox ProcessWorkingDirectoryTextBox;
        private System.Windows.Forms.Label ProcessExecutablePathLabel;
        private System.Windows.Forms.Label ProcessWorkingDirectoryLabel;
        private System.Windows.Forms.Button SelectExecutablePath;
        private System.Windows.Forms.Button SelectWorkingDirectory;
        private System.Windows.Forms.GroupBox ProcessCrashAssumptionsGroup;
        private System.Windows.Forms.CheckBox AssumeCrashIfNotRunningCheckBox;
        private System.Windows.Forms.CheckBox AssumeCrashIfUnresponsiveCheckBox;
        private System.Windows.Forms.CheckBox PostCrashCheckEnabledCheckBox;
        private System.Windows.Forms.CheckBox PostCrashWaitEnabledCheckBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox ProcessPreStartGroup;
        private System.Windows.Forms.CheckBox ProcessPreStartCommandEnabledCheckBox;
        private System.Windows.Forms.Button ProcessPreStartCommandButton;
        private System.Windows.Forms.TextBox ProcessPreStartCommandTextBox;
        private System.Windows.Forms.GroupBox PostCrashGroup;
        private System.Windows.Forms.Button ProcessPostCrashCommandButton;
        private System.Windows.Forms.TextBox ProcessPostCrashCommandTextBox;
        private System.Windows.Forms.CheckBox ProcessPostCrashCommandEnabledCheckBox;
        private System.Windows.Forms.CheckBox ProcessPostCrashCleanupEnabledCheckBox;
        private System.Windows.Forms.Label PostCrashCheckEnabledLabel;
        private System.Windows.Forms.Label PostCrashWaitEnabledLabel;
    }
}