using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;

namespace WorldCup2014WP.Pages
{
    public partial class StadiumDetailPage : PhoneApplicationPage
    {
        private string stadiumID = string.Empty;
        private string stadiumName = string.Empty;

        public StadiumDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            stadiumID = NavigationContext.QueryString[NaviParam.STADIUM_ID];
            stadiumName = NavigationContext.QueryString[NaviParam.STADIUM_NAME];

            //this.topBar.SecondaryHeader = stadiumName;

            LoadHTML();
        }

        #region HTML

        DataLoader<HTML> htmlLoader = new DataLoader<HTML>();

        private void LoadHTML()
        {
            if (htmlLoader.Loaded || htmlLoader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            htmlLoader.Load("getstadiumdetail", "&id=" + stadiumID, true, Constants.STADIUM_MODULE, string.Format(Constants.STADIUM_DETAIL_FILE_NAME_FORMAT, stadiumID),
                html =>
                {
                    string title = @"<h2 align=""center"">" + stadiumName + "</h2>";
                    string htmlContent = html.Content.Insert(html.Content.IndexOf("</style>") + 8, title);
                    htmlContent = htmlContent.Replace("max-width: 100%;", "width: 100%;");
                    browser.NavigateToString(htmlContent);
                    //browser.NavigateToString(html.Content);
                    snow1.IsBusy = false;
                });
        }

        #endregion

    }
}