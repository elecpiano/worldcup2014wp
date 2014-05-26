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
    public partial class GameCalendarMonthControl : UserControl
    {
        public GameCalendarMonthControl()
        {
            InitializeComponent();
        }

        List<DateTime> weeks = new List<DateTime>();
        const int dow_sunday = (int)DayOfWeek.Sunday;
        const int dow_monday = (int)DayOfWeek.Monday;

        public void AddDay(CalendarItem day)
        {
            DateTime dt = day.Date;
            int dow = (int)dt.DayOfWeek;
            DateTime monday = dow == dow_sunday ? dt.AddDays(-6) : dt.AddDays(dow_monday - dow);

            if (!weeks.Contains(monday))
            {
                weeks.Add(monday);
                monthPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(72, GridUnitType.Pixel) });
            }

            int row = weeks.IndexOf(monday) + 1;
            int column = dow == dow_sunday ? 6 : dow - 1;

            GameCalendarItemControl dayControl = new GameCalendarItemControl();
            dayControl.DataContext = day;
            monthPanel.Children.Add(dayControl);
            dayControl.SetValue(Grid.RowProperty, row);
            dayControl.SetValue(Grid.ColumnProperty, column);
        }

    }
}
