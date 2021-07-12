using LibVLCSharp.Shared;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AnimatedWallpaper
{

    public static class MediaHandler
    {
        public static LibVLC libVLC;
        private static string DataPath => Application.StartupPath + "Data.cfg";
        private static string VideosDirectory => Application.StartupPath + "Videos/";

        public static readonly List<MyMedia> activeMedia = new();
        public static readonly List<MyMedia> inactiveMedia = new();

        public static int CurrentIndex { get; set; }

        private static Logger logger = Logger.Instance;

        public static void Initialize()
        {
            Core.Initialize();
            //libVLC = new("--no-audio", "--input-repeat=65545");
            libVLC = new("--no-audio");

            Load();
        }

        public static bool Save()
        {
            List<string> data = new();

            foreach (MyMedia media in activeMedia)
            {
                data.Add($"{media.Name}:active");
            }

            foreach (MyMedia media in inactiveMedia)
            {
                data.Add($"{media.Name}:inactive");
            }

            try
            {
                if (File.Exists(DataPath))
                    File.Delete(DataPath);

                File.WriteAllLines(DataPath, data);
            }
            catch (IOException e)
            {
                logger.Log(LogType.Error, e.ToString());
                return false;
            }

            logger.Log(LogType.Normal, "Saved!");
            return true;
        }

        public static bool Load()
        {
            if (!File.Exists(DataPath))
            {
                logger.Log(LogType.Error, $"Failed to load ({DataPath} does not exist)");
                return false;
            }

            string[] data = File.ReadAllLines(DataPath);

            foreach (string s in data)
            {
                var split = s.Split(':');

                var name = split[0];
                var isActive = split[1] == "active";

                if (!File.Exists(VideosDirectory + name + ".mp4"))
                {
                    logger.Log(LogType.Warning, $"Could not find {name}, Skipping to the next media");
                    continue;
                }

                StreamReader reader = new(VideosDirectory + name + ".mp4");
                MyMedia media = new(libVLC, new StreamMediaInput(reader.BaseStream))
                {
                    Name = name,
                    IsActive = isActive
                };

                if (media.IsActive)
                    activeMedia.Add(media);
                else
                    inactiveMedia.Add(media);
            }

            logger.Log(LogType.Normal, $"Loaded!");
            return true;
        }

        public static MyMedia Get(string name)
        {
            var media = activeMedia.FirstOrDefault(x => x.Name == name);
            
            if(media == null)
                media = inactiveMedia.FirstOrDefault(x => x.Name == name);

            return media;
        }

        public static bool Exists(string name)
        {
            return activeMedia.Any(x => x.Name == name) && inactiveMedia.Any(x => x.Name == name);
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
            MyMedia media = new(libVLC, new StreamMediaInput(reader.BaseStream))
            {
                Name = name,
                IsActive = false
            };

            inactiveMedia.Add(media);

            Save();

            logger.Log(LogType.Normal, $"Added media ({name}) successfully!");
            return true;
        }

        public static bool Remove(string name)
        {
            var media = Get(name);

            if (media == null)
            {
                logger.Log(LogType.Error, $"Failed to remove {name}, Because it was not found.");
                return false;
            }

            if (media.IsActive)
                activeMedia.Remove(media);
            else
                inactiveMedia.Remove(media);

            media.Dispose();

            Save();

            logger.Log(LogType.Normal, $"{name} was removed successfully!");
            return true;
        }

        public static bool MoveUp(string name)
        {
            var media = Get(name);

            if (media == null)
            {
                logger.Log(LogType.Error, $"Failed to move up {name}, Because it was not found.");
                return false;
            }

            if(media.IsActive)
            {
                var index = activeMedia.IndexOf(media);

                var x = activeMedia[index];
                var y = activeMedia[index - 1];

                activeMedia[index - 1] = x;
                activeMedia[index] = y;
            }
            else
            {
                var index = inactiveMedia.IndexOf(media);

                var x = inactiveMedia[index];
                var y = inactiveMedia[index - 1];

                inactiveMedia[index - 1] = x;
                inactiveMedia[index] = y;
            }

            return true;
        }

        public static bool MoveDown(string name)
        {
            var media = Get(name);

            if (media == null)
            {
                logger.Log(LogType.Error, $"Failed to move down {name}, Because it was not found.");
                return false;
            }

            if (media.IsActive)
            {
                var index = activeMedia.IndexOf(media);

                var x = activeMedia[index];
                var y = activeMedia[index + 1];

                activeMedia[index + 1] = x;
                activeMedia[index] = y;
            }
            else
            {
                var index = inactiveMedia.IndexOf(media);

                var x = inactiveMedia[index];
                var y = inactiveMedia[index + 1];

                inactiveMedia[index + 1] = x;
                inactiveMedia[index] = y;
            }

            return true;
        }

        public static bool MoveToStart(string name)
        {
            var media = Get(name);

            if (media == null)
            {
                logger.Log(LogType.Error, $"Failed to move {name} to the start, Because it was not found.");
                return false;
            }

            if (media.IsActive)
            {
                activeMedia.Remove(media);
                activeMedia.Insert(0, media);
            }
            else
            {
                inactiveMedia.Remove(media);
                inactiveMedia.Insert(0, media);
            }

            return true;
        }

        public static bool MoveToEnd(string name)
        {
            var media = Get(name);

            if (media == null)
            {
                logger.Log(LogType.Error, $"Failed to move {name} to the end, Because it was not found.");
                return false;
            }

            if (media.IsActive)
            {
                activeMedia.Remove(media);
                activeMedia.Insert(activeMedia.Count, media);
            }
            else
            {
                inactiveMedia.Remove(media);
                inactiveMedia.Insert(inactiveMedia.Count, media);
            }

            return true;
        }

        public static bool Activate(string name)
        {
            var media = Get(name);

            if (media.IsActive)
            {
                logger.Log(LogType.Error, $"Failed to activate {name} to the start, Because it's already active.");
                return false;
            }

            media.IsActive = true;

            activeMedia.Add(media);
            inactiveMedia.Remove(media);

            logger.Log(LogType.Normal, $"{name} was activated!");
            return true;
        }

        public static bool Deactivate(string name)
        {
            var media = Get(name);

            if (!media.IsActive)
            {
                logger.Log(LogType.Error, $"Failed to deactivate {name} to the start, Because it's already inactive.");
                return false;
            }

            media.IsActive = false;

            inactiveMedia.Add(media);
            activeMedia.Remove(media);

            logger.Log(LogType.Normal, $"{name} was deactivated!");
            return true;
        }

        public static MyMedia GetCurrentMedia()
        {
            if (activeMedia.Count == 0)
                return null;

            return activeMedia[CurrentIndex];
        }

        public static MyMedia GetNextMedia()
        {
            if (activeMedia.Count == 0)
                return null;

            CurrentIndex++;
            if (CurrentIndex >= activeMedia.Count)
                CurrentIndex = 0;

            return activeMedia[CurrentIndex];
        }
    }
}