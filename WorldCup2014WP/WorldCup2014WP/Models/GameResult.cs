using System;
using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class GameResult
    {
        [DataMember(Name="ranklist")]
        public RankItem[] RankList { get; set; }
    }

    [DataContract]
    public class RankItem
    {
        [DataMember(Name = "rank")]
        public string Rank { get; set; }

        [DataMember(Name = "player")]
        public string Player { get; set; }

        [DataMember(Name = "score")]
        public string Score { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }
    }
}
