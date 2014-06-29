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
using System.IO;

namespace WorldCup2014WP.Pages
{
    public partial class NewsDetailPage : PhoneApplicationPage
    {
        #region Property

        App App { get { return App.Current as App; } }

        private string newsID = string.Empty;
        private string newsTitle = string.Empty;
        private string newsImage = string.Empty;
        private string secondaryHeader = string.Empty;

        private const int WECHAT_IMAGE_SIZE_MAX = 1024 * 32;

        private bool TryGetShareResultOnNavigatedTo = false;

        #endregion

        #region Lifecycle

        public NewsDetailPage()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            newsID = NavigationContext.QueryString[NaviParam.NEWS_ID];
            newsTitle = NavigationContext.QueryString[NaviParam.NEWS_TITLE];
            newsImage = NavigationContext.QueryString[NaviParam.NEWS_IMAGE];

            if (NavigationContext.QueryString.ContainsKey(NaviParam.NEWS_SECOND_TITLE))
            {
                secondaryHeader = NavigationContext.QueryString[NaviParam.NEWS_SECOND_TITLE];
                if (!string.IsNullOrEmpty(secondaryHeader))
                {
                    topBar.SecondaryHeader = secondaryHeader;
                }
            }

            LoadHTML();

            if (TryGetShareResultOnNavigatedTo)
            {
                GetShareResult();
                TryGetShareResultOnNavigatedTo = false;
            }
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
                    if (html==null)
                    {
                        return;
                    }
                    if (html.Content==null)
                    {
                        return;
                    }
                    string title = @"<h2 align=""center"">" + newsTitle + "</h2>";
                    string htmlContent = html.Content.Insert(html.Content.IndexOf("</style>") + 8, title);
                    htmlContent = htmlContent.Replace("max-width: 100%;", "width: 100%;");
                    browser.NavigateToString(htmlContent);
                    snow2.IsBusy = false;
                });
        }

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarShare;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            //ApplicationBar.Opacity = 0.9;
            //ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // share
            appBarShare = new ApplicationBarIconButton(new Uri("/Assets/AppBar/share.png", UriKind.Relative));
            appBarShare.Text = "分享";
            appBarShare.Click += appBarShare_Click;
            ApplicationBar.Buttons.Add(appBarShare);
        }

        void appBarShare_Click(object sender, System.EventArgs e)
        {
            if (App.IsLoggedIn)
            {
                ShowPopup("VSSNSSelector");
            }
            else
            {
                MessageBox.Show("请登录");
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

        #endregion

        #region SNS Share

        private async void Weibo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ClosePopup();
            Stream imageStream = await ImageHelper.GetImageStream(newsImage);
            string shareText = newsTitle + " " + GetNewsURL();
            WeiboAssociation.Share(imageStream, shareText);
            TryGetShareResultOnNavigatedTo = true;
        }

        private async void WeChat_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ClosePopup();
            string shareURL = GetNewsURL();
            byte[] imageData = await ImageHelper.GetImageData(newsImage);
            if (imageData.Length > WECHAT_IMAGE_SIZE_MAX)
            {
                WechatHelper.Current.Send(newsTitle, string.Empty, "Assets/Images/SnsShareDefaultImage.jpg", shareURL);
            }
            else
            {
                WechatHelper.Current.Send(newsTitle, string.Empty, imageData, shareURL);
            }
            TryGetShareResultOnNavigatedTo = true;
        }

        private string GetNewsURL()
        {
            return Constants.DOMAIN + @"/share/" + newsID + ".html";
        }

        GenericDataLoader<ShareResult> shareResultLoader = new GenericDataLoader<ShareResult>();

        private void GetShareResult()
        {
            if (shareResultLoader.Busy)
            {
                return;
            }

            string param = "&sid=" + App.User.SessionID + "&resid=" + newsID;
            shareResultLoader.UserLoad("share", param, false, string.Empty, string.Empty,
                result =>
                {
                    MessageBox.Show(result.Message);
                });
        }

        #endregion


    }
}