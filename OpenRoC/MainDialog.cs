namespace oroc
{
    using System.Linq;
    using System.Windows.Forms;

    public partial class MainDialog : Form
    {
        private ProcessDialog EditProcessForm;
        private ProcessDialog AddProcessForm;
        private SettingsDialog SettingsForm;
        private AboutDialog AboutForm;
        private LogsDialog LogsForm;

        public readonly ProcessManager ProcessManager;

        public MainDialog()
        {
            InitializeComponent();
            ProcessManager = new ProcessManager();
        }

        private void DisposeAddedComponents()
        {
            if (ProcessManager != null)
                ProcessManager.Dispose();

            if (EditProcessForm != null)
                EditProcessForm.Dispose();

            if (AddProcessForm != null)
                AddProcessForm.Dispose();

            if (SettingsForm != null)
                SettingsForm.Dispose();

            if (AboutForm != null)
                AboutForm.Dispose();

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

        private void HandleDialogRequest<T>(ref T host)
            where T : Form, new()
        {
            if (host == null || host.IsDisposed)
            {
                host = new T();
                host.Owner = this;
            }

            if (!host.Visible)
                host.Show();
            else
                host.Focus();
        }

        public void UpdateProcessList()
        {
            if (ProcessListView.Items.Count > 0)
                ProcessListView.Items.Clear();

            ProcessManager.ProcessList.ForEach(p =>
            {
                ListViewItem item = new ListViewItem(p.Path);
                item.SubItems.Add(p.State.ToString());
                ProcessListView.Items.Add(item);
            });
        }

        private void OnSettingsButtonClick(object sender, System.EventArgs e)
        {
            HandleDialogRequest(ref SettingsForm);
        }

        private void OnAddButtonClick(object sender, System.EventArgs e)
        {
            HandleDialogRequest(ref AddProcessForm);
        }

        private void OnAboutButtonClick(object sender, System.EventArgs e)
        {
            HandleDialogRequest(ref AboutForm);
        }

        private void OnLogButtonClick(object sender, System.EventArgs e)
        {
            HandleDialogRequest(ref LogsForm);
        }

        private void OnContextMenuEditButtonClick(object sender, System.EventArgs e)
        {
            MonitorableProcess process = ProcessManager.ProcessList.Single(x => x.Path == ProcessListView.FocusedItem.Text);

            if (process == null)
                return;

            if (EditProcessForm == null || EditProcessForm.IsDisposed)
            {
                EditProcessForm = new ProcessDialog(process.GetOptions());
                EditProcessForm.Owner = this;
            }

            if (!EditProcessForm.Visible)
                EditProcessForm.Show();
            else
                EditProcessForm.Focus();
        }

        private void OnContextMenuDeleteButtonClick(object sender, System.EventArgs e)
        {
            ProcessManager.Delete(ProcessListView.FocusedItem.Text);
            UpdateProcessList();
        }

        private void OnDeleteButtonClick(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in ProcessListView.SelectedItems)
                ProcessManager.Delete(item.Text);

            UpdateProcessList();
        }

        private void OnContextMenuDisableButtonClick(object sender, System.EventArgs e)
        {
            ProcessListView.FocusedItem.Checked = false;
        }
    }
}
