using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class MedalScore
    {
        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "rank")]
        public int Rank { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "gold")]
        public int Gold { get; set; }

        [DataMember(Name = "silver")]
        public int Silver { get; set; }

        [DataMember(Name = "bronze")]
        public int Bronze { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }

    [DataContract]
    public class MedalScoreList
    {
        [DataMember]
        public MedalScore[] data { get; set; }
    }
}
