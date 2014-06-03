using System;
using System.Runtime.Serialization;
using System.Windows;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class GuessResult
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "time")]
        public string Time { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "win")]
        public int Win { get; set; }

        [IgnoreDataMember]
        public Visibility IsWin
        {
            get
            {
                return this.Win == 1 ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }

    [DataContract]
    public class GameResultList
    {
        [DataMember(Name = "msg")]
        public string Message { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "data")]
        public GameResult[] Data { get; set; }
    }
}
