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

namespace WorldCup2014WP.Pages
{
    public partial class HomePage : PhoneApplicationPage
    {
        #region Property

        App App { get { return App.Current as App; } }

        #endregion

        #region Lifecycle

        public HomePage()
        {
            InitializeComponent();
            BuildApplicationBar();
            InitBannerControl();
            newsListBox.ItemsSource = newsList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LoadSplashImage();
            LoadBanner();
            LoadNews();
            //fadeAnimation.InstanceFade(this.contentPanel, 0d, 1d, Constants.NAVIGATION_DURATION, null);
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

        ApplicationBarIconButton appBarRefreshHome;
        ApplicationBarIconButton appBarRefreshNews;
        ApplicationBarMenuItem appBarSetting;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            //ApplicationBar.Opacity = 0.9;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // refresh home
            appBarRefreshHome = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefreshHome.Text = "刷新";
            appBarRefreshHome.Click += appBarRefreshHome_Click;

            // refresh news
            appBarRefreshNews = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefreshNews.Text = "刷新";
            appBarRefreshNews.Click += appBarRefreshNews_Click;

            appBarSetting = new ApplicationBarMenuItem("设置");
            appBarSetting.Click += appBarSetting_Click;

            SetAppBarForSplash();
        }

        void appBarRefreshHome_Click(object sender, EventArgs e)
        {
        }

        void appBarRefreshNews_Click(object sender, EventArgs e)
        {
            ReloadNews();
        }

        void appBarSetting_Click(object sender, EventArgs e)
        {
            SnsShare();
            //NavigationService.Navigate(new Uri("/Pages/SettingsPage.xaml", UriKind.Relative));
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
            ApplicationBar.Buttons.Add(appBarRefreshHome);
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
                default:
                    break;
            }

            VisualStateManager.GoToState(this, "vsHeaderBar" + panorama.SelectedIndex, true);
        }

        #endregion

        #region News

        GenericDataLoader<NewsList> newsLoader = new GenericDataLoader<NewsList>();
        ObservableCollection<News> newsList = new ObservableCollection<News>();
        int newsPageIndex = 1;
        int newsPageCount = 1;
        bool newsReloading = false;
        News newsMoreButtonItem = new News() { IsMoreButton = true };

        private void LoadNews()
        {
            if (newsLoader.Loaded || newsLoader.Busy)
            {
                return;
            }

            //busy
            snowNews.IsBusy = true;

            //load
            newsLoader.Load("getnewslist", string.Empty, true, Constants.NEWS_MODULE, string.Format(Constants.NEWS_LIST_FILE_NAME_FORMAT, newsPageIndex),
                list =>
                {
                    newsPageCount = list.TotalPageCount;

                    //remove more button
                    TryRemoveMoreButton();

                    if (newsReloading)
                    {
                        newsListScrollViewer.ScrollToVerticalOffset(0);
                        newsList.Clear();
                    }

                    foreach (var item in list.data)
                    {
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

        private bool ComparisonNews(News item1, News item2)
        {
            return item1.ID == item2.ID && item1.Image == item2.Image && item1.Title == item2.Title && item1.Description == item2.Description;
        }

        private void NewsItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            News news = sender.GetDataContext<News>();
            if (news.IsMoreButton)
            {
                LoadMoreNews();
                return;
            }

            switch (news.Type)
            {
                case "0":
                    VideoPage.PlayVideo(this, news.ID, this.snowNews);
                    break;
                case "1":
                    string naviString = string.Format("/Pages/NewsDetailPage.xaml?{0}={1}", NaviParam.NEWS_ID, news.ID);
                    NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                    break;
                default:
                    break;
            }
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

        #region SNS Share

        private void SnsShare()
        {
            WechatHelper.Current.Send("世界杯测试title", "世界杯测试desc", "Assets/ApplicationIcon.png", "http://google.com");
        }

        #endregion

    }
}