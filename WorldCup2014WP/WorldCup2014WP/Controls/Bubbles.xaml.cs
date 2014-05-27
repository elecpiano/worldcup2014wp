using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using WorldCup2014WP.Animations;
using System.Windows.Threading;

namespace WorldCup2014WP.Controls
{
    public partial class Bubbles : UserControl
    {
        private static Random random = new Random();
        private DispatcherTimer timer = new DispatcherTimer();
        //private static int SNOW_CANDIDATE_COUNT = 1;

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
                    Start();
                }
                else
                {
                    Stop();
                }
            }
        }

        public Bubbles()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(random.Next(timerInterval_min, timerInterval_max)); ;
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            GenerateBubble();
            //GenerateBubble();

            timer.Interval = TimeSpan.FromMilliseconds(random.Next(timerInterval_min, timerInterval_max)); ;
            timer.Start();
        }

        private void Start()
        {
            timer.Start();
            FadeAnimation.Fade(this, 0, 1d, TimeSpan.FromMilliseconds(showDuration), null);
        }

        private void Stop()
        {
            timer.Stop();
            FadeAnimation.Fade(this, 1d, 0, TimeSpan.FromMilliseconds(hideDuration), null);
        }

        int showDuration = 300;
        int hideDuration = 1000;

        int timerInterval_min = 100;
        int timerInterval_max = 500;
        int duration_min = 5000;
        int duration_max = 7000;
        int radius_min = 8;
        int radius_max = 48;
        //int rotation_min = 90;
        //int rotation_max = 360;

        private void GenerateBubble()
        {
            //string snowImage = "/Assets/Images/Bubble" + random.Next(1, SNOW_CANDIDATE_COUNT + 1).ToString() + ".png";
            string img = "/Assets/Images/Bubble1.png";
            Image image = new Image() { Source = new BitmapImage(new Uri(img, UriKind.Relative)), Stretch = Stretch.Uniform, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            int radius = random.Next(radius_min, radius_max);
            image.Width = image.Height = radius;
            double opacity = random.NextDouble();//((double)radius) / (double)radius_max;
            image.Opacity = opacity;
            this.panel1.Children.Add(image);

            double x = random.NextDouble() * this.ActualWidth - image.Width * 0.5;
            int durationMilliseconds = random.Next(duration_min, duration_max);
            TimeSpan moveDuration = TimeSpan.FromMilliseconds(durationMilliseconds);
            MoveAnimation.MoveFromTo(image, x, this.ActualHeight + image.Height, x, 0 - image.Height, moveDuration,
                fe =>
                {
                    if (this.panel1.Children.Contains(fe))
                    {
                        this.panel1.Children.Remove(fe);
                    }
                });

            //double rotationAmount = random.Next(rotation_min, rotation_max);//90d * (double)durationMilliseconds/(double)duration_min;
            //TimeSpan rotationDuration = TimeSpan.FromMilliseconds(0.9d * (double)durationMilliseconds);
            //RotateAnimation.RotateBy(image, rotationAmount, rotationDuration, null);

            //FadeAnimation.Fade(image, opacity, 0, TimeSpan.FromMilliseconds((double)durationMilliseconds * 0.9d), null);
        }
    }
}
