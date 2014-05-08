using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using WorldCup2014WP.Animations;
using System.Windows.Threading;

namespace WorldCup2014WP.Controls
{
    public partial class BigSnow : UserControl
    {
        private static Random random = new Random();
        private DispatcherTimer timer = new DispatcherTimer();
        private static int SNOW_CANDIDATE_COUNT = 8;

        private bool isBusy = false;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                if (isBusy)
                {
                    StartSnowing();
                }
                else
                {
                    StopSnowing();
                }
            }
        }

        public BigSnow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(random.Next(timerInterval_min, timerInterval_max)); ;
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            GenerateSnow();
            GenerateSnow();
            GenerateSnow();

            timer.Interval = TimeSpan.FromMilliseconds(random.Next(timerInterval_min, timerInterval_max)); ;
            timer.Start();
        }

        private void StartSnowing()
        {
            timer.Start();
            FadeAnimation.Fade(this, 0, 1d, TimeSpan.FromMilliseconds(showDuration), null);
        }

        private void StopSnowing()
        {
            timer.Stop();
            FadeAnimation.Fade(this, 1d, 0, TimeSpan.FromMilliseconds(hideDuration), null);
        }

        int showDuration = 300;
        int hideDuration = 1000;

        int timerInterval_min = 100;
        int timerInterval_max = 200;
        int duration_min = 2000;
        int duration_max = 3000;
        int radius_min = 8;
        int radius_max = 32;
        int rotation_min = 90;
        int rotation_max = 360;

        private void GenerateSnow()
        {
            string snowImage = "/Assets/Images/Snow/Snow" + random.Next(0, SNOW_CANDIDATE_COUNT).ToString() + ".png";
            Image image = new Image() { Source = new BitmapImage(new Uri(snowImage, UriKind.Relative)), Stretch = Stretch.Uniform, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            int radius = random.Next(radius_min, radius_max);
            image.Width = image.Height = radius;
            double opacity_max = ((double)radius) / (double)radius_max;
            this.snowPanel.Children.Add(image);

            int x = random.Next(0, (int)this.ActualWidth);
            int durationMilliseconds = random.Next(duration_min, duration_max);
            TimeSpan moveDuration = TimeSpan.FromMilliseconds(durationMilliseconds);
            MoveAnimation.MoveFromTo(image, x, 0 - image.Height, x, this.ActualHeight, moveDuration,
                fe =>
                {
                    if (this.snowPanel.Children.Contains(fe))
                    {
                        this.snowPanel.Children.Remove(fe);
                    }
                });

            double rotationAmount = random.Next(rotation_min, rotation_max);//90d * (double)durationMilliseconds/(double)duration_min;
            TimeSpan rotationDuration = TimeSpan.FromMilliseconds(0.9d * (double)durationMilliseconds);
            RotateAnimation.RotateBy(image, rotationAmount, rotationDuration, null);

            FadeAnimation.Fade(image, opacity_max, 0, TimeSpan.FromMilliseconds((double)durationMilliseconds * 0.9d), null);
            //FadeAnimation.Fade(image, 0, opacity_max, TimeSpan.FromMilliseconds((double)durationMilliseconds * 0.4d),
            //    fe =>
            //    {
            //        FadeAnimation.Fade(fe, opacity_max, 0, TimeSpan.FromMilliseconds((double)durationMilliseconds * 0.4d), null);
            //    });
        }
    }
}
