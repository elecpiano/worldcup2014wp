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
            PopulateDates();
        }

        #region Calendar

        ListDataLoader<CalendarItem> calendarLoader = new ListDataLoader<CalendarItem>();

        private void LoadSchedule()
        {
            if (calendarLoader.Busy)
            {
                return;
            }

            //snow1.IsBusy = true;

            calendarLoader.Load("getcalendar", string.Empty, true, Constants.CALENDAR_MODULE, Constants.CALENDAR_FILE_NAME,
                result =>
                {
                    //snow1.IsBusy = false;
                });
        }

        #endregion

        private void PopulateDates()
        {
            DateTime date = new DateTime(2014, 6, 1);
            GameCalendarItemControl dayControl = null;

            for (int i = 0; i < 7; i++)
            {
                junePanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                julyPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int w = 0; w < 5; w++)//5 weeks
            {
                junePanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                for (int d = 0; d < 7; d++)
                {
                    dayControl = new GameCalendarItemControl();
                    dayControl.DataContext = date;
                    date = date.AddDays(1);

                    junePanel.Children.Add(dayControl);
                    dayControl.SetValue(Grid.RowProperty, w);
                    dayControl.SetValue(Grid.ColumnProperty, d);
                }
            }


        }
    }
}
