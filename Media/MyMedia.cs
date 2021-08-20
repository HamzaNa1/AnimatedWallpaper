using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;

namespace AnimatedWallpaper.Media
{
    public class MyMedia : LibVLCSharp.Shared.Media
    {
        public string Name { get; init; }
        public bool IsActive { get; set; }

        public MyMedia(MediaList mediaList) : base(mediaList)
        {
        }

        public MyMedia(LibVLC libVlc, Uri uri, params string[] options) : base(libVlc, uri, options)
        {
        }

        public MyMedia(LibVLC libVlc, int fd, params string[] options) : base(libVlc, fd, options)
        {
        }

        public MyMedia(LibVLC libVlc, MediaInput input, params string[] options) : base(libVlc, input, options)
        {
        }

        public MyMedia(LibVLC libVlc, string mrl, FromType type = FromType.FromPath, params string[] options) : base(libVlc, mrl, type, options)
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