namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        private int age;

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\\W).{4,30})")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^\\w+([-_+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$")]
        public string Email { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        public int Age
        {
            get { return this.age; }
            set
            {
                if (value > 0 && value < 121)
                    this.age = value;
            }
        }

        public bool IsDeleted { get; set; }
        
        public List<UserFriends> Friends { get; set; } = new List<UserFriends>();
        public List<UserFriends> Users { get; set; } = new List<UserFriends>();

        public List<AlbumUser> SharedAlbums { get; set; } = new List<AlbumUser>();
        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
