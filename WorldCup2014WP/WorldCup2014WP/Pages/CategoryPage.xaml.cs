using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;
using WorldCup2014WP.Controls;
using System.Windows.Input;
using WorldCup2014WP.DataContext;
using System.Linq;

namespace WorldCup2014WP.Pages
{
    public partial class CategoryPage : PhoneApplicationPage
    {
        #region Property

        public static Category SelectedCategory { get; set; }

        #endregion

        #region Lifecycle

        public CategoryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (SelectedCategory != null)
            {
                this.topBar.SecondaryHeader = SelectedCategory.Title;
                this.pivotItem1.Header = new CategoryPivotHeaderData() { Title = "赛程赛果", Image = SelectedCategory.AdImage };
                this.pivotItem2.Header = new CategoryPivotHeaderData() { Title = "冬奥ABC", Image = SelectedCategory.AdImage2 };
            }

            LoadSchedules();
            LoadABC();
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

        #region VirtualizingStackPanel

        StackPanel scheduleItemsPanel = null;
        private void scheduleListBoxItemsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            scheduleItemsPanel = sender as StackPanel;
        }

        #endregion

        #region Schedule List

        ListDataLoader<GameSchedule> scheduleLoader = new ListDataLoader<GameSchedule>();
        List<GameSchedule> scheduleList = new List<GameSchedule>();

        private void LoadSchedules()
        {
            if (scheduleLoader.Loaded || scheduleLoader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            scheduleLoader.Load("getschedule", "&id=" + SelectedCategory.ID, true, Constants.SCHEDULE_MODULE, string.Format(Constants.SCHEDULE_FILE_NAME_FORMAT, SelectedCategory.ID),
                list =>
                {
                    var subscriptionList = GetSubscriptionList();
                    foreach (var item in list)
                    {
                        if (subscriptionList.Any(x => x.ID == item.ID))
                        {
                            item.Subscribed = true;
                        }
                        item.ArrowImage = "/Assets/Images/ArrowDown.png";
                    }

                    scheduleList = list;
                    this.scheduleListBox.ItemsSource = scheduleList;
                    snow1.IsBusy = false;
                    //ShowPage();
                });
        }

        private List<GameSchedule> GetSubscriptionList()
        {
            return SubscriptionDataContext.Current.LoadSubscriptions();
        }

        #endregion

        #region Result

        DataLoader<GameResult> resultLoader = new DataLoader<GameResult>();
        GameResultPanel gameResultPanel = null;
        GameSchedule expandedSchedule = null;

        private void LoadGameResult(GameSchedule schedule)
        {
            if (resultLoader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            resultLoader.Load("getresult", "&id=" + schedule.ID, true, Constants.SCHEDULE_MODULE, string.Format(Constants.RESULT_FILE_NAME_FORMAT, schedule.ID),
                list =>
                {
                    ShowResultPanel(schedule, list);
                    snow1.IsBusy = false;
                });
        }

        private void Schedule_Tap(object sender, GestureEventArgs e)
        {
            var schedule = sender.GetDataContext<GameSchedule>();
            if (schedule == expandedSchedule)
            {
                HideResultPanel();
            }
            else
            {
                if (expandedSchedule != null)
                {
                    HideResultPanel();
                }
                LoadGameResult(schedule);
            }
        }

        private void ShowResultPanel(GameSchedule schedule, GameResult result)
        {
            if (gameResultPanel == null)
            {
                gameResultPanel = new GameResultPanel();
            }
            if (scheduleItemsPanel.Children.Contains(gameResultPanel))
            {
                scheduleItemsPanel.Children.Remove(gameResultPanel);
            }

            gameResultPanel.DataContext = result;

            int index = scheduleList.IndexOf(schedule);
            scheduleItemsPanel.Children.Insert(index + 1, gameResultPanel);
            bool hasResult = false;
            if (result != null && result.RankList != null && result.RankList.Length > 0)
            {
                hasResult = true;
            }
            gameResultPanel.Show(hasResult);
            schedule.ArrowImage = "/Assets/Images/ArrowUp.png";
            expandedSchedule = schedule;
        }

        private void HideResultPanel()
        {
            gameResultPanel.Hide(scheduleItemsPanel);
            expandedSchedule.ArrowImage = "/Assets/Images/ArrowDown.png";
            expandedSchedule = null;
        }

        #endregion

        #region Subscribe

        private void Subscribe_Tap(object sender, GestureEventArgs e)
        {
            GameSchedule schedule = sender.GetDataContext<GameSchedule>();
            if (schedule.Subscribed)
            {
                ReminderHelper.RemoveReminder(schedule.ID);
                SubscriptionDataContext.Current.RemoveSubscription(schedule.ID);
                schedule.Subscribed = false;
                toast.ShowMessage("成功取消预约。");
            }
            else
            {
                schedule.StartTime = schedule.StartTime;// DateTime.Now.AddSeconds(30);
                try
                {
                    var successful = ReminderHelper.AddReminder(schedule.ID, schedule.Category, schedule.Match, schedule.StartTime,"/Pages/HomePage.xaml?");
                    if (successful)
                    {
                        SubscriptionDataContext.Current.AddSubscription(schedule);
                        schedule.Subscribed = true;
                        toast.ShowMessage("成功添加预约。");
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion

        #region Category ABC

        DataLoader<HTML> htmlLoader = new DataLoader<HTML>();

        private void LoadABC()
        {
            if (htmlLoader.Loaded || htmlLoader.Busy)
            {
                return;
            }

            snow2.IsBusy = true;

            htmlLoader.Load("getabcdetail", "&id=" + SelectedCategory.ID, true, Constants.SCHEDULE_MODULE, string.Format(Constants.CATEGORY_ABC_FILE_NAME_FORMAT, SelectedCategory.ID),
                html =>
                {
                    browser.NavigateToString(html.Content);
                    snow2.IsBusy = false;
                });
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