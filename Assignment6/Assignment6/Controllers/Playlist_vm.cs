using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment6.Controllers
{
    public class PlaylistBase
    {
        [Key]
        public int PlaylistId { get; set; }
        [Display(Name = "Playlist name")]
        public string Name { get; set; }

        public int TracksCount { get; set; }
    }

    public class PlaylistWithDetail : PlaylistBase
    {
        [Display(Name = "Track now on the playlist")]
        public string TrackName { get; set; }
        public IEnumerable<TrackBase> Tracks { get; set; }

    }

    public class PlaylistEditTracksForm : PlaylistBase
    {
        public PlaylistEditTracksForm()
        {
            TrackOnPlaylist = new List<TrackBase>();
        }
        public int PlaylistId { get; set; }
        public string Name { get; set; }
        [Display(Name = "All tracks")]
        public MultiSelectList TrackList { get; set; }
        [Display(Name = "Now on playlist")]
        public IEnumerable<TrackBase> TrackOnPlaylist { get; set; }
    }

    public class PlaylistEditTracks
    {
        public PlaylistEditTracks()
        {
            TrackIds = new List<int>();
        }
        public int PlaylistId { get; set; }
        
        public IEnumerable<int> TrackIds { get; set; }
    }
}