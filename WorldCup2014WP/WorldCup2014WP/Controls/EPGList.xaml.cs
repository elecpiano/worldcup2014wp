using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;
using System.Windows;
using System.Windows.Input;
using WorldCup2014WP.DataContext;

namespace WorldCup2014WP.Controls
{
    public partial class EPGList : UserControl
    {
        #region Property

        App App { get { return App.Current as App; } }

        public Page HostingPage { get; set; }

        //private QuickSelector quickSelector = null;
        //public QuickSelector QuickSelector
        //{
        //    get
        //    {
        //        return quickSelector;
        //    }
        //    set
        //    {
        //        quickSelector = value;
        //        if (quickSelector != null)
        //        {
        //            InitQuickSelector();
        //        }
        //    }
        //}

        //ItemsPanelMargin
        public Thickness ItemsPanelMargin
        {
            get { return (Thickness)GetValue(ItemsPanelMarginProperty); }
            set { SetValue(ItemsPanelMarginProperty, value); }
        }
        public static readonly DependencyProperty ItemsPanelMarginProperty =
            DependencyProperty.Register("ItemsPanelMargin", typeof(Thickness), typeof(EPGList), new PropertyMetadata(new Thickness(0), OnItemsPanelMarginPropertyChanged));

        private static void OnItemsPanelMarginPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EPGList control = d as EPGList;
            if (control.ItemsPanel != null)
            {
                Thickness newValue = (Thickness)e.NewValue;
                control.ItemsPanel.Margin = newValue;
            }
        }

        #endregion

        #region Lifecycle

        public EPGList()
        {
            InitializeComponent();
            epgListBox.ItemsSource = epgList;
        }

        #endregion

        #region EPG List

        ObservableCollection<EPG> epgList = new ObservableCollection<EPG>();
        ListDataLoader<EPG> epgLoader = new ListDataLoader<EPG>();

        public void LoadEpg(DateTime date)
        {
            string dateStr = date.ToString("yyyy-MM-dd");
            LoadEpg(dateStr);
        }

        public void ReloadEpg(DateTime date)
        {
            epgLoader.Loaded = false;
            LoadEpg(date);
        }

        public void ReloadEpg(string date)
        {
            epgLoader.Loaded = false;
            LoadEpg(date);
        }

        public void LoadEpg(string date)
        {
            if (epgLoader.Loaded || epgLoader.Busy)
            {
                return;
            }

            string param = "&date=" + date;

            snow1.IsBusy = true;

            epgLoader.Load("getepg", param, true, Constants.EPG_MODULE, string.Format(Constants.EPG_FILE_NAME_FORMTAT, date),
                list =>
                {
                    if (list != null)
                    {
                        epgList.Clear();
                        //List<DateTime> validHours = new List<DateTime>();

                        foreach (var item in list)
                        {
                            epgList.Add(item);
                            //validHours.Add(item.Start);
                        }

                        //SetQuickSelectorValidItems(validHours);

                        epgListBox.ScrollIntoView(list.FirstOrDefault());
                        snow1.IsBusy = false;
                    }
                });
        }

        private void EpgItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HostingPage != null)
            {
                EPG epg = sender.GetDataContext<EPG>();
                string[] paramArray = new string[] { NaviParam.LIVE_ID, epg.ID, NaviParam.LIVE_IMAGE, epg.Image, NaviParam.LIVE_TITLE, epg.Description };
                string naviStr = string.Format("/Pages/LivePage.xaml?{0}={1}&{2}={3}&{4}={5}", paramArray);
                HostingPage.NavigationService.Navigate(new Uri(naviStr, UriKind.Relative));
            }
        }

        #endregion

        #region Layout Control

        VirtualizingStackPanel ItemsPanel = null;
        private void VirtualizingStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            ItemsPanel = sender as VirtualizingStackPanel;
            ItemsPanel.Margin = ItemsPanelMargin;
        }

        #endregion

        #region Subscribe

        private void Subscribe_Tap(object sender, GestureEventArgs e)
        {
            EPG epg = sender.GetDataContext<EPG>();
            if (epg.Subscribed)
            {
                ReminderHelper.RemoveReminder(epg.ID);
                SubscriptionDataContext.Current.RemoveSubscription(epg.ID);
                epg.Subscribed = false;
                App.ShowToastMessage("成功取消预约。");
            }
            else
            {
                try
                {
                    var successful = ReminderHelper.AddReminder(epg.ID, epg.Category, epg.Match, epg.StartTime, "/Pages/HomePage.xaml?");
                    if (successful)
                    {
                        SubscriptionDataContext.Current.AddSubscription(epg);
                        epg.Subscribed = true;
                        App.ShowToastMessage("成功添加预约。");
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion


    }
}
