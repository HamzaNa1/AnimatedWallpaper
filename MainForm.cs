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
            WallpaperHandler.Initialize();
        }

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

            video.MediaPlayer = WallpaperHandler.MediaPlayer;

            WallpaperHandler.Restart();

            OpenSettings();
        }

        private void Tick(object sender, EventArgs e)
        {
            WallpaperHandler.CheckFullscreen();
        }

        private void MenuSettings_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeAll();
        }


        private void OpenSettings()
        {
            if (settings is not null && !settings.IsDisposed)
                return;

            settings = new();
            settings.Show();
        }

        private void DisposeAll()
        {
            MediaHandler.Dispose();
            WallpaperHandler.Dispose();
            WallpaperHandler.SetWallpaperToDefault();
            Logger.Instance.Dispose();
            Dispose();
        }
    }
}
