using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;
using System.Linq;

namespace WorldCup2014WP.Pages
{
    public partial class CalendarPage : PhoneApplicationPage
    {
        #region Property

        #endregion

        #region Lifecycle

        public CalendarPage()
        {
            InitializeComponent();
            BuildApplicationBar();
            daysListBox.ItemsSource = calendarList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadCalendar();
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

        #region News

        ListDataLoader<CalendarItem> calendarLoader = new ListDataLoader<CalendarItem>();
        ObservableCollection<CalendarItem> calendarList = new ObservableCollection<CalendarItem>();

        private void LoadCalendar()
        {
            if (calendarLoader.Loaded || calendarLoader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            calendarLoader.Load("getcalendar", string.Empty, true, Constants.CALENDAR_MODULE, Constants.CALENDAR_FILE_NAME,
                list =>
                {
                    calendarList.Clear();
                    if (list != null)
                    {
                        var orderedList = list.OrderBy(x => x.Date);

                        int gameDate = 1;
                        foreach (var item in orderedList)
                        {
                            item.GameDate = "DAY " + gameDate.ToString();
                            //item.DateString = item.Date.ToString("M月d日 dddd");
                            gameDate++;
                            calendarList.Add(item);
                        }
                    }
                    scrollViewer.ScrollToVerticalOffset(0);
                    snow1.IsBusy = false;
                });
        }

        private bool Comparison(CalendarItem item1, CalendarItem item2)
        {
            return item1.Date == item2.Date && item1.Image == item2.Image;
        }

        private void Day_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            CalendarItem item = sender.GetDataContext<CalendarItem>();
            string naviStr = string.Format("/Pages/EPGListPage.xaml?{0}={1}", NaviParam.CALENDAR_DATE, item.Date.ToString("yyyy-MM-dd"));
            NavigationService.Navigate(new Uri(naviStr, UriKind.Relative));
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
            calendarLoader.Loaded = false;
            LoadCalendar();
        }

        #endregion


        //#region Page Navigation Transition

        //FadeAnimation fadeAnimation = new FadeAnimation();
        //MoveAnimation moveAnimation = new MoveAnimation();

        //private void ShowPage()
        //{
        //    contentPanel.UpdateLayout();
        //    moveAnimation.InstanceMoveFromTo(this.contentPanel, 0, 90, 0, 0, Constants.NAVIGATION_DURATION, null);
        //    fadeAnimation.InstanceFade(this.contentPanel, 0d, 1d, Constants.NAVIGATION_DURATION, null);
        //}

        //private void HidePage()
        //{
        //    moveAnimation.InstanceMoveFromTo(this.contentPanel, 0, 0, 0, 90, Constants.NAVIGATION_DURATION, null);
        //    fadeAnimation.InstanceFade(this.contentPanel, 1d, 0d, Constants.NAVIGATION_DURATION, null);
        //}

        //#endregion

    }
}