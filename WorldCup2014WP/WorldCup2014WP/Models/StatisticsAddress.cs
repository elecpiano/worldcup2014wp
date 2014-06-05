using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class StatisticsAddress
    {
        [DataMember(Name = "wcdata")]
        public string URL { get; set; }
    }
}
