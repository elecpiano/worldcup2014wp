using System.Windows.Controls;
using WorldCup2014WP.Models;
using System.Windows;
using WorldCup2014WP.Utility;
using System;

namespace WorldCup2014WP.Controls
{
    public partial class LivePageItemAlbum : UserControl
    {
        public Page HostingPage { get; set; }

        public LivePageItemAlbum()
        {
            InitializeComponent();
        }

        private void Control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HostingPage != null)
            {
                LiveLineItem item = sender.GetDataContext<LiveLineItem>();
                if (item != null)
                {
                    string naviString = string.Format("/Pages/AlbumPage.xaml?{0}={1}", NaviParam.ALBUM_ID, item.ID);
                    HostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                }
            }
        }
    }
}
