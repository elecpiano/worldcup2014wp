using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;
using System.Windows.Input;

namespace WorldCup2014WP.Controls
{
    public partial class GameCalendarControl : UserControl
    {
        public GameCalendarControl()
        {
            InitializeComponent();
            this.Loaded += GameCalendarControl_Loaded;
        }

        void GameCalendarControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCalendar();
        }

        #region Calendar

        ListDataLoader<CalendarItem> calendarLoader = new ListDataLoader<CalendarItem>();

        public void LoadCalendar()
        {
            if (calendarLoader.Loaded || calendarLoader.Busy)
            {
                return;
            }

            //snow1.IsBusy = true;

            calendarLoader.Load("getcalendar", string.Empty, true, Constants.CALENDAR_MODULE, Constants.CALENDAR_FILE_NAME,
                result =>
                {
                    PopulateCalendar(result);
                    //snow1.IsBusy = false;
                });
        }

        #endregion

        List<DateTime> months = new List<DateTime>();
        Dictionary<DateTime, GameCalendarMonthControl> monthAndControls = new Dictionary<DateTime, GameCalendarMonthControl>();
        private void PopulateCalendar(List<CalendarItem> items)
        {
            months.Clear();
            monthAndControls.Clear();

            foreach (var item in items)
            {
                DateTime month = new DateTime(item.Date.Year, item.Date.Month, 1);
                if (!months.Contains(month))
                {
                    months.Add(month);
                    GameCalendarMonthControl newMonthControl = new GameCalendarMonthControl();
                    monthAndControls.Add(month, newMonthControl);
                    newMonthControl.DayTap += monthControl_DayTap;

                    PivotItem pivotItem = new PivotItem() { Header = month.ToString("yyyy年MM月") };
                    pivotItem.Content = newMonthControl;
                    pivot.Items.Add(pivotItem);
                }

                //GameCalendarMonthControl monthControl = monthAndControls[month];
                //monthControl.AddDay(item);
            }

            foreach (var month in months)
            {
                PopulateMonth(month, monthAndControls[month], items);
            }
        }

        private void PopulateMonth(DateTime month, GameCalendarMonthControl monthControl, List<CalendarItem> items)
        {
            DateTime nextMonth = month.AddMonths(1);
            DateTime dt = month;
            while (dt < nextMonth)
            {
                CalendarItem item = new CalendarItem() { Date = dt, Football = "0" };
                var sameDay = items.FirstOrDefault(x => x.Date.Year == dt.Year && x.Date.Month == dt.Month && x.Date.Day == dt.Day);
                if (sameDay != null)
                {
                    item.Football = sameDay.Football;
                }
                monthControl.AddDay(item);
                dt = dt.AddDays(1);
            }
        }

        public event EventHandler<GestureEventArgs> DayTap;

        void monthControl_DayTap(object sender, GestureEventArgs e)
        {
            if (DayTap!=null)
            {
                DayTap(sender, e);
            }
        }

    }
}
