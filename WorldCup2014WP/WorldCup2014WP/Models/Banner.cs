using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class Banner
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "img2")]
        public string Image { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "url")]
        public string URL { get; set; }
    }
}
