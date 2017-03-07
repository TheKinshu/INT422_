using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment5.Models;

namespace Assignment5.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {
            // If necessary, add constructor code here
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


        public IEnumerable<TrackBase> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.OrderBy(o => o.TrackId));
        }

        public TrackBase TrackGetById(int id)
        {
            var o = ds.Tracks.Find(id);

            return (o == null) ? null : Mapper.Map<Track, TrackBase>(o);
        }

        public TrackWithDetail TrackGetByIdWithDetail(int id)
        {
            var o = ds.Tracks.Find(id);
            ds.Tracks.Include("Album.Artist")
              .Include("MediaType")
              .SingleOrDefault(t => t.TrackId == id);
            return (o == null) ? null : Mapper.Map<Track, TrackWithDetail>(o);
        }

        public TrackWithDetail TrackAdd(TrackAdd addTrack)
        {
            var addAlbum = ds.Albums.Find(addTrack.AlbumId);
            var addMediaType = ds.MediaTypes.Find(addTrack.MediaTypeId);

            var addItem = ds.Tracks.Add(Mapper.Map<TrackAdd, Track>(addTrack));

            addItem.Album = addAlbum;
            addItem.MediaType = addMediaType;
            ds.SaveChanges();

            return (addItem == null) ? null : Mapper.Map<Track,TrackWithDetail>(addItem);
        }

        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums);
        }
        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return Mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBase>>(ds.Artists);
        }
        public IEnumerable<MediaTypeBase> MediaTypeGetAll()
        {
            return Mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBase>>(ds.MediaTypes);
        }


        public AlbumBase AlbumGetById(int id)
        {
            var o = ds.Albums.Find(id);

            return (o == null) ? null : Mapper.Map<Album, AlbumBase>(o);
        }

        public ArtistBase ArtistGetById(int id)
        {
            var o = ds.Artists.Find(id);

            return (o == null) ? null : Mapper.Map<Artist, ArtistBase>(o);
        }

        public MediaTypeBase MediaTypeGetById(int id)
        {
            var o = ds.MediaTypes.Find(id);

            return (o == null) ? null : Mapper.Map<MediaType, MediaTypeBase>(o);
        }

        
    }
}