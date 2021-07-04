using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedWallpaper
{
    public class MyMedia : Media
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
