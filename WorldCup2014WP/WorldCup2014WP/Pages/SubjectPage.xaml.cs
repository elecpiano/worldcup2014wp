﻿using System;
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
    public partial class SubjectPage : PhoneApplicationPage
    {
        private string subjectID = string.Empty;

        #region Lifecycle

        public SubjectPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            subjectID = NavigationContext.QueryString[NaviParam.SUBJECT_ID];
            Loadsubject();
        }

        #endregion

        #region data

        DataLoader<Subject> subjectLoader = new DataLoader<Subject>();

        private void Loadsubject()
        {
            if (subjectLoader.Loaded || subjectLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            //load
            subjectLoader.Load("getsubject", "&id=" + subjectID, true, Constants.SUBJECT_MODULE, string.Format(Constants.SUBJECT_FILE_NAME_FORMAT, subjectID),
                result =>
                {
                    focusSlideShow.SetItemsSource(result.FocusList);

                    scrollViewer.ScrollToVerticalOffset(0);
                    newsGroupListBox.ItemsSource = result.NewsGroups;
                    //subjectNewsList.Clear();

                    //foreach (var item in result.data)
                    //{
                    //    subjectNewsList.Add(item);
                    //}

                    //not busy
                    //snowNews.IsBusy = false;
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