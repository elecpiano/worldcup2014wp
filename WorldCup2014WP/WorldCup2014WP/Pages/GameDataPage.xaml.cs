﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;

namespace WorldCup2014WP.Pages
{
    public partial class GameDataPage : PhoneApplicationPage
    {
        #region Lifecycle

        public GameDataPage()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadScore();
            LoadSchedule();
        }

        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    base.OnNavigatingFrom(e);
        //    if (e.Uri.OriginalString != "app://external/")
        //    {
        //        HidePage();
        //    }
        //}

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarRefresh;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // refresh
            appBarRefresh = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefresh.Text = "刷新";
            appBarRefresh.Click += appBarRefresh_Click;
            ApplicationBar.Buttons.Add(appBarRefresh);
        }

        void appBarRefresh_Click(object sender, System.EventArgs e)
        {
            //LoadMedalTally();
        }

        #endregion

        #region Score

        ListDataLoader<ScoreItemData> scoreLoader = new ListDataLoader<ScoreItemData>();

        private void LoadScore()
        {
            if (scoreLoader.Busy)
            {
                return;
            }

            //snow1.IsBusy = true;

            scoreLoader.Load("getscore", string.Empty, true, Constants.GAME_DATA_MODULE, Constants.SCORE_FILE_NAME,
                result =>
                {
                    scoreListBox.ItemsSource = result;
                    scoreScrollViewer.ScrollToVerticalOffset(0);
                    //snow1.IsBusy = false;
                });
        }

        #endregion

        #region Schedule

        ListDataLoader<Schedule> scheduleLoader = new ListDataLoader<Schedule>();
        
        private void LoadSchedule()
        {
            if (scheduleLoader.Busy)
            {
                return;
            }

            //snow1.IsBusy = true;

            scheduleLoader.Load("getschedule", string.Empty, true, Constants.GAME_DATA_MODULE, Constants.SCHEDULE_FILE_NAME,
                result =>
                {
                    scheduleListBox.ItemsSource = result;
                    scheduleScrollViewer.ScrollToVerticalOffset(0);
                    //snow1.IsBusy = false;
                });
        }

        #endregion

    }
}