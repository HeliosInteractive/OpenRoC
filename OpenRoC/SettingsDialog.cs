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
            SensuInterfaceEnabledCheckBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.IsSensuInterfaceEnabled));
            SensuHostTextBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.SensuInterfaceHost));
            SensuPortTextBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.SensuInterfacePort));
            SensuTTLTextBox.SetupDataBind(Settings.Instance, nameof(Settings.Instance.SensuInterfaceTTL));
        }

        private void OnSensuInterfaceEnabledCheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            if (SensuHostTextBox.Enabled != SensuInterfaceEnabledCheckBox.Checked)
                SensuHostTextBox.Enabled = SensuInterfaceEnabledCheckBox.Checked;

            if (SensuPortTextBox.Enabled != SensuInterfaceEnabledCheckBox.Checked)
                SensuPortTextBox.Enabled = SensuInterfaceEnabledCheckBox.Checked;

            if (SensuTTLTextBox.Enabled != SensuInterfaceEnabledCheckBox.Checked)
                SensuTTLTextBox.Enabled = SensuInterfaceEnabledCheckBox.Checked;
        }

        private void SyncCheckedStates()
        {
            OnSensuInterfaceEnabledCheckBoxCheckedChanged(this, null);
        }
    }
}
