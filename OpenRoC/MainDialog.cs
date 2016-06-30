namespace oroc
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.ComponentModel;
    using System.Collections.Generic;

    public partial class MainDialog : Form
    {
        private ProcessDialog EditProcessForm;
        private ProcessDialog AddProcessForm;
        private SettingsDialog SettingsForm;
        private AboutDialog AboutForm;
        private LogsDialog LogsForm;
        private bool inhibitAutoCheck;
        public ProcessManager ProcessManager { get; private set; }

        public MainDialog()
        {
            InitializeComponent();
            LogsForm = new LogsDialog();
            HandleCreated += OnHandleCreated;
            ProcessManager = new ProcessManager();
            ProcessListView.SetDoubleBuffered(true);
        }

        private void OnHandleCreated(object sender, EventArgs e)
        {
            Log.d("Main dialog handle created.");

            List<ProcessOptions> launchOptions = Settings.Instance.Read<List<ProcessOptions>>
                (Properties.Resources.SettingsProcessListNode);

            Log.d("Launch options parsed. Number of launch processes: {0}", launchOptions.Count);

            launchOptions.ForEach((opt) => { ProcessManager.Add(opt); });
            ProcessManager.PropertyChanged += OnProcessManagerPropertyChanged;
        }

        private void OnProcessManagerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "ProcessMap")
            {
                Settings.Instance.Write(Properties.Resources.SettingsProcessListNode, ProcessManager.ProcessOptionList);
                Settings.Instance.Save();
            }
        }

        private void OnProcessListViewResize(object sender, EventArgs e)
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
            foreach (ListViewItem item in ProcessListView.Items)
                if (!ProcessManager.Contains(item.Text))
                    item.Remove();

            ProcessManager.ProcessRunnerList.ForEach(p =>
            {
                if (ProcessListView.Items.ContainsKey(p.ProcessPath))
                {
                    ProcessListView.Items[p.ProcessPath].Checked = p.State != ProcessRunner.Status.Disabled;
                    ProcessListView.Items[p.ProcessPath].SubItems[1].Text = p.GetStatusString();
                }
                else
                {
                    ProcessListView.ItemChecked -= OnProcessListViewItemChecked;

                    ListViewItem item = new ListViewItem();

                    item.Checked = p.State != ProcessRunner.Status.Disabled;
                    item.Text = p.ProcessPath;
                    item.Name = p.ProcessPath;
                    item.SubItems.Add(p.State.ToString());

                    ProcessListView.Items.Add(item);
                    ProcessListView.ItemChecked += OnProcessListViewItemChecked;
                }
            });
        }

        private void OnSettingsButtonClick(object sender, EventArgs e)
        {
            HandleDialogRequest(ref SettingsForm);
        }

        private void OnAddButtonClick(object sender, EventArgs e)
        {
            HandleDialogRequest(ref AddProcessForm);
        }

        private void OnAboutButtonClick(object sender, EventArgs e)
        {
            HandleDialogRequest(ref AboutForm);
        }

        private void OnLogButtonClick(object sender, EventArgs e)
        {
            HandleDialogRequest(ref LogsForm);
        }

        private void OnDeleteButtonClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ProcessListView.SelectedItems)
                ProcessManager.Delete(item.Text);

            UpdateProcessList();
        }

        private void OnProcessListViewItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked == (ProcessManager.Get(e.Item.Text).State != ProcessRunner.Status.Disabled))
                return;

            if (e.Item.Checked)
                ProcessManager.Get(e.Item.Text).RestoreState();
            else
                ProcessManager.Get(e.Item.Text).State = ProcessRunner.Status.Disabled;
        }

        private void OnMainDialogUpdateTimerTick(object sender, EventArgs e)
        {
            ProcessManager.ProcessRunnerList.ForEach(p => p.Monitor());
            UpdateProcessList();
        }

        #region Context menu event callbacks

        private void OnContextMenuEditButtonClick(object sender, EventArgs e)
        {
            if (ProcessListView.FocusedItem == null)
                return;

            if (!ProcessManager.Contains(ProcessListView.FocusedItem.Text))
                return;

            ProcessRunner process = ProcessManager.Get(ProcessListView.FocusedItem.Text);

            if (EditProcessForm == null || EditProcessForm.IsDisposed)
            {
                EditProcessForm = new ProcessDialog(process.ProcessOptions);
                EditProcessForm.Owner = this;
            }

            if (!EditProcessForm.Visible)
                EditProcessForm.Show();
            else
                EditProcessForm.Focus();
        }

        private void OnContextMenuDeleteButtonClick(object sender, EventArgs e)
        {
            if (ProcessListView.FocusedItem == null)
                return;

            ProcessManager.Delete(ProcessListView.FocusedItem.Text);
            UpdateProcessList();
        }

        private void OnContextMenuDisableButtonClick(object sender, EventArgs e)
        {
            if (ProcessListView.FocusedItem == null)
                return;

            ProcessManager.Get(ProcessListView.FocusedItem.Text).State = ProcessRunner.Status.Disabled;
        }

        private void OnContextMenuShowClick(object sender, EventArgs e)
        {
            if (ProcessListView.FocusedItem == null)
                return;

            ProcessManager.Get(ProcessListView.FocusedItem.Text).BringToFront();
        }

        private void OnContextMenuStopClick(object sender, EventArgs e)
        {
            if (ProcessListView.FocusedItem == null)
                return;

            ProcessManager.Get(ProcessListView.FocusedItem.Text).Stop();
        }

        private void OnContextMenuStartClick(object sender, EventArgs e)
        {
            if (ProcessListView.FocusedItem == null)
                return;

            ProcessRunner process = ProcessManager.Get(ProcessListView.FocusedItem.Text);

            if (process.State != ProcessRunner.Status.Running)
                process.Start();
        }

        #endregion

        #region Drag and drop file support

        private void OnProcessListViewDragDrop(object sender, DragEventArgs e)
        {
            string[] dragged_files = e.Data.GetData(DataFormats.FileDrop, false) as string[];

            if (dragged_files == null)
                return;

            foreach (string dragged_file in dragged_files)
            {
                if (dragged_file.IsExecutable() &&
                    !ProcessManager.Contains(dragged_file))
                {
                    ProcessOptions opts = new ProcessOptions();

                    opts.Path = dragged_file;
                    opts.WorkingDirectory = Path.GetDirectoryName(opts.Path);

                    ProcessManager.Add(opts);
                }
            }

            UpdateProcessList();
        }

        private void OnProcessListViewDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        #endregion

        #region Disable auto-check feature of ListView on double-click

        private void OnProcessListViewMouseUp(object sender, MouseEventArgs e)
        {
            inhibitAutoCheck = false;
        }

        private void OnProcessListViewMouseDown(object sender, MouseEventArgs e)
        {
            inhibitAutoCheck = true;
        }

        private void OnProcessListViewItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (inhibitAutoCheck)
                e.NewValue = e.CurrentValue;
        }

        #endregion

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

            ProcessManager = null;
            EditProcessForm = null;
            AddProcessForm = null;
            SettingsForm = null;
            AboutForm = null;
            LogsForm = null;
        }
    }
}
