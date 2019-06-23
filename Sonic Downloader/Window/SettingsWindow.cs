using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic_Downloader.Window
{
    public partial class SettingsWindow : Form
    {
        public static GeneralSettings GeneralSettings=new GeneralSettings();
        public SettingsWindow()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 3;
        }
        void UpdateControls()
        {
            switch(GeneralSettings.MaxConnection)
            {
                case 1:
                    comboBox1.SelectedIndex = 0;
                    break;
                case 2:
                    comboBox1.SelectedIndex = 1;
                    break;
                case 4:
                    comboBox1.SelectedIndex = 2;
                    break;
                case 8:
                    comboBox1.SelectedIndex = 3;
                    break;
                case 16:
                    comboBox1.SelectedIndex = 4;
                    break;
                case 32:
                    comboBox1.SelectedItem = 5;
                    break;
                case 64:
                    comboBox1.SelectedIndex = 6;
                    break;
                case 128:
                    comboBox1.SelectedIndex = 7;
                    break;
            }
            numericUpDown1.Value = GeneralSettings.Timeout;
            textBox1.Text = GeneralSettings.StoragePath;

        }
        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            if(LoadSettings())
                UpdateControls();
            
        }
        void SaveSettings()
        {
            GeneralSettings.MaxConnection = int.Parse(comboBox1.SelectedItem.ToString());
            GeneralSettings.Timeout = (int)numericUpDown1.Value;

            JSONHelper.DownloadableConfigPath = "settings.json";
            JSONHelper.SaveFiles(GeneralSettings);
        }
        public static bool LoadSettings()
        {
            JSONHelper.DownloadableConfigPath = "settings.json";
            if (File.Exists(JSONHelper.DownloadableConfigPath))
            {
                GeneralSettings = JSONHelper.LoadFiles();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SettingsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text=GeneralSettings.StoragePath = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
