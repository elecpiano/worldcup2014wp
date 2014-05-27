using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Xna.Framework.Media;
using System.Windows.Controls;
using WorldCup2014WP.Controls;

namespace WorldCup2014WP.Pages
{
    public partial class StadiumListPage : PhoneApplicationPage
    {
        #region Lifecycle

        public StadiumListPage()
        {
            InitializeComponent();
            BuildApplicationBar();
            stadiumListBox.ItemsSource = stadiumList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadStadiums();
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

        #region StadiumList

        ObservableCollection<Stadium> stadiumList = new ObservableCollection<Stadium>();
        ListDataLoader<Stadium> stadiumLoader = new ListDataLoader<Stadium>();

        private void LoadStadiums()
        {
            if (stadiumLoader.Loaded || stadiumLoader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            stadiumLoader.Load("getstadiumlist", string.Empty, true, Constants.STADIUM_MODULE, Constants.STADIUM_LIST_FILE_NAME,
                list =>
                {
                    stadiumList.Clear();
                    foreach (var item in list)
                    {
                        stadiumList.Add(item);
                    }
                    scrollViewer.ScrollToVerticalOffset(0);
                    snow1.IsBusy = false;
                });
        }

        private bool Comparison(Stadium item1, Stadium item2)
        {
            return item1.ID == item2.ID && item1.NameCN == item2.NameCN && item1.Image == item2.Image;
        }

        private void Stadium_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Stadium stadium = sender.GetDataContext<Stadium>();
            string[] paramArray = new string[] { NaviParam.STADIUM_ID, stadium.ID, NaviParam.STADIUM_NAME, stadium.NameCN };
            string strUri = string.Format("/Pages/StadiumDetailPage.xaml?{0}={1}&{2}={3}", paramArray);
            NavigationService.Navigate(new Uri(strUri, UriKind.Relative));
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
            stadiumLoader.Loaded = false;
            LoadStadiums();
        }

        #endregion

    }
}