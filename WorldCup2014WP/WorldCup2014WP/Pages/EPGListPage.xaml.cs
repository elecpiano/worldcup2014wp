using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;
using Microsoft.Phone.Shell;

namespace WorldCup2014WP.Pages
{
    public partial class EPGListPage : PhoneApplicationPage
    {
        #region Property

        private string gameDate = string.Empty;

        #endregion

        #region Lifecycle

        public EPGListPage()
        {
            InitializeComponent();
            InitEpgList();
            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameDate = NavigationContext.QueryString[NaviParam.CALENDAR_DATE];
            this.topBar.SecondaryHeader = gameDate;
            LoadEPGList();
        }

        #endregion

        #region EPG List

        private void InitEpgList()
        {
            epgList.HostingPage = this;
            epgList.QuickSelector = this.quickSelector;
        }

        private void LoadEPGList()
        {
            epgList.LoadEpg(gameDate);
        }

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarRefresh;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            //ApplicationBar.Opacity = 0.9;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // refresh
            appBarRefresh = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefresh.Text = "刷新";
            appBarRefresh.Click += appBarRefresh_Click;
            ApplicationBar.Buttons.Add(appBarRefresh);
        }

        void appBarRefresh_Click(object sender, System.EventArgs e)
        {
            epgList.ReloadEpg(gameDate);
        }

        #endregion

    }
}