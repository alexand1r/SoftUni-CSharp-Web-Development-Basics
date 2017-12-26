namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string BgColor { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<AlbumUser> SharedUsers { get; set; } = new List<AlbumUser>();

        public List<PictureAlbums> Pictures { get; set; } = new List<PictureAlbums>();

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
