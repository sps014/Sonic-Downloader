using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic_Downloader.Window
{
    partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();
            
        }


        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
