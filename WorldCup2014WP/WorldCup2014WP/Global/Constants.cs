using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup2014WP
{
    public class Constants
    {
        public const string DOMAIN = "http://115.28.21.97";// "http://api.gootrip.com"; //"http://115.28.253.73";

        //splash
        public const string SPLASH_MODULE = "splash";
        public const string SPLASH_FILE_NAME = "splash.txt";

        //banner
        public const string BANNER_MODULE = "banner";
        public const string BANNER_FILE_NAME = "banner.txt";

        //epg
        public const string EPG_MODULE = "epg";
        public const string EPG_FILE_NAME_FORMTAT = "epg_{0}.txt";

        //news 
        public const string NEWS_MODULE = "news";
        public const string NEWS_LIST_FILE_NAME_FORMAT = "news_list_page_{0}.txt";
        public const string NEWS_DETAIL_FILE_NAME_FORMAT = "news_{0}.txt";

        //medal tally
        public const string MEDAL_TALLY_MODULE = "medaltally";
        public const string MEDAL_TALLY_FILE_NAME = "medaltally.txt";

        //stadium
        public const string STADIUM_MODULE = "stadium";
        public const string STADIUM_LIST_FILE_NAME = "stadium_list.txt";
        public const string STADIUM_DETAIL_FILE_NAME_FORMAT = "stadium_{0}.txt";

        //category 
        public const string CATEGORY_MODULE = "category";
        public const string CATEGORY_LIST_FILE_NAME = "category_list.txt";

        //schedule
        public const string SCHEDULE_MODULE = "schedule";
        public const string SCHEDULE_FILE_NAME_FORMAT = "schedule_list_{0}.txt";
        public const string RESULT_FILE_NAME_FORMAT = "result_list_{0}.txt";
        public const string CATEGORY_ABC_FILE_NAME_FORMAT = "category_abc_{0}.txt";

        //subscription
        public const string KEY_SUBSCRIPTION_LIST = "subscription_list";

        //album
        public const string ALBUM_MODULE = "album";
        public const string ALBUM_FILE_NAME_FORMAT = "album_{0}.txt";

        //live page
        public const string LIVE_MODULE = "live";
        public const string LIVE_FILE_NAME_FORMAT = "live_{0}.txt";

        //news 
        public const string CALENDAR_MODULE = "calendar";
        public const string CALENDAR_FILE_NAME = "calendar.txt";

        //image helper
        public const string KEY_IMAGE_CACHE = "image_cache";

        //animation duration
        public static TimeSpan NAVIGATION_DURATION = TimeSpan.FromMilliseconds(200);

        //vod
        public const string VOD_MODULE = "vod";
        public const string VOD_FILE_NAME_FORMAT = "vod_{0}.txt";
    }

    public class NaviParam
    {
        //public const string CATEGORY_ID = "category_id";
        //public const string CATEGORY_TITLE = "category_title";

        public const string SCHEDULE_ID = "schedule_id";
        public const string ALBUM_ID = "album_id";
        public const string LIVE_ID = "live_id";
        public const string LIVE_IMAGE = "live_image";
        public const string LIVE_TITLE = "live_title";
        public const string CALENDAR_DATE = "calendar_date";

        public const string STADIUM_ID = "stadium_id";
        public const string STADIUM_NAME = "stadium_name";
        public const string NEWS_ID = "news_id";
        //public const string VOD_ID = "vod_id";

    }
}
