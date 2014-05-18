using System;
using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class Focus
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "time")]
        public DateTime Time { get; set; }
    }

    [DataContract]
    public class Recommendation
    {
        [DataMember]
        public Focus[] focus { get; set; }

        [DataMember]
        public News[] data { get; set; }
    }
}
