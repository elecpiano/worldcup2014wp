using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using WorldCup2014WP.Animations;

namespace WorldCup2014WP.Controls
{
    public partial class GameCalendarItemControl : UserControl
    {
        private static TimeSpan duration = TimeSpan.FromMilliseconds(50);
        public GameCalendarItemControl()
        {
            InitializeComponent();
        }

        private void Border_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            FadeAnimation.Fade(rect, 0d, 1d, duration,
                fe =>
                {
                    FadeAnimation.Fade(fe, 1d, 0d, duration, null);
                });
        }
    }
}
