using System;
using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class NewsGroup
    {
        [DataMember(Name="cate")]
        public string GroupTitle { get; set; }

        [DataMember(Name="news")]
        public News[] NewsList { get; set; }
    }

    [DataContract]
    public class Subject
    {
        [DataMember(Name = "contents")]
        public NewsGroup[] NewsGroups { get; set; }

        [DataMember(Name = "focus")]
        public Focus[] FocusList { get; set; }
    }

}
