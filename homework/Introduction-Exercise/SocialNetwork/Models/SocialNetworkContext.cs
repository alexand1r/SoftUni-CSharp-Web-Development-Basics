namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;

    public class SocialNetworkContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SocialNetwork;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFriends>()
                .HasKey(uf => new {uf.UserId, uf.FriendId});

            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithOne(uf => uf.User)
                .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Users)
                .WithOne(uf => uf.Friend)
                .HasForeignKey(uf => uf.FriendId);

            modelBuilder.Entity<PictureAlbums>()
                .HasKey(pa => new {pa.PictureId, pa.AlbumId});

            modelBuilder.Entity<Picture>()
                .HasMany(p => p.Albums)
                .WithOne(a => a.Picture)
                .HasForeignKey(pa => pa.PictureId);

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Pictures)
                .WithOne(p => p.Album)
                .HasForeignKey(pa => pa.AlbumId);

            modelBuilder.Entity<Album>()
                .HasOne<User>(a => a.User)
                .WithMany(u => u.Albums)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<AlbumUser>()
                .HasKey(au => new {au.AlbumId, au.UserId});

            modelBuilder.Entity<Album>()
                .HasMany(a => a.SharedUsers)
                .WithOne(u => u.Album)
                .HasForeignKey(au => au.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SharedAlbums)
                .WithOne(a => a.User)
                .HasForeignKey(au => au.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tag>()
                .HasOne<Album>(t => t.Album)
                .WithMany(a => a.Tags)
                .HasForeignKey(t => t.AlbumId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
