using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WorldCup2014WP.Controls
{
    public partial class CategoryButton : UserControl
    {
        public CategoryButton()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            VisualStateManager.GoToState(this, "Pressed", true);
        }

        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            GoNormal();
        }

        protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            GoNormal();
        }

        private void GoNormal()
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }
    }
}
