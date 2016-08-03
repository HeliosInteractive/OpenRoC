namespace oroc
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.AboutRichTextBox = new System.Windows.Forms.RichTextBox();
            this.title = new System.Windows.Forms.Label();
            this.caption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AboutRichTextBox
            // 
            this.AboutRichTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.AboutRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AboutRichTextBox.Location = new System.Drawing.Point(12, 115);
            this.AboutRichTextBox.Name = "AboutRichTextBox";
            this.AboutRichTextBox.ReadOnly = true;
            this.AboutRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.AboutRichTextBox.Size = new System.Drawing.Size(528, 170);
            this.AboutRichTextBox.TabIndex = 0;
            this.AboutRichTextBox.TabStop = false;
            this.AboutRichTextBox.Text = resources.GetString("AboutRichTextBox.Text");
            this.AboutRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.OnAboutRichTextBoxLinkClicked);
            // 
            // title
            // 
            this.title.Dock = System.Windows.Forms.DockStyle.Top;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(552, 64);
            this.title.TabIndex = 1;
            this.title.Text = "OpenRoC";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // caption
            // 
            this.caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.caption.Location = new System.Drawing.Point(0, 64);
            this.caption.Name = "caption";
            this.caption.Size = new System.Drawing.Size(552, 39);
            this.caption.TabIndex = 2;
            this.caption.Text = "Open-source Restart on Crash";
            this.caption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 297);
            this.Controls.Add(this.caption);
            this.Controls.Add(this.title);
            this.Controls.Add(this.AboutRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AboutDialog";
            this.Text = "About";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox AboutRichTextBox;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label caption;
    }
}