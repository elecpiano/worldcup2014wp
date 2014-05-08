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

namespace WorldCup2014WP.Pages
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateLocalFolderSize();
        }

        #region Settings

        private async void UpdateLocalFolderSize()
        {
            var size = await IsolatedStorageHelper.GetUserDataSize();
            localFolderSizeTextBlock.Text = Math.Round(((double)size / 1048576d), 2).ToString() + " MB";
        }

        #endregion

        private async void ClearCacheButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var result = MessageBox.Show("缓存文件有助于您在无网络连接情况下仍然可以查阅曾经访问过的内容。确定要清除缓存吗？", "清除缓存", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                await IsolatedStorageHelper.ClearUserData();
                UpdateLocalFolderSize();
            }
        }
    }
}