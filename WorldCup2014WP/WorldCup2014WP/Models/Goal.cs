using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class Goal
    {
        [DataMember(Name="img")]
        public string Image { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "goal")]
        public string GoalCount { get; set; }

        [IgnoreDataMember]
        public int Index { get; set; }

    }
}
