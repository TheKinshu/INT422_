using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class TrackBase
    {
        public TrackBase()
        {

        }
        [Key]
        public int Id { get; set; }

        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Display(Name = "Track genre")]
        public string Genre { get; set; }

        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

        [Display(Name = "Album Name")]
        public string AlbumName { get; set; }

        public string AudioUrl
        {
            get
            {
                return $"/clip/{Id}";
            }
        }
    }

    public class TrackWithDetail : TrackBase
    {

    }

    public class TrackAdd
    {
        public TrackAdd()
        {
            AlbumName = "";
        }

        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Display(Name = "Track genre")]
        public string Genre { get; set; }

        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

        public string AlbumName { get; set; }

        public int AlbumId { get; set; }

        public int GenreId { get; set; }

        [Required]
        public HttpPostedFileBase AudioUpload { get; set; }
    }

    public class TrackAddForm
    {
        public TrackAddForm()
        {
            Albums = new List<AlbumBase>();
        }

        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }

        [Display(Name = "Track genre")]
        public string Genre { get; set; }

        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

        public string AlbumName { get; set; }

        public int AlbumId { get; set; }

        public int GenreId { get; set; }

        public IEnumerable<AlbumBase> Albums { get; set; }

        public SelectList GenreList { get; set; }

        [Required]
        [Display(Name = "Sample Clip")]
        [DataType(DataType.Upload)]
        public string AudioUpload { get; set; }

    }
    public class TrackAudio
    {
        public int Id { get; set; }
        public string AudioContentType { get; set; }
        public byte[] Audio { get; set; }
    }

    public class TrackEditForm
    {
        public int Id { get; set; }

        [Display(Name = "Track name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Clip")]
        [DataType(DataType.Upload)]
        public string AudioUpload { get; set; }
    }

    public class TrackEdit
    {
        public int Id { get; set; }
        [Display(Name = "Track name")]
        public string Name { get; set; }
        [Required]
        public HttpPostedFileBase AudioUpload { get; set; }
    }
}