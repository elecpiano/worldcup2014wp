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
        GenericDataLoader<GuessFromUser> guessMakingLoader = new GenericDataLoader<GuessFromUser>();

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
                    if (result==null)
                    {
                        return;
                    }
                    int index = 1;
                    foreach (var item in result)
                    {
                        GuessControl control = new GuessControl();
                        control.DataContext = item;
                        control.MakeGuess += MakeGuess;

                        PivotItem pivotItem = new PivotItem() { Header = "竞猜" + index.ToString() };
                        pivotItem.Content = control;
                        pivot.Items.Add(pivotItem);
                        index++;
                    }
                });
        }

        void MakeGuess(object sender, GuessMakingArgument arg)
        {
            if (!App.IsLoggedIn)
            {
                MessageBox.Show("请登录");
                return;
            }

            if (arg.Gold > App.User.GoldInt)
            {
                MessageBox.Show("积分不够");
                return;
            }

            string confirmMessage = string.Format("您确定要猜 {0} 吗？(消耗积分{1})", arg.Option.OptionName, arg.Gold);
            MessageBoxResult dialogResult = MessageBox.Show(confirmMessage, "竞猜", MessageBoxButton.OKCancel);
            if (dialogResult == MessageBoxResult.Cancel)
            {
                return;
            }

            if (guessMakingLoader.Busy)
            {
                return;
            }

            string param = string.Format("&id={0}&qid={1}&gold={2}&sid={3}",
                new object[] { arg.GameID, arg.Option.ID, arg.Gold, App.User.SessionID });

            //load
            guessMakingLoader.Load("playgame", param, false, string.Empty, string.Empty,
                result =>
                {
                    if (result == null)
                    {
                        return;
                    }
                    
                    if (result.Code == "200")
                    {
                        MessageBox.Show(result.data.Message);
                        UpdateUserInfo();
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
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

                    //save user
                    var user = App.User;
                    user.Gold = result.Data.Gold.ToString();
                    App.UpdateUser(user);
                });
        }

        #endregion

    }
}