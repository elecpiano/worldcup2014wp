using System;
using System.Windows.Controls;
using WorldCup2014WP.Animations;

namespace WorldCup2014WP.Controls
{
    public partial class Snow : UserControl
    {
        //private FadeAnimation _FadeAnimation = new FadeAnimation();

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

        public Snow()
        {
            InitializeComponent();
            Storyboard1.Begin();
        }

        private void StartSnowing()
        {
            FadeAnimation.Fade(this, 0, 1d, TimeSpan.FromMilliseconds(300), null);
        }

        private void StopSnowing()
        {
            FadeAnimation.Fade(this, 1d, 0, TimeSpan.FromMilliseconds(300), null);
        }

    }
}
