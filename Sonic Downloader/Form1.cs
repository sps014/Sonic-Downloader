using Sonic.Downloader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic_Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Tasker();
        }
         void Tasker()
        {
            Downloadable d = new Downloadable();
            //d.URL = "http://localhost:5555/The%20Himalayas%20from%2020000%20ft%20(Ultra%20HD%20Footage).mp4strm";
            d.URL = "https://slproweb.com/download/Win32OpenSSL_Light-1_0_2s.exe";
            //d.URL = "https://codeload.github.com/qt-labs/qthttpserver/zip/master";
            //d.URL = "https://c2rsetup.officeapps.live.com/c2r/downloadEdge.aspx?ProductreleaseID=Edge&platform=Default&version=Edge&Channel=Dev&language=en-us&Consent=0&IID=562f0e8b-b57d-5bbe-b88e-87b10c5ea9aa";
            HeaderParser parser = new HeaderParser(d);
            parser.OnParseSuccess += Parser_OnParseSuccess;
            parser.ParseHeader();
            
            
        }

        private void Dn_OnProgress(object sender, ProgressEventArgs e)
        {
            Invoke((MethodInvoker)delegate ()
            {
                Text = NetworkHelper.DataFormatter(e.File.Downloaded)
                +$"@{NetworkHelper.DataFormatter(e.File.TransferRate)}"+"/Sec";
                progressBar1.Maximum = (int)e.File.Size;
                progressBar1.Value = (int)e.File.Downloaded;

                var obj = e as FinishedEventArgs;

                if(obj!=null)
                {
                    MessageBox.Show(watch.ElapsedMilliseconds.ToString());
                }
            });
        }
        Downloader  dn;
        Stopwatch watch;
        private void Parser_OnParseSuccess(object sender, Downloadable d)
        {
            dn= new Downloader(d);
            dn.OnProgress += Dn_OnProgress;
            dn.OnDownloadFinished += Dn_OnProgress;
            watch = new Stopwatch();
            watch.Start();
            Task.Run(() =>
            {
                dn.Download();
            });
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dn.Pause();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            dn.Download();
        }
    }
}
