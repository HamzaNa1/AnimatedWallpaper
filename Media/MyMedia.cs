using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;

namespace AnimatedWallpaper.Media
{
    public class MyMedia : LibVLCSharp.Shared.Media
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public MyMedia(MediaList mediaList) : base(mediaList)
        {
        }

        public MyMedia(LibVLC libVLC, Uri uri, params string[] options) : base(libVLC, uri, options)
        {
        }

        public MyMedia(LibVLC libVLC, int fd, params string[] options) : base(libVLC, fd, options)
        {
        }

        public MyMedia(LibVLC libVLC, MediaInput input, params string[] options) : base(libVLC, input, options)
        {
        }

        public MyMedia(LibVLC libVLC, string mrl, FromType type = FromType.FromPath, params string[] options) : base(libVLC, mrl, type, options)
        {
        }

        #nullable enable

        public static MyMedia? GetFirstByName(List<MyMedia> list, string name)
        {
            foreach (MyMedia media in list)
            {
                if (media.Name == name)
                    return media;
            }

            return null;
        }

        public static bool AnyByName(List<MyMedia> list, string name)
        {
            foreach(MyMedia media in list)
            {
                if (media.Name == name)
                    return true;
            }

            return false;
        }

        #nullable disable
    }
}