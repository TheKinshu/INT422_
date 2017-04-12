using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class AlbumBase : AlbumAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class AlbumAdd
    {
        public AlbumAdd()
        {
            ReleaseDate = DateTime.Now;
        }

        [Display(Name = "Coordinator who looks after the album")]
        public string Coordinator { get; set; }

        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }

        public int GenreId { get; set; }

        [Required]
        [Display(Name="Album name")]
        public string Name { get; set; }

        [Display(Name = "Release date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Album image (cover art)")]
        public string UrlAlbum { get; set; }

        public IEnumerable<int> TrackIds { get; set; }

        public IEnumerable<int> ArtistIds { get; set; }
    }
  
    public class AlbumAddForm : AlbumAdd
    {
        public string ArtistName { get; set; }

        [Display(Name = "Album's primary genre")]
        public SelectList GenreList { get; set; }

        public MultiSelectList TrackList { get; set; }

        public MultiSelectList ArtistList { get; set; }
    }

    public class AlbumWithDetail : AlbumBase
    {
        public AlbumWithDetail()
        {
            Tracks = new List<TrackBase>();
            Artists = new List<ArtistBase>();
        }

        public IEnumerable<String> ArtistNames { get; set; }

        [Display(Name = "Number of artists on this album")]
        public IEnumerable<ArtistBase> Artists { get; set; }
        [Display(Name = "Number of tracks on this album")]
        public IEnumerable<TrackBase> Tracks { get; set; }
    }
   
}