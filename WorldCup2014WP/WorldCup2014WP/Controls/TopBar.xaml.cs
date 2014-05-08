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
    public partial class TopBar : UserControl
    {
        #region Property

        public string SecondaryHeader
        {
            get { return (string)GetValue(SecondaryHeaderProperty); }
            set
            {
                SetValue(SecondaryHeaderProperty, value);
            }
        }

        public static readonly DependencyProperty SecondaryHeaderProperty =
            DependencyProperty.Register("SecondaryHeader", typeof(string), typeof(TopBar), new PropertyMetadata(null, SecondaryHeaderChanged));

        private static void SecondaryHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TopBar topbar = d as TopBar;
            string newString = e.NewValue.ToString();
            topbar.secondaryHeaderTextBlock.Text = newString;
            if (string.IsNullOrEmpty(newString))
            {
                topbar.secondaryHeaderPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                topbar.secondaryHeaderPanel.Visibility = Visibility.Visible;
            }
        }

        #endregion

        public TopBar()
        {
            InitializeComponent();
        }


    }
}
