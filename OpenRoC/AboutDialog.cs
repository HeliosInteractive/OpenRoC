namespace oroc
{
    using Properties;
    using System.Diagnostics;
    using System.Windows.Forms;

    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();

            AboutBrowser.DocumentText = Resources.about;
        }

        private void OnAboutBrowserNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.AbsoluteUri == "about:blank")
                e.Cancel = false;
            else
            {
                Process.Start(e.Url.AbsoluteUri).Dispose();
                e.Cancel = true;
            }
        }
    }
}
