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
using WorldCup2014WP.Controls;

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
            NewsHandler.OnNewsTap(this, news);
        }
    }
}