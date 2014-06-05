using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;
using System;

namespace WorldCup2014WP.Pages
{
    public partial class StatisticsPage : PhoneApplicationPage
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //Uri uri = new Uri("http://hosted.stats.com/matchcast/soccer/wap/coolcast_temp.html");
            //browser.Navigate(uri);

            LoadHTML();
        }

        #region Data

        DataLoader<StatisticsAddress> dataLoader = new DataLoader<StatisticsAddress>();

        private void LoadHTML()
        {
            if (dataLoader.Loaded || dataLoader.Busy)
            {
                return;
            }

            dataLoader.Load("getconfig", string.Empty, false, string.Empty, string.Empty,
                result =>
                {
                    Uri uri = new Uri(result.URL);
                    browser.Navigate(uri);
                });
        }

        #endregion

    }
}