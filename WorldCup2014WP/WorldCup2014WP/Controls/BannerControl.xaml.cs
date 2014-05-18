using System;
using System.Windows.Controls;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;
using WorldCup2014WP.Pages;
using Microsoft.Phone.Tasks;

namespace WorldCup2014WP.Controls
{
    public partial class BannerControl : UserControl
    {
        public Page HostingPage { get; set; }
        App App { get { return App.Current as App; } }

        public BannerControl()
        {
            InitializeComponent();
        }

        private void dismissButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Banner banner = sender.GetDataContext<Banner>();
            App.DismissedBannerId = banner.ID;
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void banner_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HostingPage != null)
            {
                Banner banner = sender.GetDataContext<Banner>();
                if (banner != null)
                {
                    string naviString = string.Empty;
                    switch (banner.Type)
                    {
                        case "0":
                            VideoPage.PlayVideo(HostingPage, banner.ID, this.snow1);
                            break;
                        case "1":
                            naviString = string.Format("/Pages/NewsDetailPage.xaml?{0}={1}&{2}={3}", NaviParam.NEWS_ID, banner.ID,NaviParam.NEWS_TITLE, banner.Title);
                            HostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                            break;
                        case "2":
                            naviString = string.Format("/Pages/AlbumPage.xaml?{0}={1}", NaviParam.ALBUM_ID, banner.ID);
                            HostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                            break;
                        case "4":
                            if (!string.IsNullOrEmpty(banner.URL))
                            {
                                WebBrowserTask webbrowser = new WebBrowserTask();
                                webbrowser.Uri = new Uri(banner.URL, UriKind.Absolute);
                                webbrowser.Show();
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
        }

    }
}
