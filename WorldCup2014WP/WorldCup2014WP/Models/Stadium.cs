using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class Stadium
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "namecn")]
        public string NameCN { get; set; }

        [DataMember(Name = "nameen")]
        public string NameEN { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }
    }
}
