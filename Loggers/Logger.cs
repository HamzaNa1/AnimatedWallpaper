using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace AnimatedWallpaper.Loggers
{
    public class Logger : IDisposable
    {
        private readonly string _path = Application.StartupPath + "log.txt";

        public static Logger Instance
        {
            get
            {
                return _instance ??= new Logger();
            }
        }

        private static Logger _instance;

        private readonly StreamWriter _writer;

        private Logger()
        {
            var stream = File.Create(_path);
            stream.Close();
            stream.Dispose();

            _writer = new StreamWriter(_path)
            {
                AutoFlush = true
            };
        }

        public void Log(LogType type, string message)
        {
            var prefix = type switch
            {
                LogType.Normal => "Message",
                LogType.Warning => "Warning",
                LogType.Error => "Error",
                _ => "",
            };

            var now = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var data = $"[{now}] {prefix}: {message}";

            Debug.WriteLine(data);

            _writer.WriteLineAsync(data);
        }

        public void Dispose()
        {
            _writer.Close();
            _writer.Dispose();
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
