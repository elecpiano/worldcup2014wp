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
using System.Windows.Threading;
using WorldCup2014WP.Controls;

namespace WorldCup2014WP.Pages
{
    public partial class GuessPage : PhoneApplicationPage
    {
        #region Property

        App App { get { return App.Current as App; } }

        #endregion

        #region Lifecycle

        public GuessPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateUserInfo();
            LoadGuess();
        }

        #endregion

        #region Guess Data

        ListDataLoader<GuessGame> guessLoader = new ListDataLoader<GuessGame>();

        private void LoadGuess()
        {
            if (guessLoader.Busy)
            {
                return;
            }

            //load
            guessLoader.Load("getgamelist", string.Empty, true, Constants.GUESS_MODULE, Constants.GUESS_FILE_NAME,
                result =>
                {
                    int index = 1;
                    foreach (var item in result)
                    {
                        GuessControl control = new GuessControl();
                        control.DataContext = item;

                        PivotItem pivotItem = new PivotItem() { Header = "竞猜" + index.ToString() };
                        pivotItem.Content = control;
                        pivot.Items.Add(pivotItem);
                    }
                });
        }

        #endregion

        #region User Info

        GenericDataLoader<UserInfo> userInfoLoader = new GenericDataLoader<UserInfo>();

        private void UpdateUserInfo()
        {
            if (!App.IsLoggedIn)
            {
                gold.Text = "（您还没有登录）";
                return;
            }

            if (userInfoLoader.Busy)
            {
                return;
            }

            string param = "&sid=" + App.User.SessionID;

            //load
            userInfoLoader.UserLoad("getuserinfo", param, true, Constants.USER_MODULE, Constants.USER_INFO_FILE_NAME,
                result =>
                {
                    gold.Text = result.Data.Gold.ToString();
                });
        }

        #endregion

    }
}