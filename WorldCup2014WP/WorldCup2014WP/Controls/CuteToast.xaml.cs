using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WorldCup2014WP.Controls
{
    public partial class CuteToast : UserControl
    {
        #region Singleton

        private static CuteToast instance = new CuteToast();

        public static CuteToast Current
        {
            get { return instance; }
        }

        #endregion

        public CuteToast()
        {
            InitializeComponent();
        }

        public void ShowMessage(string message)
        {
            this.messageTextBlock.Text = message;
            //VisualStateManager.GoToState(this, "Shown", true);
            this.StoryShow.Begin();
            //WaitAndHide();
        }

        //DispatcherTimer timer;

        //private void WaitAndHide()
        //{
        //    if (timer == null)
        //    {
        //        timer = new DispatcherTimer();
        //        timer.Interval = TimeSpan.FromSeconds(3);
        //        timer.Tick += timer_Tick;
        //    }
        //    timer.Start();
        //}

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    timer.Stop();
        //    Hide();
        //}

        private void Hide()
        {
            //VisualStateManager.GoToState(this, "Hidden", true);
            this.StoryShow.Stop();
            this.StoryHide.Begin();
        }

        private void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //if (timer != null)
            //{
            //    timer.Stop();
            //}
            Hide();
        }

        private void StoryShow_Completed(object sender, EventArgs e)
        {
            Hide();
        }

    }
}
