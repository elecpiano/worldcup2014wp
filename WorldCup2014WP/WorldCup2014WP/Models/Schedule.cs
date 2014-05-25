using System;
using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class Schedule
    {
        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "country1")]
        public string Country1 { get; set; }

        [DataMember(Name = "country2")]
        public string Country2 { get; set; }

        [DataMember(Name = "img1")]
        public string Image1 { get; set; }

        [DataMember(Name = "img2")]
        public string Image2 { get; set; }

        [DataMember(Name = "score")]
        public string Score { get; set; }

        [DataMember(Name = "field")]
        public string Field { get; set; }

        [DataMember(Name = "liveid")]
        public string LiveID { get; set; }

        [IgnoreDataMember]
        public string ScoreForView
        {
            get
            {
                return Score == "-1:-1" ? "vs" : Score;
            }
        }
    }

}
