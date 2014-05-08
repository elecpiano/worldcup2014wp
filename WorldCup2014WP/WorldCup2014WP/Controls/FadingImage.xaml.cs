using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using WorldCup2014WP.Animations;
using System.Windows.Media.Imaging;

namespace WorldCup2014WP.Controls
{
    public partial class FadingImage : UserControl
    {
        #region Property

        //duration
        private static TimeSpan FadingDuration = TimeSpan.FromMilliseconds(500);

        //source
        private ImageSource _Source = null;
        public ImageSource Source
        {
            get 
            {
                return (ImageSource)GetValue(SourceProperty);
                //return this.image.Source;
            }
            set
            {
                if (_Source != value)
                {
                    BitmapImage newValue = value as BitmapImage;
                    BitmapImage oldValue = _Source as BitmapImage;
                    if (newValue != null && oldValue != null && newValue.UriSource.OriginalString == oldValue.UriSource.OriginalString)
                    {
                        return;
                    }
                    _Source = value;
                    SetValue(SourceProperty, value);
                }
            }
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(FadingImage), new PropertyMetadata(null, OnSourcePropertyChanged));
        private static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FadingImage control = d as FadingImage;
            BitmapImage newValue = e.NewValue as BitmapImage;
            control.image.Source = newValue;
        }

        //stretch
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(FadingImage), new PropertyMetadata(Stretch.UniformToFill, OnStretchPropertyChanged));

        private static void OnStretchPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FadingImage control = d as FadingImage;
            Stretch newValue = (Stretch)e.NewValue;
            control.image.Stretch = newValue;
        }

        #endregion

        public FadingImage()
        {
            InitializeComponent();
            this.image.ImageOpened += image_ImageOpened;
        }

        void image_ImageOpened(object sender, RoutedEventArgs e)
        {
            FadeAnimation.Fade(this.image, 0d, 1d, FadingDuration, fe => UnRegisterEvent());
        }

        private void UnRegisterEvent()
        {
            this.image.ImageOpened -= image_ImageOpened;
        }

    }
}
