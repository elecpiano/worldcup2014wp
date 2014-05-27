using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;
using System.Collections.ObjectModel;

namespace WorldCup2014WP.Pages
{
    public partial class TeamPage : PhoneApplicationPage
    {
        #region Lifecycle

        public TeamPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadData();
        }

        #endregion

        #region data

        DataLoader<Subject> dataLoader = new DataLoader<Subject>();

        private void LoadData()
        {
            if (dataLoader.Loaded || dataLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            //load
            dataLoader.Load("getteamlist", string.Empty, true, Constants.TEAM_MODULE, Constants.TEAM_FILE_NAME,
                result =>
                {
                    scrollViewer.ScrollToVerticalOffset(0);
                    listBox.ItemsSource = result.NewsGroups;
                });
        }

        #endregion

        private void NewsItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            News news = sender.GetDataContext<News>();

            string naviString = string.Empty;
            switch (news.Type)
            {
                case "0":
                    VideoPage.PlayVideo(this, news.ID, null);
                    break;
                case "1":
                    naviString = string.Format("/Pages/NewsDetailPage.xaml?{0}={1}&{2}={3}", NaviParam.NEWS_ID, news.ID, NaviParam.NEWS_TITLE, news.Title);
                    NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                    break;
                default:
                    break;
            }
        }
    }
}