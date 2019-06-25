using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sonic.YoutubeStreams;
using YoutubeExplode.Models.MediaStreams;
using static Sonic.YoutubeStreams.YoutubeParser;

namespace Sonic_Downloader.Window
{
    public partial class YoutubeDownloaderWindow : Form
    {
        YoutubeParser p = new YoutubeParser();
        VideoInfo Info = null;

        private List<MuxedStreamInfo> muxedStreamInfos = new List<MuxedStreamInfo>();
        private List<AudioStreamInfo> audioStreamInfos = new List<AudioStreamInfo>();
        private List<VideoStreamInfo> videoStreamInfos = new List<VideoStreamInfo>();

        public string URL { get; set; }
        public string FileName { get; set; }

        public YoutubeDownloaderWindow()
        {
            InitializeComponent();
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!isWorking)
            {
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;
                isWorking = true;
                GetInfo();

            }


        }
        bool isWorking = false;
        async void GetInfo()
        {
            muxedStreamInfos.Clear();
            comboBox1.Items.Clear();
            audioStreamInfos.Clear();
            comboBox2.Items.Clear();
            videoStreamInfos.Clear();
            comboBox3.Items.Clear();

            try
            {
                Info = await p.GetVideoInfo(textBox1.Text);
                foreach (var p in Info.MediaStreamInfoSet.Muxed)
                {
                    muxedStreamInfos.Add(p);
                    comboBox1.Items.Add(p.Resolution + " / " + p.VideoEncoding + " / " + p.AudioEncoding);
                    comboBox1.SelectedIndex = 0;

                }
                foreach (var p in Info.MediaStreamInfoSet.Audio)
                {
                    audioStreamInfos.Add(p);
                    comboBox2.Items.Add(p.Bitrate + " bits/sec "  + "  " + p.AudioEncoding);
                    comboBox2.SelectedIndex = 0;
                }
                foreach (var p in Info.MediaStreamInfoSet.Video)
                {
                    videoStreamInfos.Add(p);
                    comboBox3.Items.Add(p.Resolution + " bits/sec " + "  " + p.VideoEncoding);
                    comboBox3.SelectedIndex = 0;
                }

                var imageURL = Info.Video.Thumbnails.HighResUrl;
                label4.Text =FileName= Info.Video.Title;
                label4.Visible = true;
                label5.Visible = true;
                label7.Visible = true;
                label7.Text = Info.Video.Author;
                label6.Visible = true;
                label3.Visible = true;
                label8.Visible = true;
                label11.Visible = true;
                label9.Visible = true;

                label10.Visible = true;
                comboBox2.Visible = true;
                button3.Visible = true;
                comboBox3.Visible = true;
                button4.Visible = true;

                label9.Text = Info.Video.Duration.ToString();
                comboBox1.Visible = true;
                button2.Visible = true;
                pictureBox1.Load(imageURL);

            }
            catch(Exception e)
            {
                toolStripProgressBar1.Visible = false;
                toolStripStatusLabel1.Visible = false;
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isWorking = false;

            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        private void YoutubeDownloaderWindow_Activated(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if(Sonic.Downloader.UrlVerification.Verify(Clipboard.GetText()))
                textBox1.Text = Clipboard.GetText();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex<0)
            {
                MessageBox.Show("Select a resolution", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                URL = muxedStreamInfos[comboBox1.SelectedIndex].Url;
                FileName+="."+muxedStreamInfos[comboBox1.SelectedIndex].Container.GetFileExtension();
                DialogResult=DialogResult.OK;
                this.Close();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Select a bitrate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                URL = audioStreamInfos[comboBox2.SelectedIndex].Url;
                FileName += "."+audioStreamInfos[comboBox2.SelectedIndex].Container.GetFileExtension();
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex < 0)
            {
                MessageBox.Show("Select a resolution", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                URL = videoStreamInfos[comboBox2.SelectedIndex].Url;
                FileName += "." + videoStreamInfos[comboBox2.SelectedIndex].Container.GetFileExtension();
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
