using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using WorldCup2014WP.Utility;
using WorldCup2014WP.DataContext;
using WorldCup2014WP.Models;
using Microsoft.Phone.Shell;
using System.Windows;
using System;

namespace WorldCup2014WP.Pages
{
    public partial class MySubscriptionPage : PhoneApplicationPage
    {
        #region Lifecycle

        public MySubscriptionPage()
        {
            InitializeComponent();
            BuildApplicationBar();

            subscriptionListBox.ItemsSource = subscriptionList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadSubscriptionList();
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

        ObservableCollection<EPG> subscriptionList = new ObservableCollection<EPG>();

        private void LoadSubscriptionList()
        {
            subscriptionList.Clear();
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
                    if (item.StartTime <= DateTime.Now)
                    {
                        item.Expired = true;
                    }
                    subscriptionList.Add(item);
                }
            }

            //ShowPage();
        }

        private void CheckNoData()
        {
            this.noData.Visibility = subscriptionList.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion

        #region UnSubscribe

        private void UnSubscribe_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EPG epg = sender.GetDataContext<EPG>();
            if (epg != null)
            {
                ReminderHelper.RemoveReminder(epg.ID);
            }
            SubscriptionDataContext.Current.RemoveSubscription(epg.ID);
            subscriptionList.Remove(epg);
            toast.ShowMessage("成功取消预约。");
            CheckNoData();
            HomePage.ReloatEpgList = true;
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
            subscriptionList.Clear();
            toast.ShowMessage("成功取消全部预约。");
            CheckNoData();
            HomePage.ReloatEpgList = true;
        }

        #endregion

    }
}