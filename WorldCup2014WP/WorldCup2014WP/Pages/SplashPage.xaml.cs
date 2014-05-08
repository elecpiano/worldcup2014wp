using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WorldCup2014WP.Pages
{
    public partial class SplashPage : PhoneApplicationPage
    {
        public SplashPage()
        {
            InitializeComponent();
            SetSplashImage();
            StartTimer();
        }

        #region Lifecycle

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            NavigationService.RemoveBackEntry();
        }

        #endregion

        #region Set Splash Image

        private void SetSplashImage()
        {
            SetDefualtSplashImage();
        }

        private void SetDefualtSplashImage()
        {
            this.splashImage.Source = new BitmapImage(new Uri("/Assets/Images/SplashScreenDefault.PNG", UriKind.Relative));
        }

        #endregion

        #region Timer

        DispatcherTimer timer = new DispatcherTimer();

        private void StartTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Tick -= timer_Tick;
            NavigationService.Navigate(new Uri("/Pages/HomePage_Pivot.xaml", UriKind.Relative));
        }

        #endregion
    }
}