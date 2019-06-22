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
    public partial class DownloadNowWindow : Form
    {
        private string url;
        public string URL
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                textBox1.Text = value;
            }
        }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        private string fullFilePath;
        public string FullFilePath
        {
            get
            {
                return fullFilePath;
            }
            set
            {
                fullFilePath = value;
                NameSplitter(value);
                textBox2.Text = value;
            }
        }
        private long fileSize;
        public long FileSize
        {
            get
            {
                return fileSize;
            }
            set
            {
                fileSize = value;
                if(value==-1)
                {
                    label4.Text = "Unknown";
                }
                else
                label4.Text = NetworkHelper.DataFormatter(value);
            }
        }

        public DownloadNowWindow()
        {
            InitializeComponent();
            button1.Select();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FullFilePath = textBox2.Text = saveFileDialog1.FileName;
            }
            
            
        }
        private void NameSplitter(string Name)
        {
            FileName = System.IO.Path.GetFileName(Name);
            FilePath = System.IO.Path.GetDirectoryName(Name);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
