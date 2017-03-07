using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment6.Models;

namespace Assignment6.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {
            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
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
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.OrderBy(o => o.Name));
        }

        public IEnumerable<PlaylistBase> PlaylistGetAll()
        {
            return Mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBase>>(ds.Playlists.Include("Tracks").OrderBy(t => t.Name));
        }
        
        public PlaylistWithDetail PlaylistGetByIdWithDetail(int id)
        {
            var o = ds.Playlists.Find(id);
            ds.Playlists.Include("Tracks").SingleOrDefault(t => t.PlaylistId == id);
            return (o == null) ? null : Mapper.Map<Playlist, PlaylistWithDetail>(o);
        }

        public PlaylistWithDetail PlaylistEdit(PlaylistEditTracks newItem)
        {
            var o = ds.Playlists.Include("Tracks").SingleOrDefault(i => i.PlaylistId == newItem.PlaylistId);

            if (o == null)
            {
                return null;

            }
            else
            {
                o.Tracks.Clear();

                foreach (var item in newItem.TrackIds)
                {
                    var a = ds.Tracks.Find(item);
                    o.Tracks.Add(a);
                }

                ds.SaveChanges();
                return Mapper.Map<Playlist, PlaylistWithDetail>(o);
            }
        }

    }
}