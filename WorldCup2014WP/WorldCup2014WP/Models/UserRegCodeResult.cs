using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class UserRegCodeResult
    {
        [DataMember(Name = "msg")]
        public string Message { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }
    }
}
