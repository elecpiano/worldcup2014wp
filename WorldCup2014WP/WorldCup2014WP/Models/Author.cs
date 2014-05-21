using System;
using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class AuthorImage
    {
        [DataMember(Name = "img")]
        public string Image { get; set; }
    }

    [DataContract]
    public class Author
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "face")]
        public string Face { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name="imgs")]
        public AuthorImage[] Images { get; set; }

        [DataMember(Name = "view")]
        public int ViewCount { get; set; }

        [DataMember(Name = "love")]
        public int LoveCount { get; set; }
    }

    [DataContract]
    public class AuthorList
    {
        [DataMember]
        public Author[] data { get; set; }

        
    }
}
