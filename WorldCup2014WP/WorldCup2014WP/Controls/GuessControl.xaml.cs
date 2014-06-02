using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WorldCup2014WP.Controls
{
    public partial class GuessControl : UserControl
    {
        public GuessControl()
        {
            InitializeComponent();
        }

        private void Guess_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider!=null)
            {
                bet.Text = (((int)slider.Value)*500).ToString();
            }
        }
    }
}
