using System.Runtime.Serialization;

namespace WorldCup2014WP.Models
{
    [DataContract]
    public class AlbumItem
    {
        public AlbumItem()
        { }

        public AlbumItem(string image, string title)
        {
            this.Image = image;
            this.Title = title;
        }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }
    }

    [DataContract]
    public class Album
    {
        [DataMember(Name = "album")]
        public AlbumItem[] Items { get; set; }
    }
}
