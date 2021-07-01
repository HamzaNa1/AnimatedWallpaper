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
using System.Threading;

namespace AnimatedWallpaper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private StreamReader currentReader = null;
        private Media currentMedia = null;

        private MediaPlayer mediaPlayer;
        private LibVLC libVLC;

        private void Main_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Hi");
            Location = Screen.PrimaryScreen.Bounds.Location;
            Size = Screen.PrimaryScreen.Bounds.Size;
            video.Location = Location;
            video.Size = Size;

            var menu = new ContextMenuStrip();
            menu.Items.Add("Settings", null, MenuSettings_Click);
            menu.Items.Add("Exit", null, (_, _) => Application.Exit());

            notifyIcon.ContextMenuStrip = menu;

            Core.Initialize();

            libVLC = new();
            mediaPlayer = new MediaPlayer(libVLC)
            {
                Mute = true
            };

            mediaPlayer.EndReached += (sender, args) => ThreadPool.QueueUserWorkItem(_ => {
                mediaPlayer.Play(currentMedia);
            });


            video.MediaPlayer = mediaPlayer;

            if (!File.Exists("video.mp4"))
                return;

            LoadVideo();
        }

        public void LoadVideo()
        {
            DisposeVideo();

            currentReader = new("video.mp4");
            currentMedia = new Media(libVLC, new StreamMediaInput(currentReader.BaseStream));

            mediaPlayer.Play(currentMedia);
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
            System.Diagnostics.Debug.WriteLine("Disposing");
            if (currentReader is not null)
            {
                currentReader.Dispose();
                currentReader = null;
            }

            if (currentMedia is not null)
            {
                if(mediaPlayer.IsPlaying)
                {
                    mediaPlayer.Stop();
                    mediaPlayer.Media = null;
                }

                currentMedia.Dispose();
                currentMedia = null;
            }
        }
    }
}
