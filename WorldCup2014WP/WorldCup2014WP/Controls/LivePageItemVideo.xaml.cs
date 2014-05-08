using System.Windows.Controls;
using WorldCup2014WP.Models;
using WorldCup2014WP.Pages;
using WorldCup2014WP.Utility;

namespace WorldCup2014WP.Controls
{
    public partial class LivePageItemVideo : UserControl
    {
        public Page HostingPage { get; set; }

        public LivePageItemVideo()
        {
            InitializeComponent();
        }

        private void Control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (HostingPage != null)
            {
                LiveLineItem item = sender.GetDataContext<LiveLineItem>();
                if (item != null)
                {
                    VideoPage.PlayVideo(HostingPage, item.ID, this.snow1);
                }
            }
        }
    }
}
