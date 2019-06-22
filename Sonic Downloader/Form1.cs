using Sonic.Downloader;
using Sonic_Downloader.Window;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sonic_Downloader
{
    public partial class Form1 : Form
    {

        public List<Downloader> DownloaderList = new List<Downloader>();
        public Form1()
        {
            InitializeComponent();
        }

        private void AddURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddURL();
        }
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            AddURL();
        }

        private void AddURL()
        {
            AddURLWindow auw = new AddURLWindow();
            DialogResult res=auw.ShowDialog();
            if(res==DialogResult.OK)
            {
                Downloadable file = new Downloadable() { URL = auw.URL };
                HeaderParser parser = new HeaderParser(file);
                parser.OnParseSuccess += Parser_OnParseSuccess;
                parser.OnError += Down_OnError;
                parser.ParseHeader();            
            }
        }

        private void Parser_OnParseSuccess(object sender, Downloadable File)
        {
            Invoke((MethodInvoker)delegate ()
            {
                DownloadNowWindow dnw = new DownloadNowWindow();
                dnw.URL = File.URL;
                dnw.FullFilePath = File.FullFilePath;
                dnw.FileName = File.FileName;
                dnw.FilePath = File.FilePath;
                dnw.FileSize = File.Size;
                var res = dnw.ShowDialog();
                if (res == DialogResult.OK)
                {
                    File.FileName = dnw.FileName;
                    File.FilePath = dnw.FilePath;

                    Downloader downloader = new Downloader();
                    downloader.File = File;
                    downloader.OnProgress += Downloader_OnProgress;
                    downloader.OnDownloadFinished += Downloader_OnProgress;
                    downloader.OnError += Down_OnError;
                    DownloaderList.Add(downloader);
                    downloader.Download();
                }
            });
            
        }

        private void Downloader_OnProgress(object sender, ProgressEventArgs e)
        {
            Invoke((MethodInvoker)delegate ()
            {
                UpdateChangesInGrid();
                ToggleButtons(sender as Downloader);
            });
        }

        private void UpdateChangesInGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add(new DataGridViewColumn()
            {
                HeaderText = "File Name",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 300
            });
            dataGridView1.Columns.Add(new DataGridViewColumn()
            {
                HeaderText = "File Size",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 140
            });
            dataGridView1.Columns.Add(new DataGridViewColumn()
            {
                HeaderText = "Downloaded",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 140
            });
            dataGridView1.Columns.Add(new DataGridViewColumn()
            {
                HeaderText = "Transfer Rate",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 140
            });
            dataGridView1.Columns.Add(new DataGridViewColumn()
            {
                HeaderText = "Completed",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 80
            });
            dataGridView1.Columns.Add(new DataGridViewColumn()
            {
                HeaderText = "Description",
                CellTemplate = new DataGridViewTextBoxCell(),
                Width = 500
            });


            foreach (Downloader downloader in DownloaderList)
            {
                object[] rows = new object[]
                {
                    downloader.File.FileName,
                    NetworkHelper.DataFormatter(downloader.File.Size),
                    NetworkHelper.DataFormatter(downloader.File.Downloaded),
                    NetworkHelper.DataFormatter(downloader.File.TransferRate),
                    downloader.File.Percent,
                    downloader.File.Description
                };
                dataGridView1.Rows.Add(rows);
            }

        }

        private void DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
                return;
            int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0)
            {
                ToggleButtons(DownloaderList[ind]);

                if (DownloaderList[ind].DownloadStatus == Downloader.Status.Downloading)
                {
                    CurrentDownloadWindow cdw = new CurrentDownloadWindow();
                    cdw.Downloader = DownloaderList[ind];
                    cdw.UpdateProgress(DownloaderList[ind].File);
                    cdw.ShowDialog();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLoadFiles.DownloadableConfigPath="files.json";
            SaveLoadFiles.SaveFiles(FromDownloader());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SaveLoadFiles.DownloadableConfigPath = "files.json";
            var files =SaveLoadFiles.LoadFiles();
            if (files != null)
                ToDownloader(files); 
            UpdateChangesInGrid();
        }

        private void ResumeToolStrip_Click(object sender, EventArgs e)
        {
            Resume();
        }
        void Resume()
        {

            if (dataGridView1.Rows.Count <= 0)
                return;
            int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0)
            {
                DownloaderList[ind].Download();
            }
        }
        List<Downloadable> FromDownloader()
        {
            List<Downloadable> list = new List<Downloadable>();
            foreach (Downloader d in DownloaderList)
            {
                list.Add(d.File);
            }
            return list;
        }
        void ToDownloader(List<Downloadable> list)
        {
            foreach (Downloadable d in list)
            {
                Downloader down = new Downloader(d);
                down.OnProgress += Downloader_OnProgress;
                down.OnDownloadFinished += Downloader_OnProgress;
                down.OnError += Down_OnError;
                DownloaderList.Add(down);
            }

        }

        private void Down_OnError(object sender, Exception e)
        {
            MessageBox.Show(e.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count>0)
            {
                int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);

                Downloader selDown = DownloaderList[ind];
                ToggleButtons(selDown);
            }
        }
        private void ToggleButtons(Downloader downloader)
        {
            if(dataGridView1.Rows.Count<=0)
            {
                return;
            }

            if (downloader.File.Completed)
            {
                resumeToolStrip.Enabled = false;
                stopToolStrip.Enabled = false;
                downloadNowToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = false;
            }
            else
            {
                if (downloader.DownloadStatus == Downloader.Status.Downloading)
                {
                    stopToolStrip.Enabled = true;
                    resumeToolStrip.Enabled = false;
                    downloadNowToolStripMenuItem.Enabled = false;
                    stopToolStripMenuItem.Enabled = true;
                }
                else
                {
                    resumeToolStrip.Enabled = true;
                    downloadNowToolStripMenuItem.Enabled = true;
                    stopToolStrip.Enabled = false;
                    stopToolStripMenuItem.Enabled = false;


                }
            }
        }
        private void Pause()
        {
            if (dataGridView1.Rows.Count <= 0)
                return;
            int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0)
            {
                DownloaderList[ind].Pause();
            }
        }
        private void ReDownload()
        {
            if (dataGridView1.Rows.Count <= 0)
                return;
            int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0)
            {
                Downloadable file = new Downloadable() { URL = DownloaderList[ind].File.URL };
                DownloaderList.Remove(DownloaderList[ind]);
                HeaderParser parser = new HeaderParser(file);
                parser.OnParseSuccess += parser_Redownload;
                parser.OnError += Down_OnError;
                parser.ParseHeader();
            }
        }
        void parser_Redownload(object sender,Downloadable file)
        {
            Downloader downloader = new Downloader(file);
            downloader.OnError += Down_OnError;
            downloader.OnDownloadFinished += Downloader_OnProgress;
            downloader.OnProgress += Downloader_OnProgress;
            DownloaderList.Add(downloader);
        }
        private void StopToolStrip_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void DataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
                return;
            int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0)
            {
                ToggleButtons(DownloaderList[ind]);
            }
        }

        private void DownloadNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resume();
        }

        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pause();
        }
        private void Remove()
        {
            if (dataGridView1.Rows.Count <= 0)
                return;
            int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0)
            {
                var res=MessageBox.Show($"Do you want to remove downloaded file {DownloaderList[ind].File.FileName} as well from disk.", "Warning", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                switch(res)
                {
                    case DialogResult.Yes:
                        NetworkHelper.DeleteFile(DownloaderList[ind].File.FullFilePath);
                        break;
                    case DialogResult.Cancel:
                        return;
                }

                DownloaderList.Remove(DownloaderList[ind]);

                UpdateChangesInGrid();

            }
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void DeleteToolStrip_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void RedownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReDownload();
        }
    }
}
