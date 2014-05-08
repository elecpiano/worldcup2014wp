using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class Category
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "img2")]
        public string Image2 { get; set; }

        [DataMember(Name = "adUrl")]
        public string AdImage { get; set; }

        [DataMember(Name = "adUrl2")]
        public string AdImage2 { get; set; }
    }
}
