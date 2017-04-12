using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class AlbumBase
    {
        public AlbumBase()
        {
            ReleaseDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [Display(Name = "Album name")]

        public string Name { get; set; }

        [Display(Name = "Release date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Album image (cover art)")]

        public string UrlAlbum { get; set; }

        [Display(Name = "Album's primary genre")]

        public string Genre { get; set; }

        [Display(Name = "Coordinator who looks after the album")]

        public string Coordinator { get; set; }

        [DataType(DataType.MultilineText)]

        public string Depiction { get; set; }
    }

    public class AlbumWithDetail : AlbumBase
    {

    }

    public class AlbumAdd : AlbumBase
    {
        public string ArtistName { get; set; }

        public int ArtistId { get; set; }

        public int TrackId { get; set; }

        public int GenreId { get; set; }
    }

    public class AlbumAddForm : AlbumAdd
    {
        public AlbumAddForm()
        {
            Artists = new List<ArtistBase>();
            Tracks = new List<TrackBase>();

        }

        public IEnumerable<ArtistBase> Artists { get; set; }

        public IEnumerable<TrackBase> Tracks { get; set; }

        public SelectList GenreList { get; set; }
    }
}