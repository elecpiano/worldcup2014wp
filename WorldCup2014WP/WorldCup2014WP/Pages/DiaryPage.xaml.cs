using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;
using System.Collections.ObjectModel;

namespace WorldCup2014WP.Pages
{
    public partial class DiaryPage : PhoneApplicationPage
    {
        public DiaryPage()
        {
            InitializeComponent();
            authorListBox.ItemsSource = authorList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadData();
        }

        #region Data

        GenericDataLoader<AuthorList> authorListLoader = new GenericDataLoader<AuthorList>();
        ObservableCollection<Author> authorList = new ObservableCollection<Author>();

        private void LoadData()
        {
            if (authorListLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            //load
            authorListLoader.Load("getauthorlist", string.Empty, true, Constants.AUTHOR_MODULE, Constants.AUTHOR_FILE_NAME,
                result =>
                {
                    if (result ==null)
                    {
                        return;
                    }
                    if (result.data ==null)
                    {
                        return;
                    }
                    authorListScrollViewer.ScrollToVerticalOffset(0);
                    authorList.Clear();

                    foreach (var item in result.data)
                    {
                        authorList.Add(item);
                    }

                    //not busy
                    //snowNews.IsBusy = false;
                });
        }

        private void Author_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Author author = sender.GetDataContext<Author>();
            string naviString = string.Format("/Pages/DiaryListPage.xaml?{0}={1}&{2}={3}&{4}={5}",
                NaviParam.AUTHOR_ID, author.ID,
                NaviParam.AUTHOR_NAME, author.Name,
                NaviParam.AUTHOR_FACE, author.Face);
            NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
        }

        #endregion
    }
}