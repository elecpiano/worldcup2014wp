using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup2014WP
{
    public class Constants
    {
        public const string DOMAIN = "http://api2.gootrip.com";//"http://api1.gootrip.com";// "http://115.28.21.97";// "http://api.gootrip.com"; //"http://115.28.253.73";
        public const string API_POSTFIX = "&token=wp&sign=wp&t=999";

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

        //magma 
        public const string MAGMA_MODULE = "magma";
        public const string MAGMA_LIST_FILE_NAME = "magma_list.txt";
        public const string MAGMA_DETAIL_FILE_NAME_FORMAT = "magma_{0}.txt";

        //author list 
        public const string AUTHOR_MODULE = "author";
        public const string AUTHOR_FILE_NAME = "author.txt";

        //diary list 
        public const string DIARY_MODULE = "diary";
        public const string DIARY_LIST_FILE_NAME_FORMAT = "diary_list_{0}.txt";

        //recommendation 
        public const string RECOMMENDATION_MODULE = "recommendation";
        public const string RECOMMENDATION_FILE_NAME = "recommendation.txt";
        public const string RECOMMENDATION_NEWS_DETAIL_FILE_NAME_FORMAT = "recommendation_news_{0}.txt";

        //animation duration
        public static TimeSpan SLIDE_SHOW_TRANSITION_DURATION = TimeSpan.FromMilliseconds(700);
        public static TimeSpan SLIDE_SHOW_INTERVAL = TimeSpan.FromMilliseconds(3000);

        //subject 
        public const string SUBJECT_MODULE = "subject";
        public const string SUBJECT_FILE_NAME_FORMAT = "subject_{0}.txt";

        //user center 
        public const string USER_MODULE = "user";
        public const string LOGIN_FILE_NAME = "login.txt";
        public const string USER_INFO_FILE_NAME = "login.txt";

        //game data
        public const string GAME_DATA_MODULE = "gamedata";
        public const string SCORE_FILE_NAME = "score.txt";
        public const string GOAL_FILE_NAME = "goal.txt";
        public const string SCHEDULE_FILE_NAME = "schedule.txt";

        //calendar 
        public const string CALENDAR_MODULE = "calendar";
        public const string CALENDAR_FILE_NAME = "calendar.txt";

        //stadium
        public const string STADIUM_MODULE = "stadium";
        public const string STADIUM_LIST_FILE_NAME = "stadium_list.txt";
        public const string STADIUM_DETAIL_FILE_NAME_FORMAT = "stadium_{0}.txt";

        //team 
        public const string TEAM_MODULE = "team";
        public const string TEAM_FILE_NAME = "team.txt";

        //guess 
        public const string GUESS_MODULE = "guess";
        public const string GUESS_FILE_NAME = "guess.txt";
        public const string GUESS_RESULT_FILE_NAME = "guess_result.txt";

        //////////////////////////////////////// TO-DO : remove all below

       



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

        

        //image helper
        public const string KEY_IMAGE_CACHE = "image_cache";

        

        //vod
        public const string VOD_MODULE = "vod";
        public const string VOD_FILE_NAME_FORMAT = "vod_{0}.txt";
    }

    public class NaviParam
    {
        public const string AUTHOR_ID = "author_id";
        public const string AUTHOR_NAME = "author_name";
        public const string AUTHOR_FACE = "author_face";

        public const string NEWS_ID = "news_id";
        public const string NEWS_TITLE = "news_title";
        public const string NEWS_SECOND_TITLE = "news_second_title";
        public const string NEWS_IMAGE = "news_image";

        public const string SUBJECT_ID = "subject_id";

        public const string STADIUM_ID = "stadium_id";
        public const string STADIUM_NAME = "stadium_name";

       

        /// /////////////////////////////////// TO-DO : remove all below
        public const string SCHEDULE_ID = "schedule_id";
        public const string ALBUM_ID = "album_id";
        public const string LIVE_ID = "live_id";
        public const string LIVE_IMAGE = "live_image";
        public const string LIVE_TITLE = "live_title";
        public const string CALENDAR_DATE = "calendar_date";


        
        //public const string VOD_ID = "vod_id";

    }
}
