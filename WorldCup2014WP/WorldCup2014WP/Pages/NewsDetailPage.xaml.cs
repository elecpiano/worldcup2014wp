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
    public partial class NewsDetailPage : PhoneApplicationPage
    {
        private string newsID = string.Empty;
        private string newsTitle = string.Empty;

        #region Lifecycle

        public NewsDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            newsID = NavigationContext.QueryString[NaviParam.NEWS_ID];
            newsTitle = NavigationContext.QueryString[NaviParam.NEWS_TITLE];

            LoadHTML();
        }

        #endregion

        #region HTML

        DataLoader<HTML> htmlLoader = new DataLoader<HTML>();

        private void LoadHTML()
        {
            if (htmlLoader.Loaded || htmlLoader.Busy)
            {
                return;
            }

            snow2.IsBusy = true;

            htmlLoader.Load("getdetail", "&id=" + newsID, true, Constants.NEWS_MODULE, string.Format(Constants.NEWS_DETAIL_FILE_NAME_FORMAT, newsID),
                html =>
                {
                    string title = "<h2>"+newsTitle+"</h2>";
                    string htmlContent = html.Content.Insert(html.Content.IndexOf("</style>") + 8, title);
                    htmlContent = htmlContent.Replace("max-width: 100%;", "width: 100%;");
                    browser.NavigateToString(htmlContent);
                    snow2.IsBusy = false;
                });
        }

        #endregion

    }
}