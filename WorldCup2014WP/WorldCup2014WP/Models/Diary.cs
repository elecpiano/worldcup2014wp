using System;
using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class DiaryItemImage
    {
        [DataMember(Name = "img")]
        public string Image { get; set; }
    }

    [DataContract]
    public class DiaryItem
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "imgs")]
        public DiaryItemImage[] Images { get; set; }

        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "date")]
        public DateTime Time { get; set; }

        [DataMember(Name = "view")]
        public int ViewCount { get; set; }

        [DataMember(Name = "love")]
        public int LoveCount { get; set; }

        [IgnoreDataMember]
        public string Image
        {
            get
            {
                if (Images != null && Images.Length > 0)
                {
                    return Images[0].Image;
                }
                return string.Empty;
            }
        }
    }

    [DataContract]
    public class DiaryList
    {
        [DataMember(Name = "bgimg")]
        public string BigImage { get; set; }

        [DataMember(Name = "data")]
        public DiaryItem[] data { get; set; }
    }
}
