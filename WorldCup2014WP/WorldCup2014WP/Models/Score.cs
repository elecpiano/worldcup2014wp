using System;
using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class TeamData
    {
        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "win")]
        public string Win { get; set; }

        [DataMember(Name = "draws")]
        public string Draws { get; set; }

        [DataMember(Name = "lost")]
        public string Lost { get; set; }

        [DataMember(Name = "gsga")]
        public string GSGA { get; set; }

        [DataMember(Name = "played")]
        public string Played { get; set; }

        [DataMember(Name = "score")]
        public string Score { get; set; }
    }

    [DataContract]
    public class ScoreItemData
    {
        [DataMember(Name = "team")]
        public TeamData[] Teams { get; set; }

        [DataMember(Name = "group")]
        public string Group { get; set; }

        [IgnoreDataMember]
        public string GroupName
        {
            get
            {
                return Group + "组";
            }
        }
    }
}
