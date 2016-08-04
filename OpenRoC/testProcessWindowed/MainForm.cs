namespace testProcessWindowed
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Shown += OnShown;
        }

        private void OnShown(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();

            var unresponsive = false;
            var respwaittime = (int)TimeSpan.FromMinutes(30).TotalMilliseconds;

            if (args != null)
            {
                if (args.Length > 1)
                    unresponsive = (args[1] == nameof(unresponsive));

                if (args.Length > 2)
                    respwaittime = (int)TimeSpan.FromSeconds(int.Parse(args[2])).TotalMilliseconds;
            }

            if (unresponsive)
                Thread.Sleep(respwaittime);
        }
    }
}
