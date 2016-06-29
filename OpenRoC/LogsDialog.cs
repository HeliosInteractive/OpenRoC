namespace oroc
{
    using System;
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
    }
}
