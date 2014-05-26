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
            LoadSchedule();
        }

        #region Calendar

        ListDataLoader<CalendarItem> calendarLoader = new ListDataLoader<CalendarItem>();

        public void LoadSchedule()
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

                    PivotItem pivotItem = new PivotItem() { Header = month.ToString("yyyy年MM月") };
                    pivotItem.Content = newMonthControl;
                    pivot.Items.Add(pivotItem);
                }

                GameCalendarMonthControl monthControl = monthAndControls[month];
                monthControl.AddDay(item);
            }
        }
    }
}
