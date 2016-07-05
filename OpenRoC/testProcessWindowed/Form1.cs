namespace testProcessWindowed
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Shown += OnShown;
        }

        private void OnShown(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args != null && args.Length > 1)
            {
                bool unresponsive;
                if (bool.TryParse(args[1], out unresponsive) && unresponsive)
                    Thread.Sleep(TimeSpan.FromMinutes(30));
            }
        }
    }
}
