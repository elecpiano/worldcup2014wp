using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;
using WorldCup2014WP.Animations;
using WorldCup2014WP.Controls;

namespace WorldCup2014WP.Pages
{
    public partial class CategoryListPage : PhoneApplicationPage
    {
        #region Lifecycle

        public CategoryListPage()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadCategories();
        }

        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    base.OnNavigatingFrom(e);
        //    if (e.Uri.OriginalString != "app://external/")
        //    {
        //        HidePage();
        //    }
        //}

        #endregion

        #region Category List

        ListDataLoader<Category> categoryLoader = new ListDataLoader<Category>();
        List<Category> categories = new List<Category>();

        private void LoadCategories()
        {
            if (categoryLoader.Loaded || categoryLoader.Busy)
            {
                return;
            }

            snow1.IsBusy = true;

            categoryLoader.Load("getcate", string.Empty, true, Constants.CATEGORY_MODULE, Constants.CATEGORY_LIST_FILE_NAME,
                list =>
                {
                    categories = list;
                    PopulateCategoryListBox();
                    //ShowPage();
                    this.scrollViewer.ScrollToVerticalOffset(0);
                    snow1.IsBusy = false;
                });
        }

        private bool Comparison(Category item1, Category item2)
        {
            return item1.ID == item2.ID && item1.Title == item2.Title && item1.Image == item2.Image;
        }

        private void PopulateCategoryListBox()
        {
            int i = 0;
            categoryListGrid.Children.Clear();
            categoryListGrid.RowDefinitions.Clear();
            foreach (var category in categories)
            {
                if ((i % 3) == 0)
                {
                    categoryListGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(160d) });
                }
                CategoryButton button = new CategoryButton() { DataContext = category };
                button.Tap += Category_Tap;
                button.SetValue(Grid.RowProperty, i / 3);
                button.SetValue(Grid.ColumnProperty, i % 3);
                categoryListGrid.Children.Add(button);
                i++;
            }
        }

        private void Category_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Category category = sender.GetDataContext<Category>();
            CategoryPage.SelectedCategory = category;
            string strUri = "/Pages/CategoryPage.xaml";
            NavigationService.Navigate(new Uri(strUri, UriKind.Relative));
        }

        #endregion

        //#region Page Navigation Transition

        //FadeAnimation fadeAnimation = new FadeAnimation();
        //MoveAnimation moveAnimation = new MoveAnimation();

        //private void ShowPage()
        //{
        //    contentPanel.UpdateLayout();
        //    moveAnimation.InstanceMoveFromTo(this.contentPanel, 0, 90, 0, 0, Constants.NAVIGATION_DURATION, null);
        //    fadeAnimation.InstanceFade(this.contentPanel, 0d, 1d, Constants.NAVIGATION_DURATION, null);
        //}

        //private void HidePage()
        //{
        //    moveAnimation.InstanceMoveFromTo(this.contentPanel, 0, 0, 0, 90, Constants.NAVIGATION_DURATION, null);
        //    fadeAnimation.InstanceFade(this.contentPanel, 1d, 0d, Constants.NAVIGATION_DURATION, null);
        //}

        //#endregion

        #region App Bar

        ApplicationBarIconButton appBarRefresh;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            //ApplicationBar.Opacity = 0.9;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // refresh
            appBarRefresh = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefresh.Text = "刷新";
            appBarRefresh.Click += appBarRefresh_Click;
            ApplicationBar.Buttons.Add(appBarRefresh);
        }

        void appBarRefresh_Click(object sender, System.EventArgs e)
        {
            categoryLoader.Loaded = false;
            LoadCategories();
        }

        #endregion

    }
}