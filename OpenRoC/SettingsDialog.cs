namespace oroc
{
    using System.Windows.Forms;

    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
            SetupDataBindings();

            HandleCreated += OnSettingsDialogHandleCreated;
        }

        private void OnSettingsDialogHandleCreated(object sender, System.EventArgs e)
        {
            if (!(Owner is MainDialog))
            {
                Log.e("Owner of Settings Window is not the right type.");
                return;
            }

            MainDialog main_dialog = Owner as MainDialog;

            main_dialog.SetStatusBarText(SingleInstanceCheckBox, "Allow only one instance of OpenRoC to run on this computer.");
            main_dialog.SetStatusBarText(StartMinimizedCheckBox, "Start OpenRoC minimized, in task-bar next time it launches.");

            SyncCheckedStates();
        }

        private void SetupDataBindings()
        {
            StartMinimizedCheckBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.IsStartMinimizedEnabled));
            SingleInstanceCheckBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.IsSingleInsntaceEnabled));
            HttpInterfaceEnabledCheckBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.IsWebInterfaceEnabled));
            HttpUrlTextBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.WebInterfaceAddress));
            SensuInterfaceEnabledCheckBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.IsSensuInterfaceEnabled));
            SensuHostTextBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.SensuInterfaceHost));
            SensuPortTextBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.SensuInterfacePort));
        }

        private void OnHttpInterfaceEnabledCheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            if (HttpUrlTextBox.Enabled != HttpInterfaceEnabledCheckBox.Checked)
                HttpUrlTextBox.Enabled = HttpInterfaceEnabledCheckBox.Checked;
        }

        private void OnSensuInterfaceEnabledCheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            if (SensuHostTextBox.Enabled != SensuInterfaceEnabledCheckBox.Checked)
                SensuHostTextBox.Enabled = SensuInterfaceEnabledCheckBox.Checked;

            if (SensuPortTextBox.Enabled != SensuInterfaceEnabledCheckBox.Checked)
                SensuPortTextBox.Enabled = SensuInterfaceEnabledCheckBox.Checked;
        }

        private void SyncCheckedStates()
        {
            OnHttpInterfaceEnabledCheckBoxCheckedChanged(this, null);
            OnSensuInterfaceEnabledCheckBoxCheckedChanged(this, null);
        }
    }
}
