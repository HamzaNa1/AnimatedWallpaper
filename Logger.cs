using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AnimatedWallpaper
{
    public class Logger : IDisposable
    {
        public readonly string Path = Application.StartupPath + "log.txt";

        public static Logger Instance
        {
            get
            {
                return instance ??= new Logger();
            }
        }

        private static Logger instance;

        private readonly StreamWriter writer;

        public Logger()
        {
            var stream = File.Create(Path);
            stream.Close();
            stream.Dispose();

            writer = new StreamWriter(Path)
            {
                AutoFlush = true
            };
        }

        public void Log(LogType type, string message)
        {
            string prefix = type switch
            {
                LogType.Normal => "Message",
                LogType.Warning => "Warning",
                LogType.Error => "Error",
                _ => "",
            };

            var now = DateTime.Now.ToString();
            string data = $"[{now}] {prefix}: {message}";

            Debug.WriteLine(data);

            writer.WriteLineAsync(data);
        }

        public void Dispose()
        {
            writer.Close();
            writer.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public enum LogType
    {
        Normal,
        Error,
        Warning
    }
}
