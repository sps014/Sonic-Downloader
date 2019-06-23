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
        public GeneralSettings generalSettings=new GeneralSettings();
        public SettingsWindow()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 3;
        }
        void UpdateControls()
        {
            switch(generalSettings.MaxConnection)
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
            numericUpDown1.Value = generalSettings.Timeout;
        }
        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            JSONHelper.DownloadableConfigPath = "settings.json";
            if(File.Exists(JSONHelper.DownloadableConfigPath))
            {
                generalSettings=JSONHelper.LoadFiles() as GeneralSettings;
                UpdateControls();
            }
        }
        void SaveSettings()
        {
            generalSettings.MaxConnection = int.Parse(comboBox1.SelectedItem.ToString());
            generalSettings.Timeout = (int)numericUpDown1.Value;

            JSONHelper.DownloadableConfigPath = "settings.json";
            JSONHelper.SaveFiles(generalSettings);
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
