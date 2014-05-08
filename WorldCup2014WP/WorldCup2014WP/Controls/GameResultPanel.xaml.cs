using System;
using System.Windows;
using System.Windows.Controls;

namespace WorldCup2014WP.Controls
{
    public partial class GameResultPanel : UserControl
    {
        public GameResultPanel()
        {
            InitializeComponent();
        }

        public void Show(bool hasData)
        {
            this.noData.Visibility = hasData ? Visibility.Collapsed : Visibility.Visible;
            VisualStateManager.GoToState(this, "Shown", true);
        }

        StackPanel parent = null;
        public void Hide(StackPanel container)
        {
            parent = container;
            VisualStateManager.GoToState(this, "Hidden", true);
        }

        private void Hide_Completed(object sender, EventArgs e)
        {
            if (parent != null)
            {
                if (parent.Children.Contains(this))
                {
                    parent.Children.Remove(this);
                }
            }
        }
    }
}
