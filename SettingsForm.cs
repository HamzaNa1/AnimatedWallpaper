using Microsoft.Win32;
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

namespace AnimatedWallpaper
{
    public partial class SettingsForm : Form
    {
        private readonly RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        private readonly MainForm main;

        private readonly bool initStartupState;

        public SettingsForm(MainForm main)
        {
            InitializeComponent();

            this.main = main;

            if (rkApp.GetValue("AnimatedWallpaper") is null)
            {
                startup_chk.Checked = false;
                initStartupState = false;
            }
            else
            {
                startup_chk.Checked = true;
                initStartupState = true;
            }
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            if (videoFileDialog.ShowDialog() == DialogResult.OK)
            {
                urlText.Text = videoFileDialog.FileName;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(urlText.Text))
            {
                main.DisposeVideo();
                File.Copy(urlText.Text, "video.mp4", true);
                main.LoadVideo();
            }

            if (startup_chk.Checked != initStartupState)
            {
                if (startup_chk.Checked)
                {
                    rkApp.SetValue("AnimatedWallpaper", Application.ExecutablePath);
                }
                else
                {
                    rkApp.DeleteValue("AnimatedWallpaper", false);
                }
            }

            Dispose();
        }
    }
}
