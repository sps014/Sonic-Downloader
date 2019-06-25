using Sonic.Downloader;
using Sonic.HttpServer;
using Sonic_Downloader.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic_Downloader
{
    public partial class Form1 : Form
    {

        public List<Downloader> DownloaderList = new List<Downloader>();

        private string startupArgs="";
        public string StartupArgs
        {
            get
            {
                return startupArgs;
            }
            set
            {
                startupArgs = value;
                AddURL(startupArgs);
            }
        }

        public Form1()
        {
            InitializeComponent();

        }
        private void AddURL(string URL=null)
        {
            AddURLWindow auw = new AddURLWindow();
            if (URL != null)
                auw.URL = URL;
            DialogResult res = auw.ShowDialog();
            if (res == DialogResult.OK)
            {
                Downloadable file = new Downloadable() {
                URL = auw.URL,
                DegreeOfParallelism=(uint)SettingsWindow.GeneralSettings.MaxConnection,
                FilePath=SettingsWindow.GeneralSettings.StoragePath
                };
                HeaderParser parser = new HeaderParser(file);
                parser.OnParseSuccess += Parser_OnParseSuccess;
                parser.OnError += Down_OnError;
                parser.ParseHeader();
            }
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
        void Rename()
        {
            if (dataGridView1.Rows.Count <= 0)
                return;
            int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0)
            {
                Downloader d = DownloaderList[ind];
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "All Files|*.*";

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(d.File.FullFilePath))
                    {
                        File.Move(d.File.FilePath, sf.FileName);

                        d.File.FileName = System.IO.Path.GetFileName(sf.FileName);
                        d.File.FilePath = System.IO.Path.GetDirectoryName(sf.FileName);
                    }
                }
            }
        }
        private int GetDuplicateDownloaderIndex(string url)
        {
            int res = -1;

            int j = 0;
            foreach (Downloader d in DownloaderList)
            {
                if (d.File.URL == url)
                    return j;
                j++;
            }
            return res;
        }
        private void UpdateChangesInGrid()
        {
            int selectionIndex = -1;
            if (dataGridView1.Rows.Count > 0)
                selectionIndex = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);

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
                HeaderText = "Percent",
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
                bool isUnknown = downloader.File.DownloadType == DownloadTypes.SinglePartUnknownSize ? true : false;
                object[] rows = new object[]
                {
                    downloader.File.FileName,
                    isUnknown?"Unknown":NetworkHelper.DataFormatter(downloader.File.Size),
                    NetworkHelper.DataFormatter(downloader.File.Downloaded),
                    NetworkHelper.DataFormatter(downloader.File.TransferRate)+"/Sec",
                    isUnknown?"Unknown":downloader.File.Percent.ToString("0.00")+"%",
                    downloader.File.Description
                };
                dataGridView1.Rows.Add(rows);
            }
            if (selectionIndex >= 0)
            {
                if (dataGridView1.Rows.Count > selectionIndex)
                {
                    dataGridView1.Rows[selectionIndex].Selected = true;
                }
            }

        }

        private void SaveSettings()
        {
            SaveLoadFiles.DownloadableConfigPath = "files.json";
            SaveLoadFiles.SaveFiles(FromDownloader());
        }
        private void LoadSettings()
        {
            SaveLoadFiles.DownloadableConfigPath = "files.json";
            var files = SaveLoadFiles.LoadFiles();
            if (files != null)
                ToDownloader(files);
            UpdateChangesInGrid();
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
                down.Timeout = SettingsWindow.GeneralSettings.Timeout;

                down.OnProgress += Downloader_OnProgress;
                down.OnDownloadFinished += Downloader_OnProgress;
                down.OnError += Down_OnError;
                DownloaderList.Add(down);
            }

        }
        private void ToggleButtons(Downloader downloader)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                return;
            }

            if (downloader.File.Completed)
            {
                resumeToolStrip.Enabled = false;
                stopToolStrip.Enabled = false;
                downloadNowToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = false;
                openFolderToolStripMenuItem.Enabled = true;
                openToolStripMenuItem.Enabled = true;
                resumeToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem1.Enabled = false;
                moveRenameToolStripMenuItem.Enabled = true;
            }
            else
            {
                openFolderToolStripMenuItem.Enabled = false;
                openToolStripMenuItem.Enabled = false;

                if (downloader.DownloadStatus == Downloader.Status.Downloading)
                {
                    stopToolStrip.Enabled = true;
                    resumeToolStrip.Enabled = false;
                    downloadNowToolStripMenuItem.Enabled = false;
                    moveRenameToolStripMenuItem.Enabled = false;
                    stopToolStripMenuItem.Enabled = true;
                    stopToolStripMenuItem1.Enabled = true;
                    resumeToolStripMenuItem.Enabled = false;
                }
                else
                {
                    resumeToolStrip.Enabled = true;
                    resumeToolStripMenuItem.Enabled = true;
                    stopToolStripMenuItem1.Enabled = false;
                    moveRenameToolStripMenuItem.Enabled = true;
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
        private void ReDownload(string directURL=null,string FileName=null)
        {
            if (dataGridView1.Rows.Count <= 0 && directURL==null)
                return;

            int ind = -1;
            if(dataGridView1.Rows.Count>0)
            ind=dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0 ||directURL!=null)
            {
                if (ind >= 0)
                    DownloaderList[ind].Pause();

                Downloadable file = new Downloadable()
                {
                    URL = directURL==null?DownloaderList[ind].File.URL:directURL,
                    FileName=FileName==null?DownloaderList[ind].File.FileName:FileName,
                    DegreeOfParallelism =(uint)SettingsWindow.GeneralSettings.MaxConnection,
                    FilePath =SettingsWindow.GeneralSettings.StoragePath
                };
                if (FileName != null)
                    file.FileName = FileName;

                if(directURL==null)
                DownloaderList.Remove(DownloaderList[ind]);
                HeaderParser parser = new HeaderParser(file);
                parser.OnParseSuccess += parser_Redownload;
                parser.OnError += Down_OnError;
                parser.ParseHeader();
            }
        }
        private void ReDownload(Downloader d)
        {

            Downloadable file = new Downloadable() {
                URL = d.File.URL,
                DegreeOfParallelism = (uint)SettingsWindow.GeneralSettings.MaxConnection,
                FilePath = SettingsWindow.GeneralSettings.StoragePath
            };
            DownloaderList.Remove(d);
            HeaderParser parser = new HeaderParser(file);
            parser.OnParseSuccess += parser_Redownload;
            parser.OnError += Down_OnError;
            parser.ParseHeader();
        }
        private void Remove()
        {
            if (dataGridView1.Rows.Count <= 0)
                return;
            int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0)
            {
                var res = MessageBox.Show($"Do you want to remove downloaded file {DownloaderList[ind].File.FileName} as well from disk.", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (res)
                {
                    case DialogResult.Yes:
                        DownloaderList[ind].Pause();
                        NetworkHelper.DeleteFile(DownloaderList[ind].File.FullFilePath);
                        break;
                    case DialogResult.Cancel:
                        return;
                }

                DownloaderList.Remove(DownloaderList[ind]);

                UpdateChangesInGrid();

            }
        }
        private void OpenExplorerFile(string path)
        {
            if (File.Exists(path))
            {
                string cmd = "explorer.exe";
                string args = "/select, " + path;
                Process.Start(cmd, args);
            }
            else
            {
                MessageBox.Show("File is no longer available there", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void OpenExplorerFolder(string path)
        {
            if (Directory.Exists(path))
            {
                string cmd = "explorer.exe";
                string args = "/select, " + path;
                Process.Start(cmd, args);
            }
            else
            {
                MessageBox.Show("Directory is no longer available there", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowSettingsWindow()
        {
            SettingsWindow sw = new SettingsWindow();
            sw.ShowDialog();
        }
        private void AddURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddURL();
        }
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            AddURL();
        }


        private void Parser_OnParseSuccess(object sender, Downloadable File)
        {
            Invoke((MethodInvoker)delegate ()
            {
            if (GetDuplicateDownloaderIndex(File.URL) == -1)
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
                    downloader.Timeout = SettingsWindow.GeneralSettings.Timeout;
                    downloader.File = File;
                    downloader.OnProgress += Downloader_OnProgress;
                    downloader.OnDownloadFinished += Downloader_OnProgress;
                    downloader.OnError += Down_OnError;
                    DownloaderList.Add(downloader);
                    downloader.Download();
                }
            }
            else
            {
                var r = MessageBox.Show("Download File Already added , Do you want to over write it", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (r)
                {
                    case DialogResult.Yes:
                        ReDownload(DownloaderList[GetDuplicateDownloaderIndex(File.URL)]);
                            break;

                    }
                }
            });
            
        }
        private void parser_Redownload(object sender, Downloadable file)
        {
            Invoke((MethodInvoker)delegate ()
            {
                Downloader downloader = new Downloader(file);
                downloader.Timeout = SettingsWindow.GeneralSettings.Timeout;
                downloader.OnError += Down_OnError;
                downloader.OnDownloadFinished += Downloader_OnProgress;
                downloader.OnProgress += Downloader_OnProgress;
                DownloaderList.Add(downloader);
                downloader.Download();
                UpdateChangesInGrid();
            });

        }

        private void Downloader_OnProgress(object sender, ProgressEventArgs e)
        {
            Invoke((MethodInvoker)delegate ()
            {
                UpdateChangesInGrid();
                ToggleButtons(sender as Downloader);
                var downloader = sender as Downloader;
                if (downloader.File.Completed)
                {
                    var res = MessageBox.Show("File Downloaded Successfully Do you wish to open.", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        OpenExplorerFile(downloader.File.FullFilePath);

                    }
                }
            });
        }
        private void Down_OnError(object sender, Exception e)
        {
            MessageBox.Show(e.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            if (!isFinalCloseRequested)
            {
                
                e.Cancel = true;
                MinimizeToTray();

            }
            else
            {
                notifyIcon1.Visible = false;
                notifyIcon1.Icon = null;
                notifyIcon1.Dispose();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NetworkHelper.ConnectionOptimizer();

            LoadSettings();
          
            SettingsWindow.LoadSettings();

            StartServer();
        }

        void StartServer()
        {
            SonicHttpServer sonicHttpServer = new SonicHttpServer(9595);
            sonicHttpServer.OnConnection += SonicHttpServer_OnConnection;
            sonicHttpServer.Start(2);
        }

        private void SonicHttpServer_OnConnection(object sender, ConnectionEventArgs e)
        {
            string Url = e.Request.URL; 
            e.Response.StatusCode = StatusCode.OK;
            e.Response.End("Delivered=1");

            if(Url[0]=='/')
            {
                Url= Url.Remove(0, 1);
            }
            if (Url.IndexOf("downFileUrl=")>=0)
            {
                Url = Url.Replace("downFileUrl=","");
            }
            AddURL(Url);
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
                else
                {
                    FilePropertiesWindow fpw = new FilePropertiesWindow();
                    fpw.Downloader = DownloaderList[ind];
                    fpw.ShowDialog();
                }
            }
        }


        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);

                Downloader selDown = DownloaderList[ind];
                ToggleButtons(selDown);
            }
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


        private void ResumeToolStrip_Click(object sender, EventArgs e)
        {
            Resume();
        }


        private void StopToolStrip_Click(object sender, EventArgs e)
        {
            Pause();
        }


        private void DownloadNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resume();
        }

        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pause();
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

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MinimizeToTray();
        }
        private void MinimizeToTray()
        {
            Hide();
            notifyIcon1.Visible = true;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
                return;
            int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0)
            {
                OpenExplorerFile(DownloaderList[ind].File.FullFilePath);
            }
        }


        private void OpenFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
                return;
            int ind = dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0]);
            if (ind >= 0)
            {
                OpenExplorerFolder(DownloaderList[ind].File.FilePath);
            }
        }

        private void RedownloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReDownload();
        }

        private void ResumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resume();
        }

        private void StopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void RemoveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Remove();
        }
  
        private void MoveRenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            ShowSettingsWindow();
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void SonicDownloaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void CloseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool isFinalCloseRequested = false;
        private void CloseToolStripMenuItem1_MouseDown(object sender, MouseEventArgs e)
        {
            isFinalCloseRequested = true;
        }

        private async void CloseToolStripMenuItem1_MouseUp(object sender, MouseEventArgs e)
        {
            await Task.Delay(10000);
            isFinalCloseRequested = false;
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsWindow();
        }

        private void BugReporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = "mailto:sonic.dev.contact@gmail.com?subject=Sonic Downloader (bug) &body=Kindly write the 'URL' where bug was found and 'Description'\n\n ";
            Process.Start(command);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow aw = new AboutWindow();
            aw.ShowDialog();
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            YoutubeDownloaderWindow ydw = new YoutubeDownloaderWindow();
            DialogResult r=ydw.ShowDialog();
            if(r==DialogResult.OK)
            {
                ReDownload(ydw.URL,ydw.FileName);
            }
        }
    }
}
