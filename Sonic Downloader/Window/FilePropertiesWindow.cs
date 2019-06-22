using Sonic.Downloader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic_Downloader.Window
{
    public partial class FilePropertiesWindow : Form
    {
        private Downloader downloader;
        public Downloader Downloader
        {
            get
            {
                return downloader;
            }
            set
            {
                downloader = value;
                UpdateInfo();
            }
        }
        public FilePropertiesWindow()
        {
            InitializeComponent();
        }
        void UpdateInfo()
        {
            label15.Text = downloader.File.FileName;
            textBox1.Text = downloader.File.URL;
            label11.Text = NetworkHelper.DataFormatter(downloader.File.Downloaded);
            label14.Text = "No";

            if (downloader.File.DownloadType == DownloadTypes.SinglePartUnknownSize)
            {
                label9.Text = "Unknown";
                label10.Text = "Unknown";
            }
            else
            {
                label9.Text = downloader.File.Percent.ToString()+"%";
                label10.Text = NetworkHelper.DataFormatter(downloader.File.Size);
                if(downloader.File.DownloadType==DownloadTypes.MultiplePartResumable)
                label14.Text = "Yes";
            }

            if(!downloader.File.Completed)
            {
                button1.Visible = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(File.Exists(downloader.File.FullFilePath))
            {
                string cmd = "explorer.exe";
                string args = "/select, " + downloader.File.FullFilePath;
                Process.Start(cmd, args);
            }
            else
            {
                MessageBox.Show("File is no longer available there", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
            Close();
        }
    }
}
