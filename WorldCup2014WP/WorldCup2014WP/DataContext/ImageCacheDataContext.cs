using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;

namespace WorldCup2014WP
{
    public class ImageCacheDataContext
    {
        #region Singleton

        private static ImageCacheDataContext instance = new ImageCacheDataContext();

        static ImageCacheDataContext()
        {
            //for thread-safe
        }

        private ImageCacheDataContext()
        {
            LoadImageCache();
        }

        public static ImageCacheDataContext Current
        {
            get { return instance; }
        }

        #endregion

        #region Property

        private string SPLITTER = "^";

        ImageHelper imageHelper = new ImageHelper();

        private Dictionary<string, string> _imageCache = new Dictionary<string, string>();
        public Dictionary<string, string> ImageCache
        {
            get
            {
                return _imageCache;
            }
        }

        #endregion

        IsolatedStorageSettings _Settings = IsolatedStorageSettings.ApplicationSettings;

        public void LoadImageCache()
        {
            if (_Settings.Contains(Constants.KEY_IMAGE_CACHE))
            {
                _imageCache = _Settings[Constants.KEY_IMAGE_CACHE] as Dictionary<string, string>;
            }
        }

        public async Task<BitmapImage> GetImage(string url, bool cache)
        {
            BitmapImage bi = null;
            bool noCacheFound = false;
            if (ImageCache.ContainsKey(url))
            {
                string file = ImageCache[url];
                bi = await imageHelper.ReadImage(Constants.KEY_IMAGE_CACHE, file);
                if (bi == null)
                {
                    //something is wrong, maybe the file is missing due to force deleting via external tool
                    noCacheFound = true;
                }
            }
            else
            {
                noCacheFound = true;
            }

            if (noCacheFound && cache)
            {
                string file = ComposeFileNameFromURL(url);
                bi = await imageHelper.Download(url, Constants.KEY_IMAGE_CACHE, file);
                AddImage(url);
            }

            return bi;
        }

        private string ComposeFileNameFromURL(string url)
        {
            return url.Replace("/", SPLITTER).Replace(":",SPLITTER);
        }

        public void AddImage(string url)
        {
            string fileName = ComposeFileNameFromURL(url);
            if (!ImageCache.ContainsKey(url))
            {
                ImageCache.Add(url, fileName);
            }
        }

        public void RemoveImage(string url)
        {
            if (ImageCache.ContainsKey(url))
            {
                ImageCache.Remove(url);
            }
        }

        public void SaveImageCache()
        {
            if (_Settings.Contains(Constants.KEY_IMAGE_CACHE))
            {
                _Settings[Constants.KEY_IMAGE_CACHE] = ImageCache;
            }
            else
            {
                _Settings.Add(Constants.KEY_IMAGE_CACHE, ImageCache);
            }
            _Settings.Save();
        }

        public void ClearImageCache()
        {
            if (_Settings.Contains(Constants.KEY_IMAGE_CACHE))
            {
                _imageCache = new Dictionary<string, string>();
                _Settings[Constants.KEY_IMAGE_CACHE] = _imageCache;
                _Settings.Save();
            }
        }
    }
}
