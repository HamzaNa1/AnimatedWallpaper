using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AnimatedWallpaper
{
    internal static class ScreenUtilities
    {
        public static readonly string[] IgnoreList = new string[] {
            "explorer",
            "discord", // Discord has a bug where it still counts as full screen when you close it down to the icon tray
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static bool IsForegroundFullScreen()
        {
            return IsForegroundFullScreen(null);
        }

        public static bool IsForegroundFullScreen(Screen screen)
        {
            if (screen == null)
            {
                screen = Screen.PrimaryScreen;
            }
            RECT rect = new();
            IntPtr hWnd = GetForegroundWindow();

            GetWindowRect(new HandleRef(null, hWnd), ref rect);

            _ = GetWindowThreadProcessId(hWnd, out uint procId);
            var proc = System.Diagnostics.Process.GetProcessById((int)procId);

            System.Diagnostics.Debug.WriteLine(proc.ProcessName);

            if (IgnoreList.Contains(proc.ProcessName))
                return false;

            return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top).Contains(screen.WorkingArea);
        }
    }
}
