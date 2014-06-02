using System;
using System.Runtime.Serialization;
using System.Windows;
using WorldCup2014WP.Infrastructures;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class EPG : BindableBase
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "start")]
        public string Start { get; set; }

        [DataMember(Name = "duration")]
        public string Duration { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "type")]
        public int Type { get; set; }

        [DataMember(Name = "cate")]
        public string Category { get; set; }

        [DataMember(Name = "match")]
        public string Match { get; set; }

        [DataMember(Name = "matchId")]
        public string MatchID { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "seepoint")]
        public string SeePoint { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "gold")]
        public int Gold { get; set; }

        [DataMember(Name = "showLivePage")]
        public int ShowLivePage { get; set; }

        [DataMember(Name = "guessType")]
        public int GuessType { get; set; }

        [DataMember(Name = "ad")]
        public int AD { get; set; }

        [DataMember(Name = "sns")]
        public int SNS { get; set; }

        [DataMember(Name = "final")]
        public int Final { get; set; }

        [IgnoreDataMember]
        public Visibility CCTV1
        {
            get
            {
                return this.Channel.ToLower().Contains("cctv1") ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        [IgnoreDataMember]
        public Visibility CCTV5
        {
            get
            {
                return this.Channel.ToLower().Contains("cctv5") ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        [IgnoreDataMember]
        public Visibility CCTV5Plus
        {
            get
            {
                return this.Channel.ToLower().Contains("cctv5+") ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        [IgnoreDataMember]
        public Visibility IsLive
        {
            get
            {
                return this.Type == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        [IgnoreDataMember]
        public Visibility IsVideo
        {
            get
            {
                return this.Type == 1 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private bool subscribed = false;
        [IgnoreDataMember]
        public bool Subscribed
        {
            get { return subscribed; }
            set { SetProperty(ref this.subscribed, value); }
        }

        [IgnoreDataMember]
        public DateTime StartTime
        {
            get
            {
                string[] dtArr = Start.Split(' ');
                string[] dateArr = dtArr[0].Split('-');
                int year = int.Parse(dateArr[0]);
                int month = int.Parse(dateArr[1]);
                int day = int.Parse(dateArr[2]);

                string[] timeArr = dtArr[1].Split(':');
                int hour = int.Parse(timeArr[0]);
                int extraDayCount = hour / 24;
                hour = hour % 24;
                int minute = int.Parse(timeArr[1]);
                int second = int.Parse(timeArr[2]);

                DateTime goodResult = new DateTime(year, month, day, hour, minute, second);
                if (extraDayCount>0)
                {
                    goodResult = goodResult.AddDays(extraDayCount);
                }
                return goodResult;
            }
        }

        private bool expired = false;
        [IgnoreDataMember]
        public bool Expired
        {
            get { return expired; }
            set { SetProperty(ref this.expired, value); }
        }
    }
}
