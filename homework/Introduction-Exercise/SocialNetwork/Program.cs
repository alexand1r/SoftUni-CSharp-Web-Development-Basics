namespace SocialNetwork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SocialNetwork.Models;

    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            using (var db = new SocialNetworkContext())
            {
                db.Database.Migrate();
                Console.WriteLine("Database Created");

                SeedData(db);
                Console.WriteLine("Database Seeded");

                //1.2. Users And Friends
                //1.
                //PrintUsersWithFriends(db);

                //2.
                //PrintAllActiveUsersWithMoreThan5Friends(db);

                //!!!//3. Albums - Works Only Before The Addition Of Shared Albums
                //1.
                //PrintAlbumsWithOwners(db);

                //2.
                //PrintPicturesWithMoreThan2Appearances(db);

                //3.
                //PrintUsersWithPictures(db);

                //4. Tags
                //CheckTags(db);

                //1.
                //PrintAllAlbumsWithATag(db);

                //2. 


                //5. Shared Albums
                //1.
                //PrintUsersAndSharedAlbums(db);

                //2.
                //PrintAlbumsWithMoreThan2Users(db);

                //3.
                //PrintAlbumsWithPicturesForCertainUser(db);

                //6. Roles
                //1.
                //PrintAlbumsWithUsers(db);

                //2.
                //PrintCountOfAlbumsForACertainUser(db);

                //3.
                //PrintUsersWithAtleastOneViewerAlbum(db);
            }
        }

        private static void PrintUsersWithAtleastOneViewerAlbum(SocialNetworkContext db)
        {
            var users = db.Users
                .Where(u => u.SharedAlbums.Count > 0)
                .Select(u => new
                {
                    u.Username,
                    Albums = u.SharedAlbums.Count(sa => sa.Album.IsPublic)
                })
                .ToList();

            foreach (var u in users)
            {
                Console.WriteLine($"User: {u.Username}");
                Console.WriteLine($"Albums he can view only: {u.Albums}");
                Console.WriteLine();
            }
        }

        private static void PrintCountOfAlbumsForACertainUser(SocialNetworkContext db)
        {
            var str = Console.ReadLine().Trim();

            var user = db.Users
                .Where(u => u.Username == str)
                .Select(u => new
                {
                    u.Username,
                    CountOwnerAlbums = u.Albums.Count,
                    CountViewerAlbums = u.SharedAlbums.Count
                })
                .FirstOrDefault();

            Console.WriteLine($"Owner's Albums: {user.CountOwnerAlbums}");
            Console.WriteLine($"Viewer's Albums: {user.CountViewerAlbums}");
        }

        private static void PrintAlbumsWithUsers(SocialNetworkContext db)
        {
            var albums = db.Albums
                .Select(a => new
                {
                    a.Name,
                    Owner = a.User.Username,
                    SharedUsers = a.SharedUsers.Select(su => new
                    {
                        su.User.Username
                    }),
                    UCount = a.SharedUsers.Count
                })
                .OrderBy(a => a.Owner)
                .ThenByDescending(a => a.UCount)
                .ToList();

            foreach (var album in albums)
            {
                Console.WriteLine($"Album: {album.Name}");
                Console.WriteLine($"Owner: {album.Owner}");
                foreach (var sharedUser in album.SharedUsers)
                {
                    Console.WriteLine($"Viewer: {sharedUser.Username}");
                }
                Console.WriteLine();
            }
        }

        private static void PrintAllAlbumsWithATag(SocialNetworkContext db)
        {
            var albums = db.Albums
                .Where(a => a.Tags.Count > 0)
                .Select(a => new
                {
                    a.Name,
                    Owner = a.User.Username,
                    Tags = a.Tags.Count
                })
                .OrderByDescending(a => a.Tags)
                .ThenBy(a => a.Name)
                .ToList();

            foreach (var album in albums)
            {
                Console.WriteLine($"Album: {album.Name}");
                Console.WriteLine($"Owner: {album.Owner}");
                Console.WriteLine();
            }
        }

        private static void PrintAlbumsWithPicturesForCertainUser(SocialNetworkContext db)
        {
            var username = Console.ReadLine().Trim();

            var albums = db.Albums
                .Select(a => new
                {
                    a.Name,
                    Pictures = a.Pictures.Count,
                    Users = a.SharedUsers.Select(su => new
                    {
                        su.User.Username
                    })
                })
                .OrderByDescending(a => a.Pictures)
                .ThenBy(a => a.Name)
                .ToList();

            foreach (var album in albums)
            {
                foreach (var sharedUser in album.Users)
                {
                    if (sharedUser.Username == username)
                    {
                        Console.WriteLine($"Album: {album.Name}");
                        Console.WriteLine($"Pictures: {album.Pictures}");
                        Console.WriteLine();
                    }
                }
            }
        }

        private static void PrintAlbumsWithMoreThan2Users(SocialNetworkContext db)
        {
            var albums = db.Albums
                .Where(a => a.SharedUsers.Count > 2)
                .Select(a => new
                {
                    a.Name,
                    People = a.SharedUsers.Count,
                    Status = a.IsPublic ? "Public" : "Not Public"
                })
                .OrderByDescending(a => a.People)
                .ThenBy(a => a.Name)
                .ToList();

            foreach (var album in albums)
            {
                Console.WriteLine($"Album: {album.Name}");
                Console.WriteLine($"People: {album.People}");
                Console.WriteLine($"Status: {album.Status}");
                Console.WriteLine();
            }
        }

        private static void PrintUsersAndSharedAlbums(SocialNetworkContext db)
        {
            var users = db.Users
                .Select(u => new
                {
                    u.Username,
                    Friends = u.Friends.Select(f => new
                    {
                        f.User.Username,
                        f.User.SharedAlbums
                    }),
                    u.Albums
                })
                .OrderBy(u => u.Username)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Username}");
                foreach (var friend in user.Friends)
                {
                    Console.WriteLine($"--Friend: {friend.Username}");
                    foreach (var sharedAlbum in friend.SharedAlbums)
                    {
                        if (user.Albums.Contains(sharedAlbum.Album))
                        {
                            Console.WriteLine($"---Album: {sharedAlbum.Album.Name}");
                        }
                    }
                }
            }
        }

        private static void CheckTags(SocialNetworkContext db)
        {
            var str = Console.ReadLine();
            
            db.Tags.Add(new Tag
            {
                TagString = TagTransformer.Transform(str)
            });

            db.SaveChanges();

            foreach (var tag in db.Tags)
            {
                Console.WriteLine(tag.TagString);
            }
        }

        private static void PrintUsersWithPictures(SocialNetworkContext db)
        {
            var users = db.Users
                .Select(u => new
                {
                    u.Username,
                    Albums = u.Albums.Select(a => new
                    {
                        a.Name,
                        a.IsPublic,
                        Pictures = a.Pictures.Select(p => new
                        {
                            p.Picture.Title,
                            p.Picture.Path
                        })
                    })
                    .OrderBy(a => a.Name)
                })
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Username}");
                foreach (var album in user.Albums)
                {
                    if (album.IsPublic)
                    {
                        Console.WriteLine($"--Album: {album.Name}");
                        foreach (var picture in album.Pictures)
                        {
                            Console.WriteLine($"---Picture: {picture.Title}");
                            Console.WriteLine($"---Picture Path: {picture.Path}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"--Album: {album.Name}");
                        Console.WriteLine($"--Error: Private content!");
                    }
                }
                Console.WriteLine();
            }
        }

        private static void PrintPicturesWithMoreThan2Appearances(SocialNetworkContext db)
        {
            var pictures = db.Pictures
                .Where(p => p.Albums.Count > 2)
                .Select(p => new
                {
                    p.Title,
                    AlbumCount = p.Albums.Count,
                    Albums = p.Albums.Select(a => new
                    {
                        a.Album.Name,
                        Owner = a.Album.User.Username
                    })
                })
                .OrderByDescending(p => p.AlbumCount)
                .ThenBy(p => p.Title)
                .ToList();

            foreach (var picture in pictures)
            {
                Console.WriteLine($"Picture: {picture.Title}");
                foreach (var album in picture.Albums)
                {
                    Console.WriteLine($"--Album: {album.Name} - Owner: {album.Owner}");
                }
            }
        }

        private static void PrintAlbumsWithOwners(SocialNetworkContext db)
        {
            var albums = db.Albums
                .Select(a => new
                {
                    a.Name,
                    Owner = a.User.Username,
                    Pictures = a.Pictures.Count
                })
                .OrderByDescending(a => a.Pictures)
                .ThenBy(a => a.Name)
                .ToList();

            foreach (var album in albums)
            {
                Console.WriteLine($"Album: {album.Name}");
                Console.WriteLine($"Pictures: {album.Pictures}");
                Console.WriteLine($"Owner: {album.Owner}");
                Console.WriteLine();
            }
        }

        private static void PrintAllActiveUsersWithMoreThan5Friends(SocialNetworkContext db)
        {
            var users = db.Users
                .Where(u => u.IsDeleted == false && u.Friends.Count > 5)
                .Select(u => new
                {
                    u.Username,
                    Friends = u.Friends.Count,
                    Period = DateTime.Now.Subtract(u.RegisteredOn).TotalDays,
                    u.RegisteredOn
                })
                .OrderBy(u => u.RegisteredOn)
                .ThenByDescending(u => u.Friends)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Friends: {user.Friends}");
                Console.WriteLine($"Period: {user.Period:F2}");
            }
        }

        private static void PrintUsersWithFriends(SocialNetworkContext db)
        {
            var users = db.Users
                .Select(u => new
                {
                    u.Username,
                    Friends = u.Friends.Count,
                    Status = u.IsDeleted ? "Inactive" : "Active"
                })
                .OrderByDescending(u => u.Friends)
                .ThenBy(u => u.Username)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Friends: {user.Friends}");
                Console.WriteLine($"Status: {user.Status}");
                Console.WriteLine();
            }
        }

        private static void SeedData(SocialNetworkContext db)
        {
            var maxUsers = 20;
            var currentDate = DateTime.Now;
            var users = new List<int>();

            //Adding Users

            for (int i = 0; i < maxUsers; i++)
            {
                var user = new User
                {
                    Username = $"Username {i}",
                    Age = i + 20,
                    Email = $"email_{i}@email.com",
                    IsDeleted = false,
                    Password = $"Password{i}",
                    RegisteredOn = currentDate.AddDays(i - 30),
                    LastTimeLoggedIn = currentDate.AddDays(i),
                };
                db.Users.Add(user);
            }

            db.SaveChanges();

            foreach (var user in db.Users)
            {
                users.Add(user.Id);
            }

            //Adding Friends

            for (int i = 0; i < users.Count; i++)
            {
                var currentUserId = users[i];
               
                var currentUser = db.Users.FirstOrDefault(u => u.Id == currentUserId);
                    
                currentUser.Friends.Add(new UserFriends
                {
                    FriendId = users[users.Count - 1 - i]
                });
            }

            db.SaveChanges();

            //Adding Pictures

            for (int i = 0; i < 50; i++)
            {
                db.Pictures.Add(new Picture()
                {
                    Title = $"Title {i}",
                    Caption = $"Caption {i}",
                    Path = $"C://Users/User {i}/My Pictures"
                });
            }

            db.SaveChanges();

            //Adding Albums

            var addedAlbums = new List<Album>();

            for (int i = 0; i < 20; i++)
            {
                var album = new Album()
                {
                    Name = $"Album {i}",
                    BgColor = $"Color {i}",
                    IsPublic = i % 2 == 0,
                    UserId = i + 1
                };
                addedAlbums.Add(album);
                db.Albums.Add(album);
            }

            db.SaveChanges();

            //Adding Tags To Albums

            for (int i = 0; i < 20; i++)
            {
                var currentAlbum = addedAlbums[i];
                var tagsCount = random.Next(1, 10 / 2);
                for (int j = 0; j < tagsCount; j++)
                {
                    currentAlbum.Tags.Add(new Tag()
                    {
                        TagString = $"Tag {i + j}"
                    });
                }
            }

            db.SaveChanges();

            //Adding Pictures To Albums

            var picturesIds = db.Pictures.Select(p => p.Id).ToList();

            for (int i = 0; i < 20; i++)
            {
                var currentAlbum = addedAlbums[i];
                var picturesInAlbum = random.Next(2, 50 / 2);
                var addedPictures = new List<int>();

                for (int j = 0; j < picturesInAlbum; j++)
                {
                    var pictureId = picturesIds[random.Next(0, picturesIds.Count)];

                    while (addedPictures.Contains(pictureId))
                    {
                        pictureId = picturesIds[random.Next(0, picturesIds.Count)];
                    }

                    addedPictures.Add(pictureId);

                    currentAlbum.Pictures.Add(new PictureAlbums()
                    {
                        PictureId = pictureId
                    });

                }
            }

            db.SaveChanges();

            // Adding Users To Albums

            var usersIds = db.Users.Select(u => u.Id).ToList();

            for (int i = 0; i < 20; i++)
            {
                var currentAlbum = addedAlbums[i];
                var usersForAlbum = random.Next(2, 20 / 2);
                var addedUsers = new List<int>();

                for (int j = 0; j < usersForAlbum; j++)
                {
                    var userId = usersIds[random.Next(0, usersIds.Count)];

                    while (addedUsers.Contains(userId))
                    {
                        userId = usersIds[random.Next(0, usersIds.Count)];
                    }

                    addedUsers.Add(userId);

                    currentAlbum.SharedUsers.Add(new AlbumUser()
                    {
                        UserId = userId
                    });

                }
            }

            db.SaveChanges();
        }
    }
}
