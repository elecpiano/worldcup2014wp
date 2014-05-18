using System;
using System.Windows.Controls;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;

namespace WorldCup2014WP.Controls
{
    public partial class LivePageItemNews : UserControl
    {
        public Page HostingPage { get; set; }

        public LivePageItemNews()
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
                    string naviString = string.Format("/Pages/NewsDetailPage.xaml?{0}={1}&{2}={3}", NaviParam.NEWS_ID, item.ID,NaviParam.NEWS_TITLE,item.Title);
                    HostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                }
            }
        }
    }
}
