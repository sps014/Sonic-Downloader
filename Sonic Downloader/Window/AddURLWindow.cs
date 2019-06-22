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
    public partial class AddURLWindow : Form
    {
        public string URL { get; private set; }
        public AddURLWindow()
        {
            InitializeComponent();
        }
        private void CheckClipBoardForURL()
        {
            string url=Clipboard.GetText();
            if(UrlVerification.Verify(url))
            {
                textBox1.Text = url;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (UrlVerification.Verify(textBox1.Text))
                {
                    DialogResult = DialogResult.OK;
                    URL = textBox1.Text;
                    Close();
                }
                else
                {
                    MessageBox.Show($"URL: {textBox1.Text} is invalid .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"URL: {textBox1.Text} is Null or Whitespace .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddURLWindow_Activated(object sender, EventArgs e)
        {
            CheckClipBoardForURL();
        }
    }
}
