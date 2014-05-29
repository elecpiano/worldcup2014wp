using System;
using System.IO;
using System.Windows.Resources;

namespace WorldCup2014WP.Utility
{
    public class IOHelper
    {
        //工具函数，读取res资源成byte数组
        public static byte[] ReadRes(string path)
        {
            StreamResourceInfo info = App.GetResourceStream(new Uri(path, UriKind.Relative));
            if (info == null) return null;
            Stream stream = info.Stream;
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            return data;
        }

        public static Stream ReadResAsStream(string path)
        {
            StreamResourceInfo info = App.GetResourceStream(new Uri(path, UriKind.Relative));
            if (info == null) return null;
            Stream stream = info.Stream;
            return stream;
        }
    }
}
