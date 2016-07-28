namespace oroc
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    public partial class LogsDialog : Form
    {
        public LogsDialog()
        {
            InitializeComponent();

            if (LogTextBox.Handle != IntPtr.Zero)
                Logger.Configure(this, LogTextBox);
        }

        private void OnLogsDialogFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void OnLogsDialogRichTextBoxLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText).Dispose();
        }
    }
}
