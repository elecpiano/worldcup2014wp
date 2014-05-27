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
    public partial class MagmaListPage : PhoneApplicationPage
    {
        public MagmaListPage()
        {
            InitializeComponent();
            newsListBox.ItemsSource = newsList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadNews();
        }

        #region News

        GenericDataLoader<NewsList> newsLoader = new GenericDataLoader<NewsList>();
        ObservableCollection<News> newsList = new ObservableCollection<News>();
        int newsPageIndex = 1;
        int newsPageCount = 1;
        bool newsReloading = false;
        News newsMoreButtonItem = new News() { IsMoreButton = true };
        List<DateTime> newsListDateHeaders = new List<DateTime>();

        private void LoadNews()
        {
            if (newsLoader.Loaded || newsLoader.Busy)
            {
                return;
            }

            ////busy
            //snowNews.IsBusy = true;

            //load
            newsLoader.Load("getmagmalist", string.Empty, true, Constants.MAGMA_MODULE, Constants.MAGMA_LIST_FILE_NAME,
                list =>
                {
                    newsPageCount = list.TotalPageCount;

                    //remove more button
                    TryRemoveMoreButton();

                    if (newsReloading)
                    {
                        newsListScrollViewer.ScrollToVerticalOffset(0);
                        newsList.Clear();
                        newsListDateHeaders.Clear();
                    }

                    foreach (var item in list.data)
                    {
                        //if (!newsListDateHeaders.Contains(item.Time.Date))
                        //{
                        //    newsListDateHeaders.Add(item.Time.Date);
                        //    News dateHeader = new News() { IsDateHeader = true, HeaderDate = item.Time.Date };
                        //    newsList.Add(dateHeader);
                        //}

                        newsList.Add(item);
                    }

                    if (newsPageIndex < newsPageCount)
                    {
                        //add more button
                        EnsureMoreButton();
                    }

                    ////not busy
                    //snowNews.IsBusy = false;
                });
        }

        private void LoadMoreNews()
        {
            newsPageIndex++;
            if (newsPageIndex <= newsPageCount)
            {
                newsLoader.Loaded = false;
                newsReloading = false;
                LoadNews();
            }
        }

        private void ReloadNews()
        {
            //reset values
            newsPageIndex = 1;
            newsPageCount = 1;
            newsLoader.Loaded = false;
            newsReloading = true;
            LoadNews();
        }

        private void NewsItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            News news = sender.GetDataContext<News>();
            if (news.IsMoreButton)
            {
                LoadMoreNews();
                return;
            }

            NewsHandler.OnNewsTap(this, news);
        }

        private void TryRemoveMoreButton()
        {
            if (newsList.Contains(newsMoreButtonItem))
            {
                newsList.Remove(newsMoreButtonItem);
            }
        }

        private void EnsureMoreButton()
        {
            if (!newsList.Contains(newsMoreButtonItem))
            {
                newsList.Add(newsMoreButtonItem);
            }
        }

        #endregion

    }
}