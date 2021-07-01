using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using System.IO;

namespace AnimatedWallpaper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private StreamReader currentReader = null;
        private MediaPlayer currentMediaPlayer = null;

        private void Main_Load(object sender, EventArgs e)
        {
            Location = Screen.PrimaryScreen.Bounds.Location;
            Size = Screen.PrimaryScreen.Bounds.Size;
            video.Location = Location;
            video.Size = Size;

            var menu = new ContextMenuStrip();
            menu.Items.Add("Settings", null, MenuSettings_Click);
            menu.Items.Add("Exit", null, (_, _) => Application.Exit());

            notifyIcon.ContextMenuStrip = menu;

            Core.Initialize();

            if (!File.Exists("video.mp4"))
                return;

            LoadVideo();
        }

        public void LoadVideo()
        {
            DisposeVideo();

            currentReader = new("video.mp4");
            LibVLC libVLC = new("--input-repeat=2");

            currentMediaPlayer = new MediaPlayer(new Media(libVLC, new StreamMediaInput(currentReader.BaseStream)))
            {
                Mute = true
            };

            video.MediaPlayer = currentMediaPlayer;

            video.MediaPlayer.Play();
        }

        void MenuSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new(this);
            settings.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeVideo();
        }

        public void DisposeVideo()
        {
            if (currentReader is not null)
            {
                currentReader.Dispose();
                currentReader = null;
            }

            if (currentMediaPlayer is not null)
            {
                currentMediaPlayer.Media.Dispose();
                currentMediaPlayer.Dispose();
                currentMediaPlayer = null;
            }
        }
    }
}
