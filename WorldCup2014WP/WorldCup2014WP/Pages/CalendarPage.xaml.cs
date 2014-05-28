using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;
using System.Linq;
using System.Windows.Input;

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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        #endregion

        #region Item Tap

        private void calendar_DayTap(object sender, GestureEventArgs e)
        {
            CalendarItem item = sender.GetDataContext<CalendarItem>();
            string naviString = string.Format("/Pages/EPGListPage.xaml?{0}={1}",
                NaviParam.CALENDAR_DATE, item.Date.ToString("yyyy-MM-dd"));
            NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
        }

        #endregion

    }
}