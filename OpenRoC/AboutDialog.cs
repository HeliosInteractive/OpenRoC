namespace oroc
{
    using System.Diagnostics;
    using System.Windows.Forms;
    
    public partial class AboutDialog : Form
    {

        public AboutDialog()
        {
            InitializeComponent();
        }

        private void OnAboutRichTextBoxLinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText).Dispose();
        }
    }
}
