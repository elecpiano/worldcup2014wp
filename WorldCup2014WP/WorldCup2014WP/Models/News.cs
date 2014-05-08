using System;
using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class News
    {
        [DataMember(Name="id")]
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

        [IgnoreDataMember]
        public bool IsMoreButton { get; set; }
    }

    [DataContract]
    public class NewsList
    {
        [DataMember]
        public News[] data { get; set; }

        [DataMember(Name = "total")]
        public int TotalPageCount { get; set; }

        [DataMember(Name = "page")]
        public int CurrentPageIndex { get; set; }

    }
}
