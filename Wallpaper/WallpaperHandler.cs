using AnimatedWallpaper.Media;
using AnimatedWallpaper.Screen_;
using LibVLCSharp.Shared;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace AnimatedWallpaper.Wallpaper
{
    public static class WallpaperHandler
    {
        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static MediaPlayer MediaPlayer { get; private set; }

        public static void Initialize()
        {
            MediaPlayer = new MediaPlayer(MediaHandler.LibVLC);

            MediaPlayer.EndReached += (_, _) => ThreadPool.QueueUserWorkItem((_) => PlayNext());
        }

        public static void SetWallpaperToDefault()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Themes\TranscodedWallpaper";

            _ = SystemParametersInfo(SPI_SETDESKWALLPAPER,
            0,
            path,
            SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        public static void CheckFullscreen()
        {
            try
            {
                if (MediaPlayer is null)
                    return;

                bool fullscreen = ScreenUtilities.IsForegroundFullScreen();

                if (fullscreen && MediaPlayer.IsPlaying)
                {
                    MediaPlayer.Pause();
                    System.Diagnostics.Debug.WriteLine("Paused!");
                }
                else if (!fullscreen && !MediaPlayer.IsPlaying)
                {
                    MediaPlayer.Play();
                    System.Diagnostics.Debug.WriteLine("Resumed!");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        public static void PlayNext()
        {
            MediaPlayer.Play(MediaHandler.GetNextMedia());
        }

        public static void Restart()
        {
            MediaPlayer.Play(MediaHandler.GetCurrentMedia());
        }

        public static void Dispose()
        {
            MediaPlayer.Dispose();
        }
    }
}
