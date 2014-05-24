using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WorldCup2014WP.Controls
{
    public sealed partial class EnhancedPasswordBox : UserControl
    {
        #region Properties

        #region Password

        public string Password
        {
            get
            {
                return this.mainTextBox.Password;
            }
            set
            {
                if (value != null)
                {
                    this.mainTextBox.Password = value;
                    OnTextChanged();
                }                
            }
        }

        public int MaxLength
        {
            get
            {
                return this.mainTextBox.MaxLength;
            }
            set
            {
                this.mainTextBox.MaxLength = value;
            }
        }

        #endregion

        #region InputStyle

        public Style InputStyle
        {
            get { return (Style)GetValue(InputStyleProperty); }
            set { SetValue(InputStyleProperty, value); }
        }

        public static readonly DependencyProperty InputStyleProperty =
            DependencyProperty.Register("InputStyle", typeof(Style), typeof(EnhancedPasswordBox), new PropertyMetadata(null, InputStylePropertyChanged));

        private static void InputStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var etb = d as EnhancedPasswordBox;
            if (etb != null)
            {
                Style style = (Style)e.NewValue;
                etb.mainTextBox.Style = style;
            }
        }

        #endregion

        #region WatermarkStyle

        public Style WatermarkStyle
        {
            get { return (Style)GetValue(WatermarkStyleProperty); }
            set { SetValue(WatermarkStyleProperty, value); }
        }

        public static readonly DependencyProperty WatermarkStyleProperty =
            DependencyProperty.Register("WatermarkStyle", typeof(Style), typeof(EnhancedPasswordBox), new PropertyMetadata(null, WatermarkStylePropertyChanged));

        private static void WatermarkStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var etb = d as EnhancedPasswordBox;
            if (etb != null)
            {
                Style style = (Style)e.NewValue;
                etb.watermarkTextBlock.Style = style;
            }
        }

        #endregion

        #region MessageStyle

        public Style MessageStyle
        {
            get { return (Style)GetValue(MessageStyleProperty); }
            set { SetValue(MessageStyleProperty, value); }
        }

        public static readonly DependencyProperty MessageStyleProperty =
            DependencyProperty.Register("MessageStyle", typeof(Style), typeof(EnhancedPasswordBox), new PropertyMetadata(null, MessageStylePropertyChanged));

        private static void MessageStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var etb = d as EnhancedPasswordBox;
            if (etb != null)
            {
                Style style = (Style)e.NewValue;
                etb.messageTextBlock.Style = style;
            }
        }

        #endregion

        public TextBlock WatermarkTextBlock
        {
            get
            {
                return this.watermarkTextBlock;
            }
        }

        #endregion

        #region Properties

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set
            {
                SetValue(WatermarkProperty, value);
                this.watermarkTextBlock.Text = value;
            }
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(EnhancedPasswordBox), new PropertyMetadata(null));

        private bool ErrorMessageIsShowing = false;
        private bool HintSelected = false;

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set
            {
                SetValue(ErrorMessageProperty, value);
                this.messageTextBlock.Text = value;
                if (string.IsNullOrEmpty(value))
                {
                    this.watermarkTextBlock.Visibility = Visibility.Visible;
                    this.messageTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.watermarkTextBlock.Visibility = Visibility.Collapsed;
                    this.messageTextBlock.Visibility = Visibility.Visible;
                    if (!string.IsNullOrEmpty(this.mainTextBox.Password))
                    {
                        ErrorMessageIsShowing = true;
                        this.mainTextBox.Password = string.Empty;
                    }
                }
            }
        }

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(EnhancedPasswordBox), new PropertyMetadata(null));

        private List<string> _emailPostfixes = new List<string>();

        private List<string> _emailList = new List<string>();

        #endregion

        #region Constructor

        public EnhancedPasswordBox()
        {
            this.InitializeComponent();
            this.SetValue(Canvas.ZIndexProperty, 999);
        }

        #endregion

        #region Typing

        private void mainTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            OnTextChanged();
            if (PasswordChanged != null)
            {
                PasswordChanged(this, e);
            }
        }

        private void OnTextChanged()
        {
            if (ErrorMessageIsShowing)
            {
                ErrorMessageIsShowing = false;
                return;
            }

            if (HintSelected)
            {
                HintSelected = false;
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.watermarkTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                this.watermarkTextBlock.Visibility = Visibility.Collapsed;
            }

            this.messageTextBlock.Visibility = Visibility.Collapsed;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //this.mainTextBox.SelectionStart = this.mainTextBox.Text.Length;
            //this.mainTextBox.SelectionLength = 0;
            this.messageTextBlock.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.mainTextBox.Password) && this.messageTextBlock.Visibility == Visibility.Collapsed)
            {
                this.watermarkTextBlock.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region Event

        public new event KeyEventHandler KeyDown;
        private void mainTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyDown != null)
            {
                KeyDown(this, e);
            }
        }

        public event RoutedEventHandler PasswordChanged;

        #endregion

        private void RootGrid_Tap(object sender, GestureEventArgs e)
        {
            mainTextBox.Focus();
        }


    }
}
