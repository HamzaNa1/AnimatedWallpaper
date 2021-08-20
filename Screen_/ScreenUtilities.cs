using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AnimatedWallpaper.Screen_
{
    internal static class ScreenUtilities
    {
        private static readonly string[] IgnoreList = new string[] {
            "explorer",
            "AnimatedWallpaper",
            "Discord", // Discord has a bug where it still counts as full screen when you close it down to the icon tray
            "StartMenuExperienceHost"
        };

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct Rect
        {
            public readonly int left;
            public readonly int top;
            public readonly int right;
            public readonly int bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref Rect rect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static bool IsForegroundFullScreen()
        {
            return IsForegroundFullScreen(null);
        }

        private static bool IsForegroundFullScreen(Screen screen)
        {
            screen ??= Screen.PrimaryScreen;

            Rect rect = new();
            var hWnd = GetForegroundWindow();

            GetWindowRect(new HandleRef(null, hWnd), ref rect);

            _ = GetWindowThreadProcessId(hWnd, out var procId);
            var proc = System.Diagnostics.Process.GetProcessById((int)procId);

            System.Diagnostics.Debug.WriteLine(proc.ProcessName);

            for (var i = 0; i < IgnoreList.Length; i++)
            {
                if (proc.ProcessName == IgnoreList[i])
                {
                    return false;
                }
            }

            return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top).Contains(screen.WorkingArea);
        }
    }
}
