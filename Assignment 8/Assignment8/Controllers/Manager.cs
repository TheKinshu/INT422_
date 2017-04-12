using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment8.Models;
using System.Security.Claims;

namespace Assignment8.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Declare a property to hold the user account for the current request
        // Can use this property here in the Manager class to control logic and flow
        // Can also use this property in a controller 
        // Can also use this property in a view; for best results, 
        // near the top of the view, add this statement:
        // var userAccount = new ConditionalMenu.Controllers.UserAccount(User as System.Security.Claims.ClaimsPrincipal);
        // Then, you can use "userAccount" anywhere in the view to render content
        public UserAccount UserAccount { get; private set; }

        public Manager()
        {
            // If necessary, add constructor code here

            // Initialize the UserAccount property
            UserAccount = new UserAccount(HttpContext.Current.User as ClaimsPrincipal);

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()



        public IEnumerable<GenreBase> GenreGetAll()
        {
            return Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBase>>(ds.Genres.OrderBy(g => g.Name));
        }

        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums.OrderBy(a => a.Name));
        }

        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return Mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBase>>(ds.Artists.OrderBy(a => a.Name));
        }

        public IEnumerable<TrackBase> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.OrderBy(a => a.Name));
        }

        public AlbumWithDetail AlbumGetByIdWithDetail(int id)
        {
            var o = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(a => a.Id == id);

            if (o == null)
            {
                return null;
            }
            else
            {
                var result = Mapper.Map<Album, AlbumWithDetail>(o);

                result.ArtistNames = o.Artists.Select(a => a.Name);

                return result;
            }
        }

        public AlbumBase AlbumAdd(AlbumAdd newItem)
        {
            var addedItem = ds.Albums.Add(Mapper.Map<AlbumAdd, Album>(newItem));

            var addGenre = ds.Genres.Find(newItem.GenreId);

            foreach (var item in newItem.TrackIds)
            {
                var a = ds.Tracks.Find(item);

                addedItem.Tracks.Add(a);
            }

            foreach (var item in newItem.ArtistIds)
            {
                var a = ds.Artists.Find(item);

                addedItem.Artists.Add(a);
            }

            addedItem.Genre = addGenre.Name;

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<Album, AlbumBase>(addedItem);
        }



        public ArtistWithDetail ArtistGetByIdWithDetail(int id)
        {
            var obj = ds.Artists.Include("Albums").SingleOrDefault(a => a.Id == id);

            return Mapper.Map<Artist, ArtistWithDetail>(obj);
        }

        public ArtistBase ArtistAdd(ArtistAdd newItem)
        {
            var addGenre = ds.Genres.Find(newItem.GenreId);

            var addedItem = ds.Artists.Add(Mapper.Map<ArtistAdd, Artist>(newItem));

            addedItem.Genre = addGenre.Name;

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<Artist, ArtistBase>(addedItem);
        }

        public TrackWithDetail TrackGetByIdWithDetail(int id)
        {
            var o = ds.Tracks.Include("Albums.Artists").SingleOrDefault(t => t.Id == id);

            return (o == null) ? null : Mapper.Map<Track, TrackWithDetail>(o);
        }

        public TrackBase TrackAdd(TrackAdd newItem, int id)
        {

            //var albumid = id;
            // variable gets id 
            var addGenre = ds.Genres.Find(newItem.GenreId);

            var addAlbum = ds.Albums.Find(id);

            var addedItem = ds.Tracks.Add(Mapper.Map<TrackAdd, Track>(newItem));

            addedItem.Genre = addGenre.Name;

            addedItem.Albums.Add(addAlbum);


            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<Track, TrackBase>(addedItem);
        }

        public TrackWithDetail TrackEdit(TrackEdit newItem)
        {
            var o = ds.Tracks.Include("Albums").SingleOrDefault(v => v.Id == newItem.Id);

            if (o == null)
            {
                return null;
            }
            else
            {
                ds.Entry(o).CurrentValues.SetValues(newItem);

                ds.SaveChanges();

                return Mapper.Map<Track, TrackWithDetail>(o);
            }
        }

        public bool TrackDelete(int id)
        {
            var deleteItem = ds.Tracks.Find(id);

            if (deleteItem == null)
            {
                return false;
            }
            else
            {
                ds.Tracks.Remove(deleteItem);

                ds.SaveChanges();

                return true;
            }
        }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Genre

            if (ds.RoleClaims.Count() == 0)
            {

                ds.RoleClaims.Add(new RoleClaim
                {
                    Name = "Executive"
                });
                ds.RoleClaims.Add(new RoleClaim
                {
                    Name = "Coordinator"
                });
                ds.RoleClaims.Add(new RoleClaim
                {
                    Name = "Clerk"
                });
                ds.RoleClaims.Add(new RoleClaim
                {
                    Name = "Staff"
                });
                // Add role claims

                ds.SaveChanges();
                done = true;
            }

            if (ds.Genres.Count() == 0)
            {

                ds.Genres.Add(new Genre
                {
                    Id = 1,
                    Name = "Pop"
                });

                ds.Genres.Add(new Genre
                {
                    Id = 2,
                    Name = "Rock"
                });

                ds.Genres.Add(new Genre
                {
                    Id = 3,
                    Name = "Classical"
                });

                ds.Genres.Add(new Genre
                {
                    Id = 4,
                    Name = "Metal"
                });

                ds.Genres.Add(new Genre
                {
                    Id = 5,
                    Name = "Jazz"
                });

                ds.Genres.Add(new Genre
                {
                    Id = 6,
                    Name = "Rap"
                });

                ds.Genres.Add(new Genre
                {
                    Id = 7,
                    Name = "Folk"
                });

                ds.Genres.Add(new Genre
                {
                    Id = 8,
                    Name = "Country"
                });

                ds.Genres.Add(new Genre
                {
                    Id = 9,
                    Name = "Techno"
                });

                ds.Genres.Add(new Genre
                {
                    Id = 10,
                    Name = "Opera"
                });

                ds.SaveChanges();
                done = true;
            }

            if (ds.Artists.Count() == 0)
            {

                ds.Artists.Add(new Artist
                {
                    Id = 1,
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 05, 05),
                    Executive = "admin@gmail.com",
                    UrlArtist = "http://www.kontrolmag.com/wp-content/uploads/2017/01/adele.jpg",
                    Genre = "Pop",
                    //Albums = new List<Album> { a21, a25 }
                });

                ds.Artists.Add(new Artist
                {
                    Id = 2,
                    Name = "Bryan Adams",
                    BirthName = "",
                    BirthOrStartDate = new DateTime(1959, 11, 05),
                    Executive = "admin@gmail.com",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg/220px-Bryan_Adams_Hamburg_MG_0631_flickr.jpg",
                    Genre = "Rock",
                    Albums = new List<Album>(0)
                });

                ds.Artists.Add(new Artist
                {
                    Id = 3,
                    Name = "The Beatles",
                    BirthName = "",
                    BirthOrStartDate = new DateTime(1962, 08, 15),
                    Executive = "admin@gmail.com",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg/220px-Beatles_ad_1965_just_the_beatles_crop.jpg",
                    Genre = "Pop",
                    Albums = new List<Album>(0)
                });

                ds.SaveChanges();
                done = true;
            }

            if (ds.Albums.Count() == 0)
            {

                var adele = ds.Artists.SingleOrDefault(a => a.Name == "Adele");


                ds.Albums.Add(new Album
                {
                    Id = 1,
                    Artists = new List<Artist> { adele },
                    Tracks = new List<Track> { },
                    Coordinator = "admin@gmail.com",
                    Genre = "Pop",
                    Name = "21",
                    ReleaseDate = new DateTime(2011, 1, 24),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/thumb/1/1b/Adele_-_21.png/220px-Adele_-_21.png"
                });

                ds.Albums.Add(new Album
                {
                    Id = 2,
                    Artists = new List<Artist> { adele },
                    Tracks = new List<Track> { },
                    Coordinator = "admin@gmail.com",
                    Genre = "Pop",
                    Name = "25",
                    ReleaseDate = new DateTime(2015, 11, 20),
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/thumb/9/96/Adele_-_25_%28Official_Album_Cover%29.png/220px-Adele_-_25_%28Official_Album_Cover%29.png"
                });

                ds.SaveChanges();
                done = true;
            }

            if (ds.Tracks.Count() == 0)
            {
                var a21 = ds.Albums.SingleOrDefault(a => a.Name == "21");
                var a25 = ds.Albums.SingleOrDefault(a => a.Name == "25");

                ds.Tracks.Add(new Track
                {
                    Id = 1,
                    Name = "Rolling in the Deep",
                    Clerk = "admin@gmail.com",
                    Composers = "Adele Adkins, Paul Epworth",
                    Genre = "Pop",
                    Albums = new List<Album> { a21 }
                });

                ds.Tracks.Add(new Track
                {
                    Id = 2,
                    Name = "Someone like You",
                    Clerk = "admin@gmail.com",
                    Composers = "Adele Adkins, Dan Wilson",
                    Genre = "Pop",
                    Albums = new List<Album> { a21 }
                });

                ds.Tracks.Add(new Track
                {
                    Id = 3,
                    Name = "Set Fire to the Rain",
                    Clerk = "admin@gmail.com",
                    Composers = "Adele Adkins, Fraser T Smith",
                    Genre = "Pop",
                    Albums = new List<Album> { a21 }
                });

                ds.Tracks.Add(new Track
                {
                    Id = 4,
                    Name = "Rumour Has It",
                    Clerk = "admin@gmail.com",
                    Composers = "Adele Adkins, Ryan Tedder",
                    Genre = "Pop",
                    Albums = new List<Album> { a21 }
                });

                ds.Tracks.Add(new Track
                {
                    Id = 5,
                    Name = "Turning Tables",
                    Clerk = "admin@gmail.com",
                    Composers = "Adele Adkins, Ryan Tedder",
                    Genre = "Pop",
                    Albums = new List<Album> { a21 }
                });

                ds.Tracks.Add(new Track
                {
                    Id = 6,
                    Name = "Hello",
                    Clerk = "admin@gmail.com",
                    Composers = "Adele Adkins, Greg Kurstin",
                    Genre = "Jazz",
                    Albums = new List<Album> { a25 }
                });

                ds.Tracks.Add(new Track
                {
                    Id = 7,
                    Name = "When We Were Young",
                    Clerk = "admin@gmail.com",
                    Composers = "Adele Adkins, Tobias Jesso Jr.",
                    Genre = "Jazz",
                    Albums = new List<Album> { a25 }
                });

                ds.Tracks.Add(new Track
                {
                    Id = 8,
                    Name = "Send My Love (To Your New Lover)",
                    Clerk = "admin@gmail.com",
                    Composers = "Adele Adkins, Max Martin",
                    Genre = "Pop",
                    Albums = new List<Album> { a25 }
                });

                ds.Tracks.Add(new Track
                {
                    Id = 9,
                    Name = "Water Under the Bridge",
                    Clerk = "admin@gmail.com",
                    Composers = "Adele Adkins, Greg Kurstin",
                    Genre = "Pop",
                    Albums = new List<Album> { a25 }
                });

                ds.Tracks.Add(new Track
                {
                    Id = 10,
                    Name = "Million Years Ago",
                    Clerk = "admin@gmail.com",
                    Composers = "Adele Adkins, Greg Kurstin",
                    Genre = "Folk",
                    Albums = new List<Album> { a25 }
                });


                ds.SaveChanges();
                done = true;
            }
            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "UserAccount" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it
    public class UserAccount
    {
        // Constructor, pass in the security principal
        public UserAccount(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        // Add other role-checking properties here as needed
        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}