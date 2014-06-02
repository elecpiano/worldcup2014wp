using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class GuessGame
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "name1")]
        public string Country1 { get; set; }

        [DataMember(Name = "name2")]
        public string Country2 { get; set; }

        [DataMember(Name = "img1")]
        public string Image1 { get; set; }

        [DataMember(Name = "img2")]
        public string Image2 { get; set; }

        [DataMember(Name = "options")]
        public GuessGameOption[] Options { get; set; }

        [IgnoreDataMember]
        public GuessGameOption Option0
        {
            get
            {
                GuessGameOption option = null;
                if (Options!=null && Options.Length>0)
                {
                    option = Options[0];
                }
                return option;
            }
        }

        [IgnoreDataMember]
        public GuessGameOption Option1
        {
            get
            {
                GuessGameOption option = null;
                if (Options != null && Options.Length > 1)
                {
                    option = Options[1];
                }
                return option;
            }
        }

        [IgnoreDataMember]
        public GuessGameOption Option2
        {
            get
            {
                GuessGameOption option = null;
                if (Options != null && Options.Length > 2)
                {
                    option = Options[2];
                }
                return option;
            }
        }

    }

    [DataContract]
    public class GuessGameOption
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }

        [DataMember(Name = "win")]
        public int Win { get; set; }

        [DataMember(Name = "opt")]
        public string OptionName { get; set; }
    }

}
