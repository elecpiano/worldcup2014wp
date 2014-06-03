using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Xna.Framework.Media;
using System.Windows.Controls;
using WorldCup2014WP.Controls;

namespace WorldCup2014WP.Pages
{
    public partial class GuessResultPage : PhoneApplicationPage
    {
        #region Property

        App App { get { return App.Current as App; } }

        #endregion

        #region Lifecycle

        public GuessResultPage()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadData();
        }

        #endregion

        #region Data

        GenericDataLoader<GuessResultList> dataLoader = new GenericDataLoader<GuessResultList>();

        private void LoadData()
        {
            if (dataLoader.Busy)
            {
                return;
            }

            string param = "&sid=" + App.User.SessionID;
            dataLoader.UserLoad("getgameresult", param, true, Constants.GUESS_MODULE, Constants.GUESS_RESULT_FILE_NAME,
                result =>
                {
                    listBox.ItemsSource = result.Data;
                    scrollViewer.ScrollToVerticalOffset(0);
                });
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
            dataLoader.Loaded = false;
            LoadData();
        }

        #endregion

    }
}