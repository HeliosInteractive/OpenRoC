namespace oroc
{
    using liboroc;

    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Windows.Forms.DataVisualization.Charting;

    using Nancy.Hosting.Self;

    public partial class MainDialog : Form
    {
        private Metrics.Manager metricsManager;
        private ProcessDialog editProcessForm;
        private ProcessDialog addProcessForm;
        private SettingsDialog settingsForm;
        private AboutDialog aboutForm;
        private LogsDialog logsForm;
        private NancyHost webHost;

        private Series CpuChart;
        private Series GpuChart;
        private Series RamChart;

        public ProcessManager ProcessManager { get; private set; }
        private bool inhibitAutoCheck = false;

        public MainDialog()
        {
            InitializeComponent();
            logsForm = new LogsDialog();
            SetupMainDialogStatusTexts();
            HandleCreated += OnHandleCreated;
            ProcessManager = new ProcessManager();
            metricsManager = new Metrics.Manager();

            if (!ProcessListView.SetDoubleBuffered(true))
            {
                Log.w("Unable to set DoubleBuffer on ProcessListView. This may cause flickers.");
            }
            else
            {
                Log.d("ProcessListView is DoubleBuffer enabled.");
            }
        }

        private void OnHandleCreated(object sender, EventArgs e)
        {
            Log.d("Main dialog handle created.");

            List<ProcessOptions> launchOptions = Settings.Instance.Read<List<ProcessOptions>>
                (Properties.Resources.SettingsProcessListNode);

            Log.d("Launch options parsed. Number of launch processes: {0}", launchOptions.Count);

            launchOptions.ForEach((opt) => { ProcessManager.Add(opt); });
            ProcessManager.ProcessesChanged += OnProcessManagerPropertyChanged;

            if (Settings.Instance.IsWebInterfaceEnabled)
            {
                try
                {
                    webHost = new NancyHost(
                        new Uri(Settings.Instance.WebInterfaceAddress),
                        new WebInterfaceBootstrapper(ProcessManager, this),
                        new HostConfiguration { RewriteLocalhost = false });

                    webHost.Start();

                    Log.d("Web interface is available at {0}", Settings.Instance.WebInterfaceAddress);
                }
                catch (AutomaticUrlReservationCreationFailureException)
                {
                    Log.e("Web interface failed to start. Are you an administrator?");
                }
                catch (Exception ex)
                {
                    Log.e("Web interface failed to start: {0}", ex.Message);
                }
            }

            CpuChart = metricsChart.Series[nameof(CpuChart)];
            GpuChart = metricsChart.Series[nameof(GpuChart)];
            RamChart = metricsChart.Series[nameof(RamChart)];
        }

        private void OnProcessManagerPropertyChanged()
        {
            Settings.Instance.Write(Properties.Resources.SettingsProcessListNode, ProcessManager.ProcessOptionList);
            Settings.Instance.Save();
        }

        private void OnProcessListViewResize(object sender, EventArgs e)
        {
            if (ProcessListView.Columns.Count > 0)
                ProcessListView.AutoResizeColumn(
                    ProcessListView.Columns.Count - 1,
                    ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        public void UpdateProcessList()
        {
            foreach (ListViewItem item in ProcessListView.Items)
                if (!ProcessManager.Contains(item.Text))
                    item.Remove();

            ProcessManager.ProcessRunnerList.ForEach(p =>
            {
                if (ProcessListView.Items.ContainsKey(p.ProcessOptions.Path))
                {
                    ProcessListView.Items[p.ProcessOptions.Path].Checked = p.State != ProcessRunner.Status.Disabled;
                    ProcessListView.Items[p.ProcessOptions.Path].SubItems[1].Text = p.GetStateString();
                }
                else
                {
                    ListViewItem item = new ListViewItem();

                    item.Checked = p.State != ProcessRunner.Status.Disabled;
                    item.Text = p.ProcessOptions.Path;
                    item.Name = p.ProcessOptions.Path;
                    item.SubItems.Add(p.State.ToString());

                    p.StateChanged += () => { Log.i("Process {0} changed state to: {1}", p.ProcessOptions.Path, p.State); };
                    p.OptionsChanged += () => { Log.d("Process changed options to: {0}", p.ProcessOptions.ToJson()); };
                    p.ProcessCrashed += () =>
                    {
                        Log.e("Process {0} crashed or stopped.", p.ProcessOptions.Path);

                        if (p.ProcessOptions.ScreenShotEnabled)
                            TakeScreenShot();
                    };

                    ProcessListView.Items.Add(item);
                }
            });
        }

        private void OnSettingsButtonClick(object sender, EventArgs e)
        {
            HandleDialogRequest(ref settingsForm);
        }

        private void OnAddButtonClick(object sender, EventArgs e)
        {
            HandleDialogRequest(ref addProcessForm);
        }

        private void OnAboutButtonClick(object sender, EventArgs e)
        {
            HandleDialogRequest(ref aboutForm);
        }

        private void OnLogButtonClick(object sender, EventArgs e)
        {
            HandleDialogRequest(ref logsForm);
        }

        private void HandleDialogRequest<T>(ref T host) where T : Form, new()
        {
            if (host == null || host.IsDisposed)
            {
                host = new T();
                host.Owner = this;

                if (host.Handle == IntPtr.Zero)
                    Log.d("Forced handle to be created.");
            }

            if (!host.Visible)
            {
                host.Show();
                host.Focus();
            }
            else
            {
                host.Focus();
                return;
            }
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
            metricsManager.Update();
            UpdateProcessList();

            CpuChart?.Points.DataBindY(metricsManager.CpuSamples);
            GpuChart?.Points.DataBindY(metricsManager.GpuSamples);
            RamChart?.Points.DataBindY(metricsManager.RamSamples);
        }

        #region StatusBar text feature

        public void SetStatusBarText(Control control, string text)
        {
            control.MouseEnter += (s, e) => { StatusText.Text = text; };
            control.MouseLeave += (s, e) => { ResetStatusBarText(); };
        }

        public void SetStatusBarText(ToolStripItem control, string text)
        {
            control.MouseEnter += (s, e) => { StatusText.Text = text; };
            control.MouseLeave += (s, e) => { ResetStatusBarText(); };
        }

        public void ResetStatusBarText()
        {
            StatusText.Text = Properties.Resources.StatusTextDefaultString;
        }

        private void SetupMainDialogStatusTexts()
        {
            ResetStatusBarText();
            SetStatusBarText(AddButton, "Add a new process to monitor.");
            SetStatusBarText(DeleteButton, "Delete selected processes.");
            SetStatusBarText(SettingsButton, "Adjust OpenRoC settings.");
            SetStatusBarText(LogsButton, "Open logging history window.");
            SetStatusBarText(AboutButton, "Read about OpenRoC project.");
            SetStatusBarText(ContextMenuAddButton, "Add a new process.");
            SetStatusBarText(ContextMenuEditButton, "Edit the process.");
            SetStatusBarText(ContextMenuDeleteButton, "Delete selected processes.");
            SetStatusBarText(ContextMenuDisableButton, "Disable selected processes.");
            SetStatusBarText(ContextMenuStart, "Run selected processes if they are stopped.");
            SetStatusBarText(ContextMenuStop, "Stop selected processes if they are running.");
            SetStatusBarText(ContextMenuShow, "Attempt to bring the main Window of the selected processes to top.");
        }

        #endregion

        #region Start minimized support

        protected override void SetVisibleCore(bool value)
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
                base.SetVisibleCore(!Settings.Instance.IsStartMinimizedEnabled);
            }
            else
            {
                base.SetVisibleCore(value);
            }
        }

        #endregion

        #region Taskbar right-click context menu event callbacks

        private void OnTaskbarContextMenuToggleViewButtonClick(object sender, EventArgs e)
        {
            if (e is MouseEventArgs && (e as MouseEventArgs).Button != MouseButtons.Left)
                return;

            Visible = !Visible;

            if (Visible)
                Focus();
        }

        private void OnTaskbarContextMenuExitButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Process right-click context menu event callbacks

        private void OnContextMenuEditButtonClick(object sender, EventArgs e)
        {
            if (ProcessListView.FocusedItem == null)
                return;

            if (!ProcessManager.Contains(ProcessListView.FocusedItem.Text))
                return;

            ProcessRunner process = ProcessManager.Get(ProcessListView.FocusedItem.Text);

            if (editProcessForm == null || editProcessForm.IsDisposed)
            {
                editProcessForm = new ProcessDialog(process.ProcessOptions);
                editProcessForm.Owner = this;
            }

            if (!editProcessForm.Visible)
                editProcessForm.Show();
            else
                editProcessForm.Focus();
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
                if (!ProcessManager.Contains(dragged_file))
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

        #region ScreenShot support

        public void TakeScreenShot()
        {
            if (!Directory.Exists(Program.ScreenShotDirectory))
                Directory.CreateDirectory(Program.ScreenShotDirectory);

            using (var picture = Pranas.ScreenshotCapture.TakeScreenshot())
            {
                string name = Path.Combine(
                    Program.ScreenShotDirectory,
                    string.Format("{0}.png", DateTime.Now.ToFileTime()));

                picture.Save(name);
            }
        }

        #endregion

        private void DisposeAddedComponents()
        {
            metricsManager?.Dispose();
            ProcessManager?.Dispose();
            editProcessForm?.Dispose();
            addProcessForm?.Dispose();
            settingsForm?.Dispose();
            aboutForm?.Dispose();
            logsForm?.Dispose();
            webHost?.Dispose();

            metricsManager = null;
            ProcessManager = null;
            editProcessForm = null;
            addProcessForm = null;
            settingsForm = null;
            aboutForm = null;
            logsForm = null;
            webHost = null;
        }
    }
}
