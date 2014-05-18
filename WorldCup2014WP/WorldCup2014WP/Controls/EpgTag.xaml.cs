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
    public partial class EpgTag : UserControl
    {
        public EpgTag()
        {
            InitializeComponent();
            this.Loaded += EpgTag_Loaded;
        }

        void EpgTag_Loaded(object sender, RoutedEventArgs e)
        {
            this.Storyboard1.Begin();
        }
    }
}
