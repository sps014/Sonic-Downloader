using Sonic.Downloader;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sonic_Downloader.Window
{
    public partial class AddURLWindow : Form
    {

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

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
        public AddURLWindow()
        {
            InitializeComponent();
            TopMost = true;
            Focus();
            BringToFront();
            Activate();
            TopMost = true;
            //SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            SetForegroundWindow(Handle.ToInt32());
        }
        private void CheckClipBoardForURL()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                string url = Clipboard.GetText();
                if (UrlVerification.Verify(url))
                {
                    textBox1.Text = url;
                }
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
