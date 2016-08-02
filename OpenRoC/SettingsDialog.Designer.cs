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
            this.SensuGroup = new System.Windows.Forms.GroupBox();
            this.SensuPortTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.SensuHostTextBox = new System.Windows.Forms.TextBox();
            this.HostLabel = new System.Windows.Forms.Label();
            this.SensuInterfaceEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.StartOptionsGroup.SuspendLayout();
            this.SensuGroup.SuspendLayout();
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
            // SensuGroup
            // 
            this.SensuGroup.Controls.Add(this.SensuPortTextBox);
            this.SensuGroup.Controls.Add(this.PortLabel);
            this.SensuGroup.Controls.Add(this.SensuHostTextBox);
            this.SensuGroup.Controls.Add(this.HostLabel);
            this.SensuGroup.Controls.Add(this.SensuInterfaceEnabledCheckBox);
            this.SensuGroup.Location = new System.Drawing.Point(12, 89);
            this.SensuGroup.Name = "SensuGroup";
            this.SensuGroup.Size = new System.Drawing.Size(260, 77);
            this.SensuGroup.TabIndex = 2;
            this.SensuGroup.TabStop = false;
            this.SensuGroup.Text = "Sensu interface";
            // 
            // SensuPortTextBox
            // 
            this.SensuPortTextBox.Location = new System.Drawing.Point(205, 44);
            this.SensuPortTextBox.Name = "SensuPortTextBox";
            this.SensuPortTextBox.Size = new System.Drawing.Size(45, 20);
            this.SensuPortTextBox.TabIndex = 4;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(170, 47);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(32, 13);
            this.PortLabel.TabIndex = 3;
            this.PortLabel.Text = "Port :";
            // 
            // SensuHostTextBox
            // 
            this.SensuHostTextBox.Location = new System.Drawing.Point(42, 44);
            this.SensuHostTextBox.Name = "SensuHostTextBox";
            this.SensuHostTextBox.Size = new System.Drawing.Size(123, 20);
            this.SensuHostTextBox.TabIndex = 2;
            // 
            // HostLabel
            // 
            this.HostLabel.AutoSize = true;
            this.HostLabel.Location = new System.Drawing.Point(7, 47);
            this.HostLabel.Name = "HostLabel";
            this.HostLabel.Size = new System.Drawing.Size(35, 13);
            this.HostLabel.TabIndex = 1;
            this.HostLabel.Text = "Host :";
            // 
            // SensuInterfaceEnabledCheckBox
            // 
            this.SensuInterfaceEnabledCheckBox.AutoSize = true;
            this.SensuInterfaceEnabledCheckBox.Location = new System.Drawing.Point(10, 23);
            this.SensuInterfaceEnabledCheckBox.Name = "SensuInterfaceEnabledCheckBox";
            this.SensuInterfaceEnabledCheckBox.Size = new System.Drawing.Size(142, 17);
            this.SensuInterfaceEnabledCheckBox.TabIndex = 0;
            this.SensuInterfaceEnabledCheckBox.Text = "Enable Sensu interface :";
            this.SensuInterfaceEnabledCheckBox.UseVisualStyleBackColor = true;
            this.SensuInterfaceEnabledCheckBox.CheckedChanged += new System.EventHandler(this.OnSensuInterfaceEnabledCheckBoxCheckedChanged);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 177);
            this.Controls.Add(this.SensuGroup);
            this.Controls.Add(this.StartOptionsGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsDialog";
            this.Text = "Settings";
            this.StartOptionsGroup.ResumeLayout(false);
            this.StartOptionsGroup.PerformLayout();
            this.SensuGroup.ResumeLayout(false);
            this.SensuGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox StartOptionsGroup;
        private System.Windows.Forms.CheckBox StartMinimizedCheckBox;
        private System.Windows.Forms.CheckBox SingleInstanceCheckBox;
        private System.Windows.Forms.GroupBox SensuGroup;
        private System.Windows.Forms.CheckBox SensuInterfaceEnabledCheckBox;
        private System.Windows.Forms.TextBox SensuPortTextBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox SensuHostTextBox;
        private System.Windows.Forms.Label HostLabel;
    }
}