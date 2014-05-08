using System;
using System.Windows.Controls;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;
using WorldCup2014WP.Pages;
using Microsoft.Phone.Tasks;

namespace WorldCup2014WP.Controls
{
    public partial class HeaderMoreButton : UserControl
    {
        public HeaderMoreButton()
        {
            InitializeComponent();
            this.Loaded += HeaderMoreButton_Loaded;
        }

        void HeaderMoreButton_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Storyboard1.Begin();
            this.Loaded -= HeaderMoreButton_Loaded;
        }

    }
}
