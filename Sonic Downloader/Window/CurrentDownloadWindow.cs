using Sonic.Downloader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            textBox1.Text = File.URL;
            label9.Text = downloader.DownloadStatus.ToString();
            label11.Text = NetworkHelper.DataFormatter(File.Downloaded)+"  ( "+File.Percent.ToString("0.00")+"% )";
            label12.Text = NetworkHelper.DataFormatter(File.TransferRate) + "/Sec";
            label14.Text = "No";
            Text = downloader.File.FileName;

            if (File.DownloadType!=DownloadTypes.MultiplePartResumable)
                rangeProgressBar1.Visible = false;

            if (File.DownloadType != DownloadTypes.SinglePartUnknownSize)
            {
                label10.Text = NetworkHelper.DataFormatter(File.Size);
                label13.Text = NetworkHelper.TimeLeft(File.TransferRate, File.Downloaded, File.Size);

                progressBar1.Maximum =(int) File.Size;
                progressBar1.Value = (int) File.Downloaded;

                if (File.DownloadType == DownloadTypes.MultiplePartResumable)
                {
                    label14.Text = "Yes";
                    button1.Visible = true;
                    if(rangeProgressBar1.Visible==false)
                    rangeProgressBar1.Visible = true;
                    if(File.RangeList!=null)
                    rangeProgressBar1.Ranges = GetRangeEquivalent(File.RangeList);
                    rangeProgressBar1.Invalidate();
                }
                else
                {
                    button1.Visible = false;
                }
            }
            else
            {
                button1.Enabled = false;
                label10.Text = File.Size.ToString();
                label13.Text = "Undefined";

            }
        }

        private List<RangeProgressBar.Range> GetRangeEquivalent(List<Sonic.Downloader.Range> ranges)
        {
            List<RangeProgressBar.Range> list=new List<RangeProgressBar.Range>();
            foreach(Range r in ranges)
            {
                RangeProgressBar.Range rn = new RangeProgressBar.Range();
                rn.Start = r.Start;
                rn.End = r.End;
                rn.Downloaded = r.Downloaded;

                list.Add(rn);
            }
            return list;
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
