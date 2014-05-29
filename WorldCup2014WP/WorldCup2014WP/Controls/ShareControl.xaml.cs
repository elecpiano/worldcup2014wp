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

namespace WorldCup2014WP.Controls
{
    public partial class ShareControl : UserControl
    {
        public ShareControl()
        {
            InitializeComponent();
        }

        #region SNS Share

        private void SnsShareWechat()
        {
            WechatHelper.Current.Send("世界杯测试title", "世界杯测试desc", "Assets/ApplicationIcon.png", "http://google.com");
        }

        //private void SnsShareWeibo()
        //{
        //    PhotoChooserTask task = new PhotoChooserTask();
        //    task.Show();
        //    task.Completed += task_Completed;
        //}

        //void task_Completed(object sender, PhotoResult e)
        //{
        //    (sender as PhotoChooserTask).Completed -= task_Completed;
        //    if (e.ChosenPhoto != null)
        //    {
        //        WeiboAssociation.Share(e.ChosenPhoto, "CCTV 世界杯 微博测试");
        //    }
        //}

        #endregion

        private void weibo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void wechat_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
    }
}
