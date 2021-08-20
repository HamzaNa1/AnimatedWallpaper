using AnimatedWallpaper.Loggers;
using LibVLCSharp.Shared;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AnimatedWallpaper.Media
{
    public static class MediaHandler
    {
        private static string DataPath => Application.StartupPath + "Data.cfg";
        private static string VideosDirectory => Application.StartupPath + "Videos/";

        public static readonly List<MyMedia> ActiveMedia = new();
        public static readonly List<MyMedia> InactiveMedia = new();

        private static int CurrentIndex { get; set; }
        public static LibVLC LibVlc { get; private set; }

        private static readonly Logger Logger = Logger.Instance;

        public static void Initialize()
        {
            Core.Initialize();
            //libVLC = new("--no-audio", "--input-repeat=65545");
            LibVlc = new LibVLC("--no-audio");

            Load();
        }

        public static bool Save()
        {
            var data = ActiveMedia.Select(media => $"{media.Name}:active").ToList();
            data.AddRange(InactiveMedia.Select(media => $"{media.Name}:inactive"));

            try
            {
                if (File.Exists(DataPath))
                    File.Delete(DataPath);

                File.WriteAllLines(DataPath, data);
            }
            catch (IOException e)
            {
                Logger.Log(LogType.Error, e.ToString());
                return false;
            }

            Logger.Log(LogType.Normal, "Saved!");
            return true;
        }

        private static bool Load()
        {
            if (!File.Exists(DataPath))
            {
                Logger.Log(LogType.Error, $"Failed to load ({DataPath} does not exist)");
                return false;
            }

            var data = File.ReadAllLines(DataPath);

            foreach (var s in data)
            {
                var split = s.Split(':');

                var name = split[0];
                var isActive = split[1] == "active";

                if (!File.Exists(VideosDirectory + name + ".mp4"))
                {
                    Logger.Log(LogType.Warning, $"Could not find {name}, Skipping to the next media");
                    continue;
                }

                StreamReader reader = new(VideosDirectory + name + ".mp4");
                MyMedia media = new(LibVlc, new StreamMediaInput(reader.BaseStream))
                {
                    Name = name,
                    IsActive = isActive
                };

                if (media.IsActive)
                    ActiveMedia.Add(media);
                else
                    InactiveMedia.Add(media);
            }

            Logger.Log(LogType.Normal, "Loaded!");
            return true;
        }

        public static MyMedia Get(string name)
        {
            return MyMedia.GetFirstByName(ActiveMedia, name) ?? MyMedia.GetFirstByName(InactiveMedia, name);
        }

        private static bool Exists(string name)
        {
            return MyMedia.AnyByName(ActiveMedia, name) || MyMedia.AnyByName(InactiveMedia, name);
        }

        public static bool Add(string url, string name)
        {
            if (!Directory.Exists(VideosDirectory))
                Directory.CreateDirectory(VideosDirectory);

            if (!File.Exists(url))
            {
                Logger.Instance.Log(LogType.Error, $"Failed to add {name}, Because {url} does not exist.");
                return false;
            }

            if (Exists(name))
            {
                Logger.Instance.Log(LogType.Error, $"Failed to add {name}, Because it already exist.");
                return false;
            }

            File.Copy(url, VideosDirectory + name + ".mp4", true);

            StreamReader reader = new(VideosDirectory + name + ".mp4");
            MyMedia media = new(LibVlc, new StreamMediaInput(reader.BaseStream))
            {
                Name = name,
                IsActive = false
            };

            InactiveMedia.Add(media);

            Save();

            Logger.Log(LogType.Normal, $"Added media ({name}) successfully!");
            return true;
        }

        public static bool Remove(string name)
        {
            var media = Get(name);

            if (media == null)
            {
                Logger.Log(LogType.Error, $"Failed to remove {name}, Because it was not found.");
                return false;
            }

            if (media.IsActive)
                ActiveMedia.Remove(media);
            else
                InactiveMedia.Remove(media);

            media.Dispose();

            Save();

            Logger.Log(LogType.Normal, $"{name} was removed successfully!");
            return true;
        }

        public static bool MoveUp(string name)
        {
            var media = Get(name);

            if (media == null)
            {
                Logger.Log(LogType.Error, $"Failed to move up {name}, Because it was not found.");
                return false;
            }

            if(media.IsActive)
            {
                var index = ActiveMedia.IndexOf(media);

                var x = ActiveMedia[index];
                var y = ActiveMedia[index - 1];

                ActiveMedia[index - 1] = x;
                ActiveMedia[index] = y;
            }
            else
            {
                var index = InactiveMedia.IndexOf(media);

                var x = InactiveMedia[index];
                var y = InactiveMedia[index - 1];

                InactiveMedia[index - 1] = x;
                InactiveMedia[index] = y;
            }

            return true;
        }

        public static bool MoveDown(string name)
        {
            var media = Get(name);

            if (media == null)
            {
                Logger.Log(LogType.Error, $"Failed to move down {name}, Because it was not found.");
                return false;
            }

            if (media.IsActive)
            {
                var index = ActiveMedia.IndexOf(media);

                var x = ActiveMedia[index];
                var y = ActiveMedia[index + 1];

                ActiveMedia[index + 1] = x;
                ActiveMedia[index] = y;
            }
            else
            {
                var index = InactiveMedia.IndexOf(media);

                var x = InactiveMedia[index];
                var y = InactiveMedia[index + 1];

                InactiveMedia[index + 1] = x;
                InactiveMedia[index] = y;
            }

            return true;
        }

        public static bool MoveToStart(string name)
        {
            var media = Get(name);

            if (media == null)
            {
                Logger.Log(LogType.Error, $"Failed to move {name} to the start, Because it was not found.");
                return false;
            }

            if (media.IsActive)
            {
                ActiveMedia.Remove(media);
                ActiveMedia.Insert(0, media);
            }
            else
            {
                InactiveMedia.Remove(media);
                InactiveMedia.Insert(0, media);
            }

            return true;
        }

        public static bool MoveToEnd(string name)
        {
            var media = Get(name);

            if (media == null)
            {
                Logger.Log(LogType.Error, $"Failed to move {name} to the end, Because it was not found.");
                return false;
            }

            if (media.IsActive)
            {
                ActiveMedia.Remove(media);
                ActiveMedia.Insert(ActiveMedia.Count, media);
            }
            else
            {
                InactiveMedia.Remove(media);
                InactiveMedia.Insert(InactiveMedia.Count, media);
            }

            return true;
        }

        public static bool Activate(string name)
        {
            var media = Get(name);

            if (media.IsActive)
            {
                Logger.Log(LogType.Error, $"Failed to activate {name} to the start, Because it's already active.");
                return false;
            }

            media.IsActive = true;

            ActiveMedia.Add(media);
            InactiveMedia.Remove(media);

            Logger.Log(LogType.Normal, $"{name} was activated!");
            return true;
        }

        public static bool Deactivate(string name)
        {
            var media = Get(name);

            if (!media.IsActive)
            {
                Logger.Log(LogType.Error, $"Failed to deactivate {name} to the start, Because it's already inactive.");
                return false;
            }

            media.IsActive = false;

            InactiveMedia.Add(media);
            ActiveMedia.Remove(media);

            Logger.Log(LogType.Normal, $"{name} was deactivated!");
            return true;
        }

        public static MyMedia GetCurrentMedia()
        {
            if (ActiveMedia.Count == 0)
                return null;

            return ActiveMedia[CurrentIndex];
        }

        public static MyMedia GetNextMedia()
        {
            if (ActiveMedia.Count == 0)
                return null;

            CurrentIndex++;
            if (CurrentIndex >= ActiveMedia.Count)
                CurrentIndex = 0;

            return ActiveMedia[CurrentIndex];
        }

        public static void Dispose()
        {
            LibVlc.Dispose();
        }
    }
}