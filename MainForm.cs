using System;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using System.IO;
using System.Windows.Automation;

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
            Location = Screen.PrimaryScreen.Bounds.Location;
            Size = Screen.PrimaryScreen.Bounds.Size;
            video.Location = Location;
            video.Size = Size;

            var menu = new ContextMenuStrip();
            menu.Items.Add("Settings", null, MenuSettings_Click);
            menu.Items.Add("Exit", null, (_, _) => Application.Exit());

            notifyIcon.ContextMenuStrip = menu;

            Timer timer = new()
            {
                Interval = 10000,
            };

            timer.Tick += Tick;
            timer.Start();

            Automation.AddAutomationFocusChangedEventHandler(OnFocusChangedHandler);

            Core.Initialize();

            libVLC = new("--no-audio", "--input-repeat=65545");
            mediaPlayer = new MediaPlayer(libVLC);
         
            video.MediaPlayer = mediaPlayer;

            if (!File.Exists(Application.StartupPath + "video.mp4"))
                return;

            LoadVideo();
        }

        private void Tick(object sender, EventArgs e)
        {
            CheckFullscreen();
        }

        private void OnFocusChangedHandler(object sender, AutomationFocusChangedEventArgs e)
        {
            CheckFullscreen();
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

        public void LoadVideo()
        {
            DisposeVideo();

            currentReader = new(Application.StartupPath + "video.mp4");
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
            Automation.RemoveAllEventHandlers();

            DisposeVideo();
        }

        public void DisposeVideo()
        {
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
