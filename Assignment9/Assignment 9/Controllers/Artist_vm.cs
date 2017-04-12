using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class ArtistBase
    {
        public ArtistBase()
        {
            BirthOrStartDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [Display(Name = "If applicable artist's birth name")]
        public string BirthName { get; set; }

        [Display(Name = "Birth date or start date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }

        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        public string Executive { get; set; }

        [DataType(DataType.MultilineText)]
        public string Portrayal { get; set; }

    }

    public class ArtistWithDetail : ArtistBase
    {

    }

    public class ArtistAdd : ArtistBase
    {
        public int GenreId { get; set; }
    }

    public class ArtistAddForm : ArtistAdd
    {
        public ArtistAddForm()
        {
            Albums = new List<AlbumBase>();
        }

        public IEnumerable<AlbumBase> Albums { get; set; }

        public SelectList GenreList { get; set; }
    }

    public class ArtistWithMediaItemStringIds : ArtistBase
    {
        public ArtistWithMediaItemStringIds()
        {
            MediaItems = new List<MediaItemBase>();
        }

        public IEnumerable<MediaItemBase> MediaItems { get; set; }
    }
}