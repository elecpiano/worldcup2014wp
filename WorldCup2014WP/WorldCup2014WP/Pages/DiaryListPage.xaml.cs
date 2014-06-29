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
using WorldCup2014WP.Controls;

namespace WorldCup2014WP.Pages
{
    public partial class DiaryListPage : PhoneApplicationPage
    {
        #region Property

        private string authorId = string.Empty;
        private string authorName = string.Empty;
        private string authorFace = string.Empty;

        #endregion

        #region Lifecycle

        public DiaryListPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            authorId = NavigationContext.QueryString[NaviParam.AUTHOR_ID];
            authorName = NavigationContext.QueryString[NaviParam.AUTHOR_NAME];
            authorFace = NavigationContext.QueryString[NaviParam.AUTHOR_FACE];

            nameTextBlock.Text = authorName;
            faceImage.DataContext = authorFace;

            LoadData();
        }

        #endregion

        #region Data

        GenericDataLoader<DiaryList> dataLoader = new GenericDataLoader<DiaryList>();
        //ObservableCollection<News> recommendationNewsList = new ObservableCollection<News>();

        private void LoadData()
        {
            if (dataLoader.Loaded || dataLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            //load
            dataLoader.Load("getdiarylist", "&id=" + authorId, true, Constants.DIARY_MODULE, string.Format(Constants.DIARY_LIST_FILE_NAME_FORMAT, authorId),
                result =>
                {
                    if (result==null)
                    {
                        return;
                    }
                    if (result.data ==null)
                    {
                        return;
                    }
                    coverImage.DataContext = result.BigImage;
                    diaryListBox.ItemsSource = result.data;
                    //not busy
                    //snowNews.IsBusy = false;
                });
        }

        #endregion

        private void DiaryItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            DiaryItem item = sender.GetDataContext<DiaryItem>();
            NewsHandler.OnNewsTap(this, item.ID,string.Empty, item.Image, item.Name);
        }

    }
}