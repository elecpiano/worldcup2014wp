
using System;
using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class CalendarItem
    {
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "gold")]
        public int Gold { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [IgnoreDataMember]
        public string GameDate { get; set; }
    }
}
