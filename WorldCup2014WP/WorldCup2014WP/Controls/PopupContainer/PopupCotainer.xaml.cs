using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;

namespace WorldCup2014WP.Controls
{
    public partial class PopupCotainer : UserControl
    {
        #region Property

        private PhoneApplicationPage _BasePage;
        private FrameworkElement _PopupContent { get; set; }
        Popup _Popup;
        CubicEase _EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };

        public bool IsShown
        {
            get
            {
                return _Popup.IsOpen;
            }
        }

        private PopupMode _Mode = PopupMode.Top;
        public PopupMode Mode
        {
            get
            {
                return _Mode;
            }
            set
            {
                _Mode = value;
            }
        }

        #endregion

        #region Constructor

        public PopupCotainer(PhoneApplicationPage basePage)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PopupCotainer_Loaded);
            _BasePage = basePage;
        }

        public void UpdateBasePage(PhoneApplicationPage basePage)
        {
            if (_BasePage != basePage)
            {
                _BasePage = basePage;
            }
        }

        #endregion

        #region Event

        public EventHandler OnClosing = null;
        public EventHandler OnClosed = null;

        #endregion

        #region Private Method

        private void PopupCotainer_Loaded(object sender, RoutedEventArgs e)
        {
            var story = PrepareShowStory();
            story.Completed += story_Completed;
            story.Begin();
        }

        void story_Completed(object sender, EventArgs e)
        {
            if (OnCompletelyShown != null)
            {
                OnCompletelyShown();
                OnCompletelyShown = null;
            }
        }

        private void BasePage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close(null);
            e.Cancel = true;
        }

        private Storyboard PrepareShowStory()
        {
            Storyboard story = new Storyboard();
            DoubleAnimation animation;

            if (this.Mode == PopupMode.Top)
            {
                topContentArea.Children.Add(_PopupContent);
                UpdateLayout();

                animation = new DoubleAnimation();
                animation.From = 0 - topPopupArea.ActualHeight;
                animation.To = SystemTray.IsVisible ? 32 : 0;
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                animation.EasingFunction = _EasingFunction;
                Storyboard.SetTarget(animation, topPopupTransform);
                Storyboard.SetTargetProperty(animation, new PropertyPath("TranslateY"));
                story.Children.Add(animation);
            }
            else if (this.Mode == PopupMode.Bottom)
            {
                bottomContentArea.Children.Add(_PopupContent);
                UpdateLayout();

                animation = new DoubleAnimation();
                animation.From = bottomPopupArea.ActualHeight - 0;
                animation.To = 0;
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                animation.EasingFunction = _EasingFunction;
                Storyboard.SetTarget(animation, bottomPopupTransform);
                Storyboard.SetTargetProperty(animation, new PropertyPath("TranslateY"));
                story.Children.Add(animation);
            }

            animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            Storyboard.SetTarget(animation, mask);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.Opacity)"));
            story.Children.Add(animation);

            return story;
        }

        private Storyboard PrepareCloseStory()
        {
            Storyboard story = new Storyboard();
            DoubleAnimation animation;

            if (this.Mode == PopupMode.Top)
            {
                story.Completed += new EventHandler(StoryReverse_Completed);
                animation = new DoubleAnimation();
                animation.From = topPopupTransform.TranslateY;
                animation.To = 0 - topPopupArea.ActualHeight;
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                animation.EasingFunction = _EasingFunction;
                Storyboard.SetTarget(animation, topPopupTransform);
                Storyboard.SetTargetProperty(animation, new PropertyPath("TranslateY"));
                story.Children.Add(animation);
            }
            else if (this.Mode == PopupMode.Bottom)
            {
                story.Completed += new EventHandler(StoryReverse_Completed);
                animation = new DoubleAnimation();
                animation.From = bottomPopupTransform.TranslateY;
                animation.To = bottomPopupArea.ActualHeight;
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
                animation.EasingFunction = _EasingFunction;
                Storyboard.SetTarget(animation, bottomPopupTransform);
                Storyboard.SetTargetProperty(animation, new PropertyPath("TranslateY"));
                story.Children.Add(animation);
            }

            animation = new DoubleAnimation();
            animation.From = mask.Opacity;
            animation.To = 0;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            Storyboard.SetTarget(animation, mask);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.Opacity)"));
            story.Children.Add(animation);

            return story;
        }

        private void StoryReverse_Completed(object sender, EventArgs e)
        {
            ClosePopup();
            if (OnClosed != null)
            {
                OnClosed(this, EventArgs.Empty);
            }
        }

        private void ClosePopup()
        {
            topContentArea.Children.Clear();
            bottomContentArea.Children.Clear();

            _PopupContent = null;
            //_BasePage = null;

            Popup parent = this.Parent as Popup;
            if (parent != null)
            {
                parent.Child = null;
                parent.IsOpen = false;
            }
        }

        private void mask_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close(null);
        }

        #endregion

        #region Public Method

        Action OnCompletelyShown;
        public void Show(FrameworkElement popupContent, PopupMode mode, Action onCompletelyShown = null)
        {
            OnCompletelyShown = onCompletelyShown;
            _BasePage.BackKeyPress += BasePage_BackKeyPress;
            _PopupContent = popupContent;
            Mode = mode;
            _Popup = new Popup();
            _Popup.Child = this;
            _Popup.IsOpen = true;
        }

        public void Close(EventHandler onClosed)
        {
            //OnClosedAction = onClosed;
            OnClosed += onClosed;
            _BasePage.BackKeyPress -= BasePage_BackKeyPress;
            var story = PrepareCloseStory();
            story.Begin();
            if (OnClosing != null)
            {
                OnClosing(this, EventArgs.Empty);
            }
        }

        #endregion
    }

    public static class PopupManager
    {
        public static void CloseMeAsPopup(this FrameworkElement popupContent, EventHandler onClosed = null)
        {
            var contentArea = popupContent.Parent as Grid;
            if (contentArea != null)
            {
                var popupArea = contentArea.Parent as Grid;
                if (popupArea != null)
                {
                    var layoutRoot = popupArea.Parent as Grid;
                    if (layoutRoot != null)
                    {
                        var container = layoutRoot.Parent as PopupCotainer;
                        if (container != null)
                        {
                            container.Close(onClosed);
                        }
                    }
                }
            }
        }
    }
}
