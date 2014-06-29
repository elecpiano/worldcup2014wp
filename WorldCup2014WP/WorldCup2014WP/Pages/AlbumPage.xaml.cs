using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;
using Microsoft.Xna.Framework.Media;
using System.IO;
using Microsoft.Phone.Shell;
using WorldCup2014WP.Controls;

namespace WorldCup2014WP.Pages
{
    public partial class AlbumPage : PhoneApplicationPage
    {
        #region Property

        private int imageCount = 0;
        private int currentIndex = 0;
        private bool bottomPanelShown = true;
        FadingImage imageCenter, imageLeft, imageRight;
        private string albumID = string.Empty;

        private const string FILE_NAME_PREFIX = "SochiWinterOlympics_";

        #endregion

        #region Lifecycle

        public AlbumPage()
        {
            InitializeComponent();
            BuildApplicationBar();
            imageCenter = image1;
            imageLeft = image3;
            imageRight = image2;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            albumID = NavigationContext.QueryString[NaviParam.ALBUM_ID];
            FitOrientation();
            LoadAlbumData(albumID);
        }

        #endregion

        #region Data

        DataLoader<WorldCup2014WP.Models.Album> albumloader = new DataLoader<WorldCup2014WP.Models.Album>();
        ObservableCollection<AlbumItem> albumItems = new ObservableCollection<AlbumItem>();
        ImageHelper imageHelper = new ImageHelper();

        private void LoadAlbumData(string id)
        {
            if (albumloader.Loaded || albumloader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            albumloader.Load("getalbum", "&id=" + id, true, Constants.ALBUM_MODULE, string.Format(Constants.ALBUM_FILE_NAME_FORMAT, id),
                album =>
                {
                    if (album==null)
                    {
                        return;
                    }
                    if (album.Items ==null)
                    {
                        return;
                    }
                    albumItems.Clear();
                    foreach (var item in album.Items)
                    {
                        albumItems.Add(item);
                    }
                    imageCount = albumItems.Count;
                    currentIndex = 0;
                    UpdateCurrentIndex();
                    snow1.IsBusy = false;
                });
        }

        #endregion

        #region Selection

        int previousPanoramaIndex = 0;

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool swipeToLeft = false;
            if (panorama.SelectedIndex == 0)
            {
                imageCenter = image1;
                imageLeft = image3;
                imageRight = image2;

                if (previousPanoramaIndex == 1)
                {
                    swipeToLeft = false;
                }
                else if (previousPanoramaIndex == 2)
                {
                    swipeToLeft = true;
                }
            }
            else if (panorama.SelectedIndex == 1)
            {
                imageCenter = image2;
                imageLeft = image1;
                imageRight = image3;

                if (previousPanoramaIndex == 2)
                {
                    swipeToLeft = false;
                }
                else if (previousPanoramaIndex == 0)
                {
                    swipeToLeft = true;
                }
            }
            else if (panorama.SelectedIndex == 2)
            {
                imageCenter = image3;
                imageLeft = image2;
                imageRight = image1;

                if (previousPanoramaIndex == 0)
                {
                    swipeToLeft = false;
                }
                else if (previousPanoramaIndex == 1)
                {
                    swipeToLeft = true;
                }
            }

            previousPanoramaIndex = panorama.SelectedIndex;

            if (swipeToLeft)
            {
                currentIndex++;
                if (currentIndex >= imageCount)
                {
                    currentIndex = 0;
                }
            }
            else
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = imageCount - 1;
                }
            }

            UpdateCurrentIndex();
        }

        private void UpdateCurrentIndex()
        {
            indexTextBlock.Text = (currentIndex + 1).ToString() + "/" + imageCount.ToString();
            descriptionTextBlock.Text = albumItems[currentIndex].Title;

            int indexForLeft = currentIndex == 0 ? (imageCount - 1) : (currentIndex - 1);
            int indexForRight = currentIndex == (imageCount - 1) ? 0 : (currentIndex + 1);

            imageCenter.Source = new BitmapImage(new Uri(albumItems[currentIndex].Image, UriKind.RelativeOrAbsolute));
            imageLeft.Source = new BitmapImage(new Uri(albumItems[indexForLeft].Image, UriKind.RelativeOrAbsolute));
            imageRight.Source = new BitmapImage(new Uri(albumItems[indexForRight].Image, UriKind.RelativeOrAbsolute));
            //imageCenter.Source = await ImageCacheDataContext.Current.GetImage(albumItems[currentIndex].Image, true);
            //imageLeft.Source = await ImageCacheDataContext.Current.GetImage(albumItems[indexForLeft].Image, true);
            //imageRight.Source = await ImageCacheDataContext.Current.GetImage(albumItems[indexForRight].Image, true);
        }

        #endregion

        #region Tap

        private void Panorama_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            bottomPanelShown = !bottomPanelShown;
            if (bottomPanelShown)
            {
                VisualStateManager.GoToState(this, "BottomPanelShown", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "BottomPanelHidden", true);
            }
        }

        #endregion

        #region Orientatin

        Thickness landscape_margin = new Thickness(58, 0, 0, 0);
        Thickness portrait_margin = new Thickness(61, 0, 0, 0);
        double landscape_scale = 1.16d;
        double portrait_scale = 1.29d;
        double landscape_grid_width = 690;
        double landscape_grid_height = 415;
        double portrait_grid_width = 372;
        double portrait_grid_height = 620;

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            FitOrientation();
        }

        private void FitOrientation()
        {
            if (this.Orientation == PageOrientation.PortraitUp || this.Orientation == PageOrientation.PortraitDown)
            {
                panorama.Margin = portrait_margin;
                panoramaTransform.ScaleX = portrait_scale;
                panoramaTransform.ScaleY = portrait_scale;
                grid1.Width = grid2.Width = grid3.Width = portrait_grid_width;
                grid1.Height = grid2.Height = grid3.Height = portrait_grid_height;

                this.LayoutRoot.Margin = new Thickness(0, 0, 0, 0);
            }
            else if (this.Orientation == PageOrientation.LandscapeLeft || this.Orientation == PageOrientation.LandscapeRight)
            {
                panorama.Margin = landscape_margin;
                panoramaTransform.ScaleX = landscape_scale;
                panoramaTransform.ScaleY = landscape_scale;
                grid1.Width = grid2.Width = grid3.Width = landscape_grid_width;
                grid1.Height = grid2.Height = grid3.Height = landscape_grid_height;

                this.LayoutRoot.Margin = new Thickness(0, 0, 48, 0);
            }
        }

        #endregion

        #region Save

        private void SaveImage()
        {
            BitmapImage bi = imageCenter.Source as BitmapImage;
            if (bi != null)
            {
                using (Stream stream = bi.ToStream())
                {
                    MediaLibrary library = new MediaLibrary();
                    string fileName = FILE_NAME_PREFIX + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".jpg";
                    library.SavePicture(fileName, stream);
                }
            }
        }

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarSave;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            //ApplicationBar.Opacity = 0.9;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // refresh
            appBarSave = new ApplicationBarIconButton(new Uri("/Assets/AppBar/save.png", UriKind.Relative));
            appBarSave.Text = "保存";
            appBarSave.Click += appBarSave_Click;
            ApplicationBar.Buttons.Add(appBarSave);
        }

        void appBarSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                SaveImage();
                toast.ShowMessage("保存成功！");
            }
            catch (Exception ex)
            {
                toast.ShowMessage("保存失败！");
            }
        }

        #endregion

    }
}