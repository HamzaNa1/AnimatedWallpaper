using System;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using System.Threading;

namespace AnimatedWallpaper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            MediaHandler.Initialize();
        }

        private MediaPlayer mediaPlayer;

        private SettingsForm settings;

        private void MainForm_Load(object sender, EventArgs e)
        {
            Location = Screen.PrimaryScreen.Bounds.Location;
            Size = Screen.PrimaryScreen.Bounds.Size;
            video.Location = Location;
            video.Size = Size;

            var menu = new ContextMenuStrip();
            menu.Items.Add("Settings", null, MenuSettings_Click);
            menu.Items.Add("Exit", null, (_, _) =>
            {
                DisposeAll();
                Application.Exit();
            });

            notifyIcon.ContextMenuStrip = menu;

            System.Windows.Forms.Timer timer = new()
            {
                Interval = 300,
            };

            timer.Tick += Tick;
            timer.Start();

            mediaPlayer = new MediaPlayer(MediaHandler.libVLC);

            mediaPlayer.EndReached += (_, _) => ThreadPool.QueueUserWorkItem((_) =>
            {
                PlayNext();
            });

            video.MediaPlayer = mediaPlayer;

            Restart();

            OpenSettings();
        }

        private void Tick(object sender, EventArgs e)
        {
            CheckFullscreen();
        }

        void MenuSettings_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void PlayNext()
        {
            mediaPlayer.Play(MediaHandler.GetNextMedia());
        }

        public void Restart()
        {
            mediaPlayer.Play(MediaHandler.GetCurrentMedia());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeAll();
        }

        private void CheckFullscreen()
        {
            try
            {
                if (mediaPlayer is null)
                    return;

                bool fullscreen = ScreenUtilities.IsForegroundFullScreen();

                if (fullscreen && mediaPlayer.IsPlaying)
                {
                    mediaPlayer.Pause();
                    System.Diagnostics.Debug.WriteLine("Paused!");
                }
                else if (!fullscreen && !mediaPlayer.IsPlaying)
                {
                    mediaPlayer.Play();
                    System.Diagnostics.Debug.WriteLine("Resumed!");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        private void OpenSettings()
        {
            if (settings is not null && !settings.IsDisposed)
                return;

            settings = new(this);
            settings.Show();
        }

        private void DisposeAll()
        {
            Dispose();
            Logger.Instance.Dispose();
            WallpaperHandler.SetWallpaperToDefault();
        }
    }
}
