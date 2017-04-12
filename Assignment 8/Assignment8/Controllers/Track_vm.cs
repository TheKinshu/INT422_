using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class TrackBase : TrackAdd
    {
        [Key]
        public int Id { get; set; }     
        
    }

    public class TrackAdd
    {
        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        public string Genre { get; set; }

        [Display(Name = "Track genre")]
        public int GenreId { get; set;}

        [Required]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

    }

    public class TrackAddForm : TrackAdd
    {
        public SelectList GenreList { get; set; }
    }

    public class TrackWithDetail : TrackBase
    {

        public TrackWithDetail()
        {
            Albums = new List<AlbumBase>();
        }

        [Display(Name = "Albums with this track")]

        public IEnumerable<AlbumBase> Albums { get; set; }
    }

    public class TrackEditForm
    {
        public int Id { get; set; }

        public string Composers { get; set; }

        public string Name { get; set; }
    }

    public class TrackEdit
    {
        public int Id { get; set; }

        public string Composers { get; set; }

        [Required]
        public string Name { get; set; }

    }
}