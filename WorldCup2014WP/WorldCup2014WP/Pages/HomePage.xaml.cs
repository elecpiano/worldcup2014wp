using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Controls;
using System.Collections.ObjectModel;
using Microsoft.Phone.Tasks;
using System.Collections.Generic;

namespace WorldCup2014WP.Pages
{
    public partial class HomePage : PhoneApplicationPage
    {
        #region Property

        App App { get { return App.Current as App; } }
        public static bool ReloatEpgList = true;

        #endregion

        #region Lifecycle

        public HomePage()
        {
            InitializeComponent();
            BuildApplicationBar();
            InitBannerControl();
            InitEpgList();
            newsListBox.ItemsSource = newsList;
            recommendationNewsListBox.ItemsSource = recommendationNewsList;
            authorListBox.ItemsSource = authorList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.Toast = this.toast;

            LoadSplashImage();
            LoadBanner();
            if (ReloatEpgList)
            {
                LoadEpg(true);
                ReloatEpgList = false;
            }
            LoadRecommendation();
            LoadAuthorList();
            LoadNews();
            //fadeAnimation.InstanceFade(this.contentPanel, 0d, 1d, Constants.NAVIGATION_DURATION, null);
            this.bubbles.IsBusy = true;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
        }

        #endregion

        #region Splash

        DataLoader<Splash> splashLoader = new DataLoader<Splash>();
        ImageHelper imageHelperSplash = new ImageHelper();
        string openedSplashImageSource = string.Empty;
        private void LoadSplashImage()
        {
            //DisplayLocalSplashImage();

            if (splashLoader.Loaded || splashLoader.Busy)
            {
                return;
            }

            bigSnow.IsBusy = true;

            splashLoader.Load("getsplash", string.Empty, true, Constants.SPLASH_MODULE, Constants.SPLASH_FILE_NAME,
                splash =>
                {
                    if (openedSplashImageSource == splash.Image)
                    {
                        return;
                    }
                    openedSplashImageSource = splash.Image;
                    this.splashImage.Source = new BitmapImage(new Uri(splash.Image, UriKind.RelativeOrAbsolute));
                });

            //splashLoader.LoadWithoutCaching("getsplash",
            //    splash =>
            //    {
            //        if (splash != null)
            //        {
            //            imageHelperSplash.Download(splash.Image, Constants.SPLASH_MODULE, Constants.SPLASH_FILE_NAME, SplashDownLoadCallback);
            //        }
            //    });

        }

        //private void SplashDownLoadCallback()
        //{
        //    Dispatcher.BeginInvoke(() =>
        //    {
        //        DisplayLocalSplashImage();
        //        bigSnow.IsBusy = false;
        //    });
        //}

        //private async void DisplayLocalSplashImage()
        //{
        //    BitmapImage source = await imageHelperSplash.ReadImage(Constants.SPLASH_MODULE, Constants.SPLASH_FILE_NAME);
        //    this.splashImage.Source = source;
        //}

        private void splashImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            bigSnow.IsBusy = !bigSnow.IsBusy;
        }

        #endregion

        #region Banner

        Banner theBanner = null;
        ListDataLoader<Banner> bannerLoader = new ListDataLoader<Banner>();

        private void InitBannerControl()
        {
            this.bannerControl.HostingPage = this;
        }

        private void DismissBanner()
        {
            App.DismissedBannerId = theBanner.ID;
            this.bannerControl.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void LoadBanner()
        {
            if (bannerLoader.Loaded || bannerLoader.Busy)
            {
                return;
            }

            bannerLoader.Load("getbanner", string.Empty, true, Constants.BANNER_MODULE, Constants.BANNER_FILE_NAME,
                list =>
                {
                    if (list.Count > 0)
                    {
                        theBanner = list[0];
                        if (theBanner.ID != App.DismissedBannerId)
                        {
                            bannerControl.Visibility = System.Windows.Visibility.Visible;
                            this.DataContext = theBanner;
                        }
                        else
                        {
                            bannerControl.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                });
        }

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarRefreshEpg;
        ApplicationBarIconButton appBarRefreshNews;
        ApplicationBarMenuItem appBarSetting;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            //ApplicationBar.Opacity = 0.9;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // refresh epg
            appBarRefreshEpg = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefreshEpg.Text = "刷新";
            appBarRefreshEpg.Click += appBarRefreshEpg_Click;

            // refresh news
            appBarRefreshNews = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefreshNews.Text = "刷新";
            appBarRefreshNews.Click += appBarRefreshNews_Click;

            appBarSetting = new ApplicationBarMenuItem("设置");
            appBarSetting.Click += appBarSetting_Click;

            SetAppBarForSplash();
        }

        void appBarRefreshEpg_Click(object sender, EventArgs e)
        {
            LoadEpg(true);
        }

        void appBarRefreshNews_Click(object sender, EventArgs e)
        {
            ReloadNews();
        }

        void appBarSetting_Click(object sender, EventArgs e)
        {
            //SnsShareWechat();
            //SnsShareWeibo();
            NavigationService.Navigate(new Uri("/Pages/SettingsPage.xaml", UriKind.Relative));
        }

        private void ClearAppBar()
        {
            ApplicationBar.Buttons.Clear();
            ApplicationBar.MenuItems.Clear();
        }

        private void SetAppBarForSplash()
        {
            ClearAppBar();
            ApplicationBar.MenuItems.Add(appBarSetting);
            //ApplicationBar.Mode = ApplicationBarMode.Minimized;
        }

        private void SetAppBarForHome()
        {
            ClearAppBar();
            ApplicationBar.Buttons.Add(appBarRefreshEpg);
            ApplicationBar.MenuItems.Add(appBarSetting);
            //ApplicationBar.Mode = ApplicationBarMode.Default;
        }

        private void SetAppBarForNews()
        {
            ClearAppBar();
            ApplicationBar.Buttons.Add(appBarRefreshNews);
            ApplicationBar.MenuItems.Add(appBarSetting);
            //ApplicationBar.Mode = ApplicationBarMode.Default;
        }

        private void SetAppBarForMore()
        {
            ClearAppBar();
            ApplicationBar.MenuItems.Add(appBarSetting);
            //ApplicationBar.Mode = ApplicationBarMode.Minimized;
        }

        #endregion

        #region Panorama Selection

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (panorama.SelectedIndex)
            {
                case 0:
                    SetAppBarForSplash();
                    break;
                case 1:
                    SetAppBarForHome();
                    break;
                case 2:
                    SetAppBarForNews();
                    break;
                case 3:
                    SetAppBarForMore();
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    break;
            }

            //this.bubbles.IsBusy = panorama.SelectedIndex == 5;
            //VisualStateManager.GoToState(this, "vsHeaderBar" + panorama.SelectedIndex, true);
        }

        #endregion

        #region EPG

        private void InitEpgList()
        {
            epgList.HostingPage = this;
        }

        private void LoadEpg(bool reload)
        {
            //DateTime today = new DateTime(2014, 2, 18);// TO-DO: DateTime.Today;
            DateTime today = DateTime.Today;

            if (reload)
            {
                epgList.ReloadEpg(today);
            }
            else
            {
                epgList.LoadEpg(today);
            }
        }

        private void EpgMoreButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowPopup("vsCalendarShown");
            //NavigationService.Navigate(new Uri("/Pages/CalendarPage.xaml", UriKind.Relative));
        }

        #endregion

        #region Popup Management

        bool popupShown = false;

        private void ShowPopup(string visualState)
        {
            popupShown = true;
            VisualStateManager.GoToState(this, visualState, true);
            popupMask.Opacity = 1;
            popupMask.IsHitTestVisible = true;
        }

        private void ClosePopup()
        {
            VisualStateManager.GoToState(this, "vsCalendarHidden", true);
            popupShown = false;
            popupMask.Opacity = 0;
            popupMask.IsHitTestVisible = false;
        }

        private void popupMask_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ClosePopup();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (popupShown)
            {
                e.Cancel = true;
                ClosePopup();
                return;
            }

            base.OnBackKeyPress(e);
        }

        #endregion

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

            //busy
            snowNews.IsBusy = true;

            //load
            newsLoader.Load("getnewslist", "&page=" + newsPageIndex.ToString(), true, Constants.NEWS_MODULE, string.Format(Constants.NEWS_LIST_FILE_NAME_FORMAT, newsPageIndex),
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
                        if (!newsListDateHeaders.Contains(item.Time.Date))
                        {
                            newsListDateHeaders.Add(item.Time.Date);
                            News dateHeader = new News() { IsDateHeader = true, HeaderDate = item.Time.Date };
                            newsList.Add(dateHeader);
                        }

                        newsList.Add(item);
                    }

                    if (newsPageIndex < newsPageCount)
                    {
                        //add more button
                        EnsureMoreButton();
                    }

                    //not busy
                    snowNews.IsBusy = false;
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

        #region Recommendation

        GenericDataLoader<Recommendation> recommendationLoader = new GenericDataLoader<Recommendation>();
        ObservableCollection<News> recommendationNewsList = new ObservableCollection<News>();

        private void LoadRecommendation()
        {
            if (recommendationLoader.Loaded || recommendationLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            //load
            recommendationLoader.Load("getrecommends", string.Empty, true, Constants.RECOMMENDATION_MODULE, Constants.RECOMMENDATION_FILE_NAME,
                result =>
                {
                    recommendationSlideShow.SetItemsSource(result.focus);

                    recommendationNewsListScrollViewer.ScrollToVerticalOffset(0);
                    recommendationNewsList.Clear();

                    foreach (var item in result.data)
                    {
                        recommendationNewsList.Add(item);
                    }

                    //not busy
                    //snowNews.IsBusy = false;
                });
        }

        #endregion

        #region Author

        GenericDataLoader<AuthorList> authorListLoader = new GenericDataLoader<AuthorList>();
        ObservableCollection<Author> authorList = new ObservableCollection<Author>();

        private void LoadAuthorList()
        {
            if (authorListLoader.Loaded || authorListLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            //load
            authorListLoader.Load("getauthorlist", string.Empty, true, Constants.AUTHOR_MODULE, Constants.AUTHOR_FILE_NAME,
                result =>
                {
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

        #region SNS Share

        private void SnsShareWechat()
        {
            WechatHelper.Current.Send("世界杯测试title", "世界杯测试desc", "Assets/ApplicationIcon.png", "http://google.com");
        }

        private void SnsShareWeibo()
        {
            PhotoChooserTask task = new PhotoChooserTask();
            task.Show();
            task.Completed += task_Completed;
        }

        void task_Completed(object sender, PhotoResult e)
        {
            (sender as PhotoChooserTask).Completed -= task_Completed;
            if (e.ChosenPhoto != null)
            {
                WeiboAssociation.Share(e.ChosenPhoto, "CCTV 世界杯 微博测试");
            }
        }

        #endregion

        #region More Page

        private void userCenter_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/UserCenterPage.xaml", UriKind.Relative));
        }

        private void gameCalendar_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/CalendarPage.xaml", UriKind.Relative));
        }
        private void gameData_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/GameDataPage.xaml", UriKind.Relative));
        }

        private void stadium_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/StadiumListPage.xaml", UriKind.Relative));
        }

        private void team_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/TeamPage.xaml", UriKind.Relative));
        }

        private void magma_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MagmaListPage.xaml", UriKind.Relative));
        }

        #endregion


    }
}