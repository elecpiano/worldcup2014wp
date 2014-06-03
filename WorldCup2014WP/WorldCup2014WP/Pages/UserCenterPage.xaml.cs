using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Models;
using System.Windows.Threading;

namespace WorldCup2014WP.Pages
{
    public partial class UserCenterPage : PhoneApplicationPage
    {
        #region Property

        App App { get { return App.Current as App; } }

        #endregion

        #region Lifecycle

        public UserCenterPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SetUserInfo();
            UpdateUserInfo();
        }

        #endregion

        #region Button Click

        private void userImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (App.IsLoggedIn)
            {
                ShowPopup("VSLogout");
            }
            else
            {
                ShowPopup("VSRegOrLogin");
            }
        }

        private void Register_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            bool validInput = true;
            if (string.IsNullOrEmpty(registerID.Text.Trim()))
            {
                registerID.ErrorMessage = "请输入用户名";
                validInput = false;
            }
            if (string.IsNullOrEmpty(registerPassword.Password))
            {
                registerPassword.ErrorMessage = "请输入密码";
                validInput = false;
            }
            if (string.IsNullOrEmpty(activationCode.Text.Trim()))
            {
                activationCode.ErrorMessage = "请输入激活码";
                validInput = false;
            }

            if (validInput)
            {
                Register(registerID.Text, activationCode.Text, registerPassword.Password);
            }
        }

        private void Login_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            bool validInput = true;
            if (string.IsNullOrEmpty(loginID.Text.Trim()))
            {
                loginID.ErrorMessage = "请输入用户名";
                validInput = false;
            }
            if (string.IsNullOrEmpty(loginPassword.Password))
            {
                loginPassword.ErrorMessage = "请输入密码";
                validInput = false;
            }

            if (validInput)
            {
                Login(loginID.Text, loginPassword.Password);
            }
        }

        private void ToRegister_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowPopup("VSReg");
        }

        private void ToLogin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowPopup("VSLogin");
        }

        private void Logout_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Logout();
        }

        private void ChangePassword_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void ToChangeNickname_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowPopup("VSChangeNickname");
        }

        private void ChangeNickname_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ChangeNickname();
        }

        private void sendActivationCode_Click(object sender, RoutedEventArgs e)
        {
            bool validInput = true;
            if (string.IsNullOrEmpty(registerID.Text.Trim()))
            {
                registerID.ErrorMessage = "请输入手机号";
                validInput = false;
            }

            if (validInput)
            {
                GetRegCode(registerID.Text);
            }
        }

        #endregion

        #region Popup Management

        bool popupShown = false;

        private void ShowPopup(string visualState)
        {
            popupShown = true;
            VisualStateManager.GoToState(this, visualState, true);
            popupMask.Opacity = 1;
            popupMask.IsHitTestVisible = true;
        }

        private void ClosePopup()
        {
            VisualStateManager.GoToState(this, "Normal", true);
            popupShown = false;
            popupMask.Opacity = 0;
            popupMask.IsHitTestVisible = false;
        }

        private void popupMask_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ClosePopup();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (popupShown)
            {
                e.Cancel = true;
                ClosePopup();
                return;
            }

            base.OnBackKeyPress(e);
        }

        private void Normal_Completed(object sender, EventArgs e)
        {
            registerID.Text = string.Empty;
            registerPassword.Password = string.Empty;
            activationCode.Text = string.Empty;
            loginID.Text = string.Empty;
            loginPassword.Password = string.Empty;
        }

        #endregion

        #region Auth

        GenericDataLoader<UserLoginResult> loginLoader = new GenericDataLoader<UserLoginResult>();
        GenericDataLoader<UserRegCodeResult> regCodeLoader = new GenericDataLoader<UserRegCodeResult>();
        GenericDataLoader<UserLoginResult> registerLoader = new GenericDataLoader<UserLoginResult>();
        GenericDataLoader<UserInfo> userInfoLoader = new GenericDataLoader<UserInfo>();

        private void Login(string userId, string password)
        {
            if (loginLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            string param = "&name=" + userId + "&pwd=" + password;

            //load
            loginLoader.UserLoad("login", param, true, Constants.USER_MODULE, Constants.LOGIN_FILE_NAME,
                result =>
                {
                    if (result.Code == "200")
                    {
                        App.User = result;
                        gold.Text = App.User.Gold;
                        SetUserInfo();
                        ClosePopup();
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }

                    //not busy
                    //snowNews.IsBusy = false;
                });
        }

        private void Logout()
        {
            App.User = null;
            SetUserInfo();
            ClosePopup();
        }

        private void SetUserInfo()
        {
            if (App.IsLoggedIn)
            {
                userImage.DataContext = App.User.Image;
                userName.Text = App.User.NickName;
                goldPanel.Visibility = Visibility.Visible;
            }
            else
            {
                goldPanel.Visibility = Visibility.Collapsed;
                userImage.DataContext = "/Assets/Images/UserDefault.png";
                userName.Text = "未登录";
            }
        }

        private void GetRegCode(string userId)
        {
            if (regCodeLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            string param = "&name=" + userId;

            //load
            regCodeLoader.UserLoad("regcode", param, false, string.Empty, string.Empty,
                result =>
                {
                    if (result.Code == "200")
                    {
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                        if (timer != null)
                        {
                            timer.Stop();
                        }
                        sendActivationCode.Content = "发送激活码";
                        sendActivationCode.IsEnabled = true;
                    }
                    //not busy
                    //snowNews.IsBusy = false;
                });
            CountDownForRegCode();
        }

        private void Register(string userId, string code, string password)
        {
            if (registerLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            string param = "&name=" + userId + "&code=" + code + "&pwd=" + password;

            //load
            registerLoader.UserLoad("signup", param, true, Constants.USER_MODULE, Constants.LOGIN_FILE_NAME,
                result =>
                {
                    if (result.Code == "200")
                    {
                        App.User = result;
                        SetUserInfo();
                        ClosePopup();
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }

                    //not busy
                    //snowNews.IsBusy = false;
                });
        }

        private void UpdateUserInfo()
        {
            if (!App.IsLoggedIn)
            {
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
                    if (result.Code != "200")
                    {
                        MessageBox.Show(result.Message);
                    }
                    else
                    {
                        if (result.Data != null)
                        {
                            gold.Text = result.Data.Gold.ToString();
                        }
                    }
                });
        }

        private void ChangeNickname()
        {
            if (loginLoader.Busy)
            {
                return;
            }

            string newName = newNickname.Text.Trim();

            if (string.IsNullOrEmpty(newName))
            {
                ClosePopup();
                return;
            }

            string param = "&name=" + newName + "&sid=" + App.User.SessionID;

            //load
            loginLoader.UserLoad("changenickname", param, false, string.Empty, string.Empty,
                result =>
                {
                    if (result.Code == "200")
                    {
                        userName.Text = newName;

                        //save user
                        var user = App.User;
                        user.NickName = newName;
                        App.UpdateUser(user);
                        ClosePopup();
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }

                    //not busy
                    //snowNews.IsBusy = false;
                });
        }

        #endregion

        #region Count Down Timer

        DispatcherTimer timer;
        DateTime countDownStartTime = DateTime.MinValue;
        private void CountDownForRegCode()
        {
            sendActivationCode.IsEnabled = false;
            sendActivationCode.Content = "60";
            countDownStartTime = DateTime.Now;

            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += timer_Tick;
            }
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            var duration = DateTime.Now - countDownStartTime;
            if (duration.TotalSeconds >= 60)
            {
                timer.Stop();
                sendActivationCode.Content = "发送激活码";
                sendActivationCode.IsEnabled = true;
                return;
            }
            sendActivationCode.Content = (60 - (int)duration.TotalSeconds).ToString();
        }

        #endregion

        #region Tiles

        private void mySubscription_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string naviStr = "/Pages/MySubscriptionPage.xaml";
            NavigationService.Navigate(new Uri(naviStr, UriKind.Relative));
        }

        private void guessHistory_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (App.IsLoggedIn)
            {
                string naviStr = "/Pages/GuessResultPage.xaml";
                NavigationService.Navigate(new Uri(naviStr, UriKind.Relative));
            }
            else
            {
                MessageBox.Show("请登录");
            }
        }

        #endregion

    }
}