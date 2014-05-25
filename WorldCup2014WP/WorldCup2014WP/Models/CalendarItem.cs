
using System;
using System.Runtime.Serialization;
using System.Windows;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class CalendarItem
    {
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "football")]
        public string Football { get; set; }

        [IgnoreDataMember]
        public Visibility HasGame
        {
            get
            {
                return Football == "0" ? Visibility.Collapsed : Visibility.Visible;
            }
        }

    }
}
