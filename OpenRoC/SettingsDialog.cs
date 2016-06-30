namespace oroc
{
    using System.Windows.Forms;

    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
            SetupDataBindings();
        }

        private void SetupDataBindings()
        {
            StartMinimizedCheckBox.DataBindings.Add(new Binding("Checked", Settings.Instance, "IsStartMinimizedEnabled"));
            SingleInstanceCheckBox.DataBindings.Add(new Binding("Checked", Settings.Instance, "IsSingleInsntaceEnabled"));
        }
    }
}
