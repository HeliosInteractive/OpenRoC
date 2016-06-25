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
            this.AboutBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // AboutBrowser
            // 
            this.AboutBrowser.AllowWebBrowserDrop = false;
            this.AboutBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AboutBrowser.IsWebBrowserContextMenuEnabled = false;
            this.AboutBrowser.Location = new System.Drawing.Point(0, 0);
            this.AboutBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.AboutBrowser.Name = "AboutBrowser";
            this.AboutBrowser.ScriptErrorsSuppressed = true;
            this.AboutBrowser.ScrollBarsEnabled = false;
            this.AboutBrowser.Size = new System.Drawing.Size(370, 241);
            this.AboutBrowser.TabIndex = 2;
            this.AboutBrowser.WebBrowserShortcutsEnabled = false;
            this.AboutBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.OnAboutBrowserNavigating);
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 241);
            this.Controls.Add(this.AboutBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AboutDialog";
            this.Text = "About";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser AboutBrowser;
    }
}