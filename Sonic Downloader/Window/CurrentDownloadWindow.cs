using Sonic.Downloader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic_Downloader.Window
{
    public partial class CurrentDownloadWindow : Form
    {
        Downloader downloader;
        public Downloader Downloader
        {
            get
            {
                return downloader;
            }
            set
            {
                downloader = value;
                downloader.OnProgress += Downloader_OnProgress;
                downloader.OnDownloadFinished += Downloader_OnProgress;
            }
        }

        public CurrentDownloadWindow()
        {
            InitializeComponent();
        }
        private void Downloader_OnProgress(object sender, ProgressEventArgs e)
        {
            Invoke((MethodInvoker)delegate ()
            {
                UpdateProgress(e.File);
            });
        }

        public void UpdateProgress(Downloadable File)
        {
            label8.Text = File.URL;
            label9.Text = downloader.DownloadStatus.ToString();
            label11.Text = NetworkHelper.DataFormatter(File.Downloaded);
            label12.Text = NetworkHelper.DataFormatter(File.TransferRate) + "/Sec";
            label14.Text = "No";
            Text = downloader.File.FileName;


            if (File.DownloadType != DownloadTypes.SinglePartUnknownSize)
            {
                label10.Text = NetworkHelper.DataFormatter(File.Size);
                label13.Text = NetworkHelper.TimeLeft(File.TransferRate, File.Downloaded, File.Size);

                progressBar1.Maximum =(int) File.Size;
                progressBar1.Value = (int) File.Downloaded;

                if (File.DownloadType == DownloadTypes.MultiplePartResumable)
                {
                    label14.Text = "Yes";
                    button1.Enabled = true;

                }
                else
                {
                    button1.Enabled = false;
                }
            }
            else
            {
                button1.Enabled = false;
                label10.Text = File.Size.ToString();
                label13.Text = "Undefined";

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(downloader.File.DownloadType==DownloadTypes.MultiplePartResumable)
            {
                if(downloader.DownloadStatus==Downloader.Status.Downloading)
                {
                    button1.Text = "Resume";
                    downloader.Pause();
                }
                else
                {
                    button1.Text = "Pause";
                    downloader.Download();
                }
            }
        }
        private async  void CurrentDownloadWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            downloader.OnProgress -= Downloader_OnProgress;
            downloader.OnDownloadFinished -= Downloader_OnProgress;
            await Task.Delay(1000);
        }
    }
}
