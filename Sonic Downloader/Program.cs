using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Sonic_Downloader
{
    static class Program
    {
        static Form1 MainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] Arguments)
        {
            bool bInstanceFlag;
            Mutex mutex = new Mutex(true, "Sonic Downloader", out bInstanceFlag);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!bInstanceFlag)
            {
                MainForm = new Form1();
                SingleInstanceApplication.Run(MainForm, NewInstanceHandler);
            }
            else
            {
                MainForm = new Form1();
                SingleInstanceApplication.Run(MainForm, NewInstanceHandler);
                MainForm.notifyIcon1.Visible = false;
                MainForm.Close();
            }
        }
        public static void NewInstanceHandler(object sender, StartupNextInstanceEventArgs e)
        {
            if (e.CommandLine.Count > 1)
                MainForm.StartupArgs = e.CommandLine[1];
            e.BringToForeground = false;
        }
        public class SingleInstanceApplication : WindowsFormsApplicationBase
        {
            private SingleInstanceApplication()
            {
                base.IsSingleInstance = true;
            }
            public static void Run(Form f, StartupNextInstanceEventHandler startupHandler)
            {
                SingleInstanceApplication app = new SingleInstanceApplication();
                app.MainForm = f;
                app.StartupNextInstance += startupHandler;
                app.Run(Environment.GetCommandLineArgs());
            }
        }
    }
}
