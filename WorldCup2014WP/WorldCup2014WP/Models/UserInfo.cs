using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class UserInfo
    {
        [DataMember(Name="msg")]
        public string Message { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "data")]
        public UserInfoData Data { get; set; }
    }

    [DataContract]
    public class UserInfoData
    {
        [DataMember(Name = "point")]
        public int point { get; set; }

        [DataMember(Name = "exp")]
        public int Exp { get; set; }

        [DataMember(Name = "rank")]
        public string Rank { get; set; }

        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "nickname")]
        public string NickName { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "userid")]
        public string SessionID { get; set; }

        [DataMember(Name = "gold")]
        public int Gold { get; set; }
    }
}
