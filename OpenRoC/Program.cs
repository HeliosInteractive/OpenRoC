namespace oroc
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainDialog());
        }

        public static string Directory
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }
    }
}
