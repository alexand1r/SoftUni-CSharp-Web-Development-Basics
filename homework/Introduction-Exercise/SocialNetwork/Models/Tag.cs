namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using SocialNetwork.Models.Attributes;

    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Tag]
        public string TagString { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }
}
