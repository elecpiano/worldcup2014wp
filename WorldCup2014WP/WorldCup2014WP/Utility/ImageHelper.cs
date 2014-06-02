using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Windows.Storage;
using Microsoft.Phone.Net.NetworkInformation;

namespace WorldCup2014WP.Utility
{
    public class ImageHelper
    {
        App App { get { return App.Current as App; } }

        static string folderName = string.Empty;
        static string fileName = string.Empty;
        Action onDownloaded = null;

        public async Task<BitmapImage> Download(string uri, string folder, string file)
        {
            BitmapImage bi = null;
            try
            {
                //download
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(uri));
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                //save
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                var dataFolder = await local.CreateFolderAsync(folder, CreationCollisionOption.OpenIfExists);

                using (Stream stream = response.GetResponseStream())
                {
                    bi = new BitmapImage();

                    byte[] fileBytes = new byte[response.ContentLength];
                    await stream.ReadAsync(fileBytes, 0, (int)response.ContentLength);

                    var newFile = await dataFolder.CreateFileAsync(file, CreationCollisionOption.ReplaceExisting);

                    // Write the data
                    using (var s = await newFile.OpenStreamForWriteAsync())
                    {
                        await s.WriteAsync(fileBytes, 0, fileBytes.Length);
                    }
                }

                //read
                dataFolder = await local.GetFolderAsync(folder);
                var fileStream = await dataFolder.OpenStreamForReadAsync(file);
                bi = new BitmapImage();
                bi.SetSource(fileStream);
                return bi;
            }
            catch (WebException e)
            {
            }
            catch (Exception e)
            {
            }
            return bi;
        }

        public void Download(string uri, string folder, string file, Action callback)
        {
            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {
                return;
            }

            try
            {
                folderName = folder;
                fileName = file;
                onDownloaded = callback;

                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(uri));
                request.Method = "GET";
                request.BeginGetResponse(Download_Callback, request);
            }
            catch (WebException e)
            {
            }
            catch (Exception e)
            {
            }
        }

        private async void Download_Callback(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                WebResponse response = request.EndGetResponse(result);

                using (Stream stream = response.GetResponseStream())
                {
                    byte[] fileBytes = new byte[response.ContentLength];
                    await stream.ReadAsync(fileBytes, 0, (int)response.ContentLength);

                    // Get the local folder.
                    StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                    // Create a new folder
                    var dataFolder = await local.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

                    // Create a new file
                    var file = await dataFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                    // Write the data
                    using (var s = await file.OpenStreamForWriteAsync())
                    {
                        await s.WriteAsync(fileBytes, 0, fileBytes.Length);
                    }

                    onDownloaded();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        public async Task<BitmapImage> ReadImage(string folder, string file)
        {
            try
            {
                // Get the local folder.
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                if (local != null)
                {
                    // Get the DataFolder folder.
                    var dataFolder = await local.GetFolderAsync(folder);

                    // Get the file.
                    var stream = await dataFolder.OpenStreamForReadAsync(file);

                    // Read the data.
                    var bi = new BitmapImage();
                    bi.SetSource(stream);
                    return bi;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public static async Task<byte[]> GetImageData(string uri)
        {
            byte[] fileBytes = null;
            try
            {
                //download
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(uri));
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                using (Stream stream = response.GetResponseStream())
                {
                    fileBytes = new byte[response.ContentLength];
                    await stream.ReadAsync(fileBytes, 0, (int)response.ContentLength);
                }

            }
            catch (Exception e)
            {
            }
            return fileBytes;
        }

        public static async Task<Stream> GetImageStream(string uri)
        {
            byte[] fileBytes = null;
            MemoryStream ms = null;
            try
            {
                //download
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(uri));
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                using (Stream stream = response.GetResponseStream())
                {
                    fileBytes = new byte[response.ContentLength];
                    await stream.ReadAsync(fileBytes, 0, (int)response.ContentLength);
                    ms = new MemoryStream(fileBytes);
                }

            }
            catch (Exception e)
            {
            }
            return ms;
        }

    }
}
