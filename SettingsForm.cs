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
        private MainForm main;

        public SettingsForm(MainForm main)
        {
            InitializeComponent();

            this.main = main;
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
            File.Copy(urlText.Text, "video.mp4", true);
            main.LoadVideo();
            Dispose();
        }
    }
}
