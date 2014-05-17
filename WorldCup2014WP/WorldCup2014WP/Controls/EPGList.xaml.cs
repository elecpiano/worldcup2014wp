using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;
using System.Windows;

namespace WorldCup2014WP.Controls
{
    public partial class EPGList : UserControl
    {
        #region Property

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
            if (control.ItemsPanel!=null)
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

        #region QuickSelector

        Dictionary<DateTime, bool> hoursOfDay = new Dictionary<DateTime, bool>();

        //private void InitQuickSelector()
        //{
        //    DateTime dt = DateTime.Today;
        //    for (int i = 0; i < 24; i++)
        //    {
        //        hoursOfDay.Add(dt, false);
        //        dt = dt.AddHours(1);
        //    }

        //    //initial values, all are invalid items
        //    quickSelector.SetItems(hoursOfDay);
        //    quickSelector.SelectionChanged += QuickSelector_SelectionChanged;
        //}

        //private void SetQuickSelectorValidItems(IEnumerable<DateTime> validItems)
        //{
        //    var keyList = hoursOfDay.Keys.ToList();
        //    foreach (var key in keyList)
        //    {
        //        if (validItems.Any(x => x.Hour == key.Hour))
        //        {
        //            hoursOfDay[key] = true;
        //        }
        //    }

        //    //update again
        //    quickSelector.SetItems(hoursOfDay);
        //}

        //private void QuickSelector_SelectionChanged(object sender, DateTime selectedDateTime)
        //{
        //    epgListBox.ScrollIntoView(epgList.FirstOrDefault(x => x.Start.Hour == selectedDateTime.Hour));
        //}

        #endregion

        VirtualizingStackPanel ItemsPanel = null;
        private void VirtualizingStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            ItemsPanel = sender as VirtualizingStackPanel;
            ItemsPanel.Margin = ItemsPanelMargin;
        }

    }
}
