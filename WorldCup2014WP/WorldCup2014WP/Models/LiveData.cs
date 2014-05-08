using System;
using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class LiveData
    {
        [DataMember(Name = "data")]
        public LiveLineItem[] LineItems { get; set; }

        [DataMember(Name = "score")]
        public LiveScore Score { get; set; }

        [DataMember(Name = "ranklist")]
        public RankItem[] RankList { get; set; }
    }

    [DataContract]
    public class LiveLineItem
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "time")]
        public DateTime Time { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "bgimg")]
        public string BigImage { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "type")]
        public int Type { get; set; }

        [DataMember(Name = "album")]
        public AlbumItem[] Album { get; set; }
    }

    [DataContract]
    public class LiveScore
    {
        [DataMember(Name = "player1")]
        public string Player1 { get; set; }

        [DataMember(Name = "img1")]
        public string Image1 { get; set; }

        [DataMember(Name = "score1")]
        public int Score1 { get; set; }

        [DataMember(Name = "player2")]
        public string Player2 { get; set; }

        [DataMember(Name = "img2")]
        public string Image2 { get; set; }

        [DataMember(Name = "score2")]
        public int Score2 { get; set; }
    }

    //[DataContract]
    //public class LiveRankListItem
    //{
    //    [DataMember(Name = "rank")]
    //    public string Rank { get; set; }

    //    [DataMember(Name = "country")]
    //    public string Country { get; set; }

    //    [DataMember(Name = "img")]
    //    public string Image { get; set; }

    //    [DataMember(Name = "player")]
    //    public string Player { get; set; }

    //    [DataMember(Name = "score")]
    //    public string Score { get; set; }
    //}
}
