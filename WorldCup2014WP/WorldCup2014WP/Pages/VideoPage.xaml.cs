using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;
using System.Collections.Generic;
using Microsoft.Phone.Tasks;
using WorldCup2014WP.Controls;
using System.Windows.Controls;

namespace WorldCup2014WP.Pages
{
    public partial class VideoPage : PhoneApplicationPage
    {
        //private string vodID = string.Empty;
        private static List<VideoItem> videos = new List<VideoItem>();

        #region Lifecycle

        public VideoPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            videoListBox.ItemsSource = videos;
        }

        #endregion

        #region Data

        static DataLoader<VOD> dataLoader = new DataLoader<VOD>();

        public static void PlayVideo(Page hostingPage, string vodID, Snow snow1=null)
        {
            if (dataLoader.Busy)
            {
                return;
            }

            if (snow1!=null)
            {
                snow1.IsBusy = true;
            }

            dataLoader.Load("getvod", "&id=" + vodID, false, Constants.VOD_MODULE, string.Format(Constants.VOD_FILE_NAME_FORMAT, vodID),
                data => 
                {
                    if (data==null)
                    {
                        return;
                    }
                    if (data.Videos==null)
                    {
                        return;
                    }
                    if (snow1 != null)
                    {
                        snow1.IsBusy = false;
                    }                    

                    if (data.Videos != null && data.Videos.Length > 0)
                    {
                        videos.Clear();
                        foreach (var item in data.Videos)
                        {
                            videos.Add(item);
                        }

                        if (hostingPage != null)
                        {
                            string naviString = string.Format("/Pages/VideoPage.xaml");
                            hostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                        }
                    }
                    else
                    {
                        PlaySingleVideo(data.URL);
                    }

                });
        }

        #endregion

        private void Video_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            VideoItem item = sender.GetDataContext<VideoItem>();

            MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
            mediaPlayerLauncher.Media = new Uri(item.URL, UriKind.Absolute);
            mediaPlayerLauncher.Controls = MediaPlaybackControls.Pause | MediaPlaybackControls.Stop;
            mediaPlayerLauncher.Orientation = MediaPlayerOrientation.Landscape;
            mediaPlayerLauncher.Show();
        }

        private static void PlaySingleVideo(string url)
        {
            MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
            mediaPlayerLauncher.Media = new Uri(url, UriKind.Absolute);
            mediaPlayerLauncher.Controls = MediaPlaybackControls.Pause | MediaPlaybackControls.Stop;
            mediaPlayerLauncher.Orientation = MediaPlayerOrientation.Landscape;
            mediaPlayerLauncher.Show();
        }

    }
}