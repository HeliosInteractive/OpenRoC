namespace oroc
{
    using System.Windows.Forms;
    using System.ComponentModel;

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
            MainDialog main_dialog = Owner as MainDialog;

            main_dialog.SetStatusBarText(SingleInstanceCheckBox, "Allow only one instance of OpenRoC to run on this computer.");
            main_dialog.SetStatusBarText(StartMinimizedCheckBox, "Start OpenRoC minimized, in task-bar next time it launches.");
            main_dialog.SetStatusBarText(SensuInterfaceEnabledCheckBox, "Enable Sensu 'client socket' support (applicable next time OpenRoC launches).");
            main_dialog.SetStatusBarText(SensuHostTextBox, "Sensu UDP 'client' host (applicable next time OpenRoC launches).");
            main_dialog.SetStatusBarText(SensuPortTextBox, "Sensu UDP 'client' port (applicable next time OpenRoC launches).");
            main_dialog.SetStatusBarText(SensuTTLTextBox, "Sensu checks TTL in seconds. Interval is 80% of TTL and timeout is equal to TTL.");

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

            Settings.Instance.PropertyChanged += OnSettingsInstancePropertyChanged;
        }

        private void OnSettingsInstancePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.Instance.SensuInterfaceTTL))
                (Owner as MainDialog).SetSensuInterfaceUpdateTimerInterval(Settings.Instance.SensuInterfaceTTL);
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
