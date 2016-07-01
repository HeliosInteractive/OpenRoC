namespace oroc
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.StartOptionsGroup = new System.Windows.Forms.GroupBox();
            this.SingleInstanceCheckBox = new System.Windows.Forms.CheckBox();
            this.StartMinimizedCheckBox = new System.Windows.Forms.CheckBox();
            this.HttpGroup = new System.Windows.Forms.GroupBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.HostLabel = new System.Windows.Forms.Label();
            this.HttpPortTextBox = new System.Windows.Forms.TextBox();
            this.HttpHostTextBox = new System.Windows.Forms.TextBox();
            this.HttpInterfaceEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.StartOptionsGroup.SuspendLayout();
            this.HttpGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartOptionsGroup
            // 
            this.StartOptionsGroup.Controls.Add(this.SingleInstanceCheckBox);
            this.StartOptionsGroup.Controls.Add(this.StartMinimizedCheckBox);
            this.StartOptionsGroup.Location = new System.Drawing.Point(12, 6);
            this.StartOptionsGroup.Name = "StartOptionsGroup";
            this.StartOptionsGroup.Size = new System.Drawing.Size(260, 77);
            this.StartOptionsGroup.TabIndex = 0;
            this.StartOptionsGroup.TabStop = false;
            this.StartOptionsGroup.Text = "Next time on start";
            // 
            // SingleInstanceCheckBox
            // 
            this.SingleInstanceCheckBox.AutoSize = true;
            this.SingleInstanceCheckBox.Location = new System.Drawing.Point(10, 46);
            this.SingleInstanceCheckBox.Name = "SingleInstanceCheckBox";
            this.SingleInstanceCheckBox.Size = new System.Drawing.Size(155, 17);
            this.SingleInstanceCheckBox.TabIndex = 1;
            this.SingleInstanceCheckBox.Text = "Force single-instance mode";
            this.SingleInstanceCheckBox.UseVisualStyleBackColor = true;
            // 
            // StartMinimizedCheckBox
            // 
            this.StartMinimizedCheckBox.AutoSize = true;
            this.StartMinimizedCheckBox.Location = new System.Drawing.Point(10, 23);
            this.StartMinimizedCheckBox.Name = "StartMinimizedCheckBox";
            this.StartMinimizedCheckBox.Size = new System.Drawing.Size(146, 17);
            this.StartMinimizedCheckBox.TabIndex = 0;
            this.StartMinimizedCheckBox.Text = "Start OpenRoC minimized";
            this.StartMinimizedCheckBox.UseVisualStyleBackColor = true;
            // 
            // HttpGroup
            // 
            this.HttpGroup.Controls.Add(this.PortLabel);
            this.HttpGroup.Controls.Add(this.HostLabel);
            this.HttpGroup.Controls.Add(this.HttpPortTextBox);
            this.HttpGroup.Controls.Add(this.HttpHostTextBox);
            this.HttpGroup.Controls.Add(this.HttpInterfaceEnabledCheckBox);
            this.HttpGroup.Location = new System.Drawing.Point(12, 89);
            this.HttpGroup.Name = "HttpGroup";
            this.HttpGroup.Size = new System.Drawing.Size(260, 100);
            this.HttpGroup.TabIndex = 1;
            this.HttpGroup.TabStop = false;
            this.HttpGroup.Text = "HTTP interface";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(7, 69);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(32, 13);
            this.PortLabel.TabIndex = 4;
            this.PortLabel.Text = "Port :";
            // 
            // HostLabel
            // 
            this.HostLabel.AutoSize = true;
            this.HostLabel.Location = new System.Drawing.Point(7, 46);
            this.HostLabel.Name = "HostLabel";
            this.HostLabel.Size = new System.Drawing.Size(35, 13);
            this.HostLabel.TabIndex = 3;
            this.HostLabel.Text = "Host :";
            // 
            // HttpPortTextBox
            // 
            this.HttpPortTextBox.Enabled = false;
            this.HttpPortTextBox.Location = new System.Drawing.Point(42, 67);
            this.HttpPortTextBox.Name = "HttpPortTextBox";
            this.HttpPortTextBox.Size = new System.Drawing.Size(208, 20);
            this.HttpPortTextBox.TabIndex = 2;
            // 
            // HttpHostTextBox
            // 
            this.HttpHostTextBox.Enabled = false;
            this.HttpHostTextBox.Location = new System.Drawing.Point(42, 44);
            this.HttpHostTextBox.Name = "HttpHostTextBox";
            this.HttpHostTextBox.Size = new System.Drawing.Size(208, 20);
            this.HttpHostTextBox.TabIndex = 1;
            // 
            // HttpInterfaceEnabledCheckBox
            // 
            this.HttpInterfaceEnabledCheckBox.AutoSize = true;
            this.HttpInterfaceEnabledCheckBox.Enabled = false;
            this.HttpInterfaceEnabledCheckBox.Location = new System.Drawing.Point(10, 23);
            this.HttpInterfaceEnabledCheckBox.Name = "HttpInterfaceEnabledCheckBox";
            this.HttpInterfaceEnabledCheckBox.Size = new System.Drawing.Size(132, 17);
            this.HttpInterfaceEnabledCheckBox.TabIndex = 0;
            this.HttpInterfaceEnabledCheckBox.Text = "Enable web interface :";
            this.HttpInterfaceEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 199);
            this.Controls.Add(this.HttpGroup);
            this.Controls.Add(this.StartOptionsGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsDialog";
            this.Text = "Settings";
            this.StartOptionsGroup.ResumeLayout(false);
            this.StartOptionsGroup.PerformLayout();
            this.HttpGroup.ResumeLayout(false);
            this.HttpGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox StartOptionsGroup;
        private System.Windows.Forms.CheckBox StartMinimizedCheckBox;
        private System.Windows.Forms.CheckBox SingleInstanceCheckBox;
        private System.Windows.Forms.GroupBox HttpGroup;
        private System.Windows.Forms.CheckBox HttpInterfaceEnabledCheckBox;
        private System.Windows.Forms.TextBox HttpHostTextBox;
        private System.Windows.Forms.TextBox HttpPortTextBox;
        private System.Windows.Forms.Label HostLabel;
        private System.Windows.Forms.Label PortLabel;
    }
}