using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class UserLoginResult
    {
        [DataMember(Name="msg")]
        public string Message { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "userid")]
        public string SessionID { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "nickname")]
        public string NickName { get; set; }

        [DataMember(Name = "gold")]
        public string Gold { get; set; }

        [DataMember(Name = "goldlogin")]
        public string GoldLogin { get; set; }

        [DataMember(Name = "rank")]
        public string Rank { get; set; }

        [DataMember(Name = "exp")]
        public string Exp { get; set; }
    }
}
