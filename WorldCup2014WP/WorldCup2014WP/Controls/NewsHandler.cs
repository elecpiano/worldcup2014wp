﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using WorldCup2014WP.Models;
using WorldCup2014WP.Pages;

namespace WorldCup2014WP.Controls
{
    public class NewsHandler
    {
        public static void OnNewsTap(Page hostingPage, News news)
        {
            string naviString = string.Empty;
            switch (news.Type)
            {
                case "0":
                    VideoPage.PlayVideo(hostingPage, news.ID, null);
                    break;
                case "1":
                    naviString = string.Format("/Pages/NewsDetailPage.xaml?{0}={1}&{2}={3}&{4}={5}", new object[] { NaviParam.NEWS_ID, news.ID, NaviParam.NEWS_TITLE, news.Title, NaviParam.NEWS_IMAGE, news.Image });
                    hostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                    break;
                case "2":
                    naviString = string.Format("/Pages/AlbumPage.xaml?{0}={1}", NaviParam.ALBUM_ID, news.ID);
                    hostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                    break;
                case "31":
                    naviString = string.Format("/Pages/SubjectPage.xaml?{0}={1}", NaviParam.SUBJECT_ID, news.ID);
                    hostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                    break;
                case "15":
                    naviString = string.Format("/Pages/SubjectPage.xaml?{0}={1}", NaviParam.SUBJECT_ID, news.ID);
                    hostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                    break;
                default:
                    break;
            }
        }

        public static void OnNewsTap(Page hostingPage, string id, string title, string image, string secondaryHeader = "")
        {
            string naviString = string.Format("/Pages/NewsDetailPage.xaml?{0}={1}&{2}={3}&{4}={5}", new object[] { NaviParam.NEWS_ID, id, NaviParam.NEWS_TITLE, title, NaviParam.NEWS_IMAGE, image });
            if (!string.IsNullOrEmpty(secondaryHeader))
            {
                naviString = string.Format(naviString + "&{0}={1}", NaviParam.NEWS_SECOND_TITLE, secondaryHeader);
            }
            hostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
        }
    }
}
