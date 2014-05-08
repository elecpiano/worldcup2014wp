using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;

namespace WorldCup2014WP.Controls
{
    public delegate void SelectionChangedEventHandler(object sender, DateTime datetime);

    public partial class QuickSelector : UserControl
    {
        #region Property

        private const string ITEM_STRING_FORMAT = "HH:mm";
        private const double UNIT_HEIGHT = 26d;
        private double offset_y = 0d;
        private List<DateTime> _keys;
        private Dictionary<DateTime, bool> _items;
        private bool eventRaised = false;

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                }
            }
        }

        #endregion

        public QuickSelector()
        {
            InitializeComponent();
            selectionHint.Height = UNIT_HEIGHT;
            //VisualStateManager.GoToState(this, "SelectionPreviewHidden", false);
        }

        public void SetItems(Dictionary<DateTime, bool> items)
        {
            this.itemsControl.ItemsSource = null;
            this.itemsControl.ItemsSource = _items = items;
            _keys = _items.Keys.ToList();
            offset_y = UNIT_HEIGHT * ((items.Count - 1) * 0.5d);
        }

        private void SetSelectionPosition(MouseEventArgs e)
        {
            eventRaised = false;
            VisualStateManager.GoToState(this, "SelectionPreviewShown", true);
            Point p = e.GetPosition(InterActionArea);

            double y = p.Y - p.Y % UNIT_HEIGHT;
            int tempIndex = (int)(y / UNIT_HEIGHT);

            if (tempIndex < 0 || tempIndex >= _items.Count)
            {
                return;
            }

            SelectedIndex = tempIndex;

            y = y - offset_y;

            selectionHintTransform.TranslateY = y;
            //selectionPreviewTransform.TranslateY = y;
            selectionPreviewTransform.TranslateY = p.Y - offset_y - UNIT_HEIGHT * 0.5d;

            selectionPreviewText.Text = _keys[SelectedIndex].ToString(ITEM_STRING_FORMAT);
            selectionPreviewText.Opacity = _items[_keys[SelectedIndex]] ? 1d : 0.3d;
        }

        private void RaiseSelectionChanged()
        {
            if (eventRaised)
            {
                return;
            }
            eventRaised = true;
            VisualStateManager.GoToState(this, "SelectionPreviewHidden", true);

            if (_items[_keys[SelectedIndex]])
            {
                if (this.SelectionChanged != null)
                {
                    SelectionChanged(this, _keys[SelectedIndex]);
                }
            }
        }

        private void Dismiss()
        {
            VisualStateManager.GoToState(this, "SelectionPreviewHidden", false);
        }

        #region Selection Changed Event

        public event SelectionChangedEventHandler SelectionChanged;

        #endregion

        #region Touch Event

        private void InterActionArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetSelectionPosition(e);
        }

        private void InterActionArea_MouseMove(object sender, MouseEventArgs e)
        {
            SetSelectionPosition(e);
        }

        private void InterActionArea_MouseLeave(object sender, MouseEventArgs e)
        {
            //RaiseSelectionChanged();
            Dismiss();
        }

        private void InterActionArea_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RaiseSelectionChanged();
        }

        #endregion


    }
}
