using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WorldCup2014WP.Utility
{
    public static class BitmapImageExtension
    {
        public static Stream ToStream(this BitmapImage bitmap)
        {
            var writeableBitmap = new WriteableBitmap(bitmap);
            var stream = new MemoryStream();
            writeableBitmap.SaveJpeg(stream, bitmap.PixelWidth, bitmap.PixelHeight, 0, 100);
            stream.Position = 0;
            return stream;
        } 
    }
}
