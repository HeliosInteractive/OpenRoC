namespace oroc
{
    using System.Windows.Forms;

    public partial class MainDialog : Form
    {
        private AddProcessDialog AddProcessForm;
        private SettingsDialog SettingsForm;
        private LogsDialog LogsForm;

        public MainDialog()
        {
            InitializeComponent();
        }

        private void DisposeAddedComponents()
        {
            if (AddProcessForm != null)
                AddProcessForm.Dispose();

            if (SettingsForm != null)
                SettingsForm.Dispose();

            if (LogsForm != null)
                LogsForm.Dispose();
        }

        private void OnProcessListViewResize(object sender, System.EventArgs e)
        {
            if (ProcessListView.Columns.Count > 0)
                ProcessListView.AutoResizeColumn(
                    ProcessListView.Columns.Count - 1,
                    ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private static void HandleDialogRequest<T>(ref T host)
            where T : Form, new()
        {
            if (host == null || host.IsDisposed)
                host = new T();

            if (!host.Visible)
                host.Show();
            else
                host.Focus();
        }

        private void OnSettingsButtonClick(object sender, System.EventArgs e)
        {
            HandleDialogRequest(ref SettingsForm);
        }

        private void OnAddButtonClick(object sender, System.EventArgs e)
        {
            HandleDialogRequest(ref AddProcessForm);
        }

        private void OnLogButtonClick(object sender, System.EventArgs e)
        {
            HandleDialogRequest(ref LogsForm);
        }
    }
}
