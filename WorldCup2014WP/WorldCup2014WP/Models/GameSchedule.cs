using System;
using System.Runtime.Serialization;
using WorldCup2014WP.Infrastructures;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class GameSchedule : BindableBase
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "cate")]
        public string Category { get; set; }

        [DataMember(Name = "start")]
        public DateTime StartTime { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "match")]
        public string Match { get; set; }

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "end")]
        public DateTime EndTime { get; set; }

        private bool subscribed = false;
        [IgnoreDataMember]
        public bool Subscribed
        {
            get { return subscribed; }
            set { SetProperty(ref this.subscribed, value); }
        }

        private string arrowImage = "/Assets/Images/ArrowDown.png";//default value does not work, don't know why
        public string ArrowImage
        {
            get { return arrowImage; }
            set { SetProperty(ref this.arrowImage, value); }
        }
    }
}
