using System.Windows;
using System.Windows.Controls;

namespace WorldCup2014WP.Controls
{
    public class ContentButton : ContentControl
    {
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
