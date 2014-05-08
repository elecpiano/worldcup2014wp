using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using WorldCup2014WP.Utility;
using WorldCup2014WP.DataContext;
using WorldCup2014WP.Models;
using Microsoft.Phone.Shell;
using System.Windows;

namespace WorldCup2014WP.Pages
{
    public partial class MySubscriptionPage : PhoneApplicationPage
    {
        #region Lifecycle

        public MySubscriptionPage()
        {
            InitializeComponent();
            BuildApplicationBar();

            scheduleListBox.ItemsSource = scheduleList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadScheduleList();
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

        #region Subscription List

        ObservableCollection<GameSchedule> scheduleList = new ObservableCollection<GameSchedule>();

        private void LoadScheduleList()
        {
            scheduleList.Clear();
            var list = SubscriptionDataContext.Current.LoadSubscriptions();
            if (list.Count == 0)
            {
                this.noData.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.noData.Visibility = System.Windows.Visibility.Collapsed;
                foreach (var item in list)
                {
                    scheduleList.Add(item);
                }
            }

            //ShowPage();
        }

        private void CheckNoData()
        {
            this.noData.Visibility = scheduleList.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion

        #region UnSubscribe

        private void UnSubscribe_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            GameSchedule schedule = sender.GetDataContext<GameSchedule>();
            if (schedule != null)
            {
                ReminderHelper.RemoveReminder(schedule.ID);
            }
            SubscriptionDataContext.Current.RemoveSubscription(schedule.ID);
            scheduleList.Remove(schedule);
            toast.ShowMessage("成功取消预约。");
            CheckNoData();
        }

        #endregion

        #region App Bar

        ApplicationBarMenuItem appBarClear;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            //ApplicationBar.Opacity = 0.9;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            //clear
            appBarClear = new ApplicationBarMenuItem("清空");
            appBarClear.Click += appBarClear_Click;
            ApplicationBar.MenuItems.Add(appBarClear);
        }

        void appBarClear_Click(object sender, System.EventArgs e)
        {
            ReminderHelper.ClearReminders();
            SubscriptionDataContext.Current.ClearSubscriptions();
            scheduleList.Clear();
            toast.ShowMessage("成功取消全部预约。");
            CheckNoData();
        }

        #endregion

        //#region Page Navigation Transition

        //FadeAnimation fadeAnimation = new FadeAnimation();
        //MoveAnimation moveAnimation = new MoveAnimation();

        //private void ShowPage()
        //{
        //    this.contentPanel.UpdateLayout();
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