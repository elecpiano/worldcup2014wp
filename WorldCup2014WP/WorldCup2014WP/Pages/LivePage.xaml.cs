using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;
using System.Windows.Media.Imaging;
using WorldCup2014WP.Controls;
using System.Windows.Threading;

namespace WorldCup2014WP.Pages
{
    public partial class LivePage : PhoneApplicationPage
    {
        #region Property

        private string liveID = string.Empty;
        private string liveTitle = string.Empty;
        private string liveImage = string.Empty;

        #endregion

        #region Lifecycle

        public LivePage()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            liveID = NavigationContext.QueryString[NaviParam.LIVE_ID];
            liveImage = NavigationContext.QueryString[NaviParam.LIVE_IMAGE];
            liveTitle = NavigationContext.QueryString[NaviParam.LIVE_TITLE];

            this.titleImage.Source = new BitmapImage(new Uri(liveImage, UriKind.RelativeOrAbsolute));
            this.titleTextBlock1.Text = this.titleTextBlock2.Text = liveTitle;

            LoadData();
            StartTimer();

        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            StopTimer();
        }

        #endregion

        #region Data

        GenericDataLoader<LiveData> liveLoader = new GenericDataLoader<LiveData>();

        private void LoadData()
        {
            if (liveLoader.Loaded || liveLoader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            liveLoader.Load("getlivepage", "&id=" + liveID, true, Constants.LIVE_MODULE, string.Format(Constants.LIVE_FILE_NAME_FORMAT, liveID),
                data =>
                {
                    if (data==null)
                    {
                        return;
                    }
                    PopulateScore(data);
                    PopulateRankList(data);
                    PopulateLineItems(data);

                    snow1.IsBusy = false;
                });
        }

        private void PopulateScore(LiveData data)
        {
            if (data.Score != null)
            {
                scorePanel.DataContext = data.Score;
                scorePanel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                scorePanel.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void PopulateRankList(LiveData data)
        {
            if (data.RankList != null)
            {
                rankListBox.ItemsSource = data.RankList;
            }
            else
            {
                rankListBox.ItemsSource = null;
            }
        }

        private void PopulateLineItems(LiveData data)
        {
            lineItemsStackPanel.Children.Clear();

            if (data == null)
            {
                return;
            }
            if (data.LineItems == null)
            {
                return;
            }

            FrameworkElement control = null;

            foreach (var item in data.LineItems)
            {
                switch (item.Type.ToString())
                {
                    case "0"://video
                        control = new LivePageItemVideo() { HostingPage = this };
                        break;
                    case "1"://news
                        control = new LivePageItemNews() { HostingPage = this };
                        break;
                    case "2"://album
                        if (item.Album.Length > 3)
                        {
                            List<AlbumItem> newList = new List<AlbumItem>();
                            for (int i = 0; i < 3; i++)
                            {
                                newList.Add(item.Album[i]);
                            }
                            item.Album = newList.ToArray();
                        }
                        control = new LivePageItemAlbum() { HostingPage = this };
                        break;
                    case "12"://live text
                        control = new LivePageItemLiveText() { HostingPage = this };
                        break;
                    default:
                        control = null;
                        break;
                }

                if (control != null)
                {
                    control.DataContext = item;
                    lineItemsStackPanel.Children.Add(control);
                }
            }
        }

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarRefresh;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            //ApplicationBar.Opacity = 0.9;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // refresh
            appBarRefresh = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefresh.Text = "刷新";
            appBarRefresh.Click += appBarRefresh_Click;
            ApplicationBar.Buttons.Add(appBarRefresh);
        }

        void appBarRefresh_Click(object sender, System.EventArgs e)
        {
            liveLoader.Loaded = false;
            LoadData();
        }

        #endregion

        #region Timer

        private DispatcherTimer timer = null;
        private void StartTimer()
        {
            if (timer==null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(30);
                timer.Tick += timer_Tick;
            }
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            liveLoader.Loaded = false;
            LoadData();
        }

        private void StopTimer()
        {
            if (timer!=null)
            {
                timer.Stop();
            }
        }

        #endregion

    }
}