using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections;
using WorldCup2014WP.Animations;

namespace WorldCup2014WP.Controls
{
    public partial class SlideShow : UserControl
    {
        List<FrameworkElement> items = new List<FrameworkElement>();
        int index = 0;
        int totalItemsCount = 0;
        FrameworkElement activeItem = null;
        public SlideShow()
        {
            InitializeComponent();
        }

        private void item_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement item = sender as FrameworkElement;
            items.Add(item);
            if (items.Count == 1)
            {
                item.Opacity = 1;
                activeItem = item;
            }
            else
            {
                item.Opacity = 0;
            }

            if (items.Count == totalItemsCount && items.Count > 1)
            {
                SlideNext();
            }
        }

        private void SlideNext()
        {
            FadeAnimation.Fade(activeItem, 1d, 0d, Constants.SLIDE_SHOW_TRANSITION_DURATION, null);
            index++;
            if (index == totalItemsCount)
            {
                index = 0;
            }

            dotsListBox.SelectedIndex = index;
            activeItem = items[index];
            FadeAnimation.Fade(activeItem, 0d, 1d, Constants.SLIDE_SHOW_TRANSITION_DURATION,
                fe =>
                {
                    FadeAnimation.Fade(activeItem, 1d, 1d, Constants.SLIDE_SHOW_INTERVAL,
                        fe2 =>
                        {
                            SlideNext();
                        });
                });
        }

        public void SetItemsSource(Array data)
        {
            items.Clear();
            totalItemsCount = data.Length;
            index = 0;
            dotsListBox.Items.Clear();
            for (int i = 0; i < totalItemsCount; i++)
            {
                dotsListBox.Items.Add(i);
            }
            listbox.ItemsSource = data;
        }

    }
}
