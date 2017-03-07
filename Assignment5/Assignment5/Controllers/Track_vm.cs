using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TrackBase
    {
        public TrackBase()
        {

        }

        [Key]
        public int TrackId { get; set; }
        [Display(Name = "Track name")]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Artist name")]
        public string AlbumArtistName { get; set; }

        [Display(Name = "Album title")]
        public string AlbumTitle { get; set; }

        [Display(Name = "Media type")]
        public string MediaTypeName { get; set; }
    }
    public class TrackWithDetail : TrackBase
    {
        public TrackWithDetail() { }
        [Display(Name = "Artist name")]
        public string AlbumArtistName { get; set; }
        [Display(Name = "Album title")]
        public string AlbumTitle { get; set; }
        //public string MediaTypeName { get; set; }
        public MediaTypeBase MediaType { get; set; }
    }

    public class TrackAdd
    {
        public TrackAdd()
        {
            Name = "";
            Composer = "";
            Milliseconds = 0;
            UnitPrice = 0;
        }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public decimal UnitPrice { get; set; }

        [Range(1, Int32.MaxValue)]
        public int AlbumId { get; set; }


        [Range(1, Int32.MaxValue)]
        public int MediaTypeId { get; set; }

    }

    public class TrackAddForm : TrackAdd
    {
        public TrackAddForm()
        {

        }

        public SelectList AlbumList { get; set; }

        public string AlbumTitle { get; set; }

        public SelectList MediaTypeList { get; set; }
    }
}