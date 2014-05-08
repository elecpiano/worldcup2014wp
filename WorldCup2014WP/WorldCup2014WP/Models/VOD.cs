using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class VOD
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "url")]
        public string URL { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "videos")]
        public VideoItem[] Videos { get; set; }
    }

    [DataContract]
    public class VideoItem
    {
        [DataMember(Name = "duration")]
        public string Duration { get; set; }

        [DataMember(Name = "image")]
        public string Image { get; set; }

        [DataMember(Name = "url")]
        public string URL { get; set; }

    }
}
