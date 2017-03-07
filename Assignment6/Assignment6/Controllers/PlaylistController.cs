using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment6.Controllers
{
    public class PlaylistController : Controller
    {
        Manager m = new Manager();
        // GET: Playlist
        public ActionResult Index()
        {
            var o = m.PlaylistGetAll();
            return View(o);
        }

        // GET: Playlist/Details/5
        public ActionResult Details(int id)
        {
            var o = m.PlaylistGetByIdWithDetail(id);
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
            
        }

        // GET: Playlist/Edit/5
        public ActionResult Edit(int id)
        {
            var o = m.PlaylistGetByIdWithDetail(id);
            var selectedValues = o.Tracks.Select(e => e.TrackId);
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = AutoMapper.Mapper.Map<PlaylistBase, PlaylistEditTracksForm>(o);
                editForm.TrackList = new MultiSelectList(m.TrackGetAll(), "TrackId", dataTextField: "NameFull",selectedValues: selectedValues);

                List<TrackBase> temp = new List<TrackBase>();
                foreach (var item in o.Tracks)
                {
                    temp.Add(item);
                }
                editForm.TrackOnPlaylist = temp;

                return View(editForm);

            }
        }

        // POST: Playlist/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracks collection)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = collection.PlaylistId });
            }
            if (id.GetValueOrDefault() != collection.PlaylistId)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = m.PlaylistEdit(collection);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = collection.PlaylistId });
            }
            else
            {
                return RedirectToAction("Details", new { id = collection.PlaylistId });
            }
        }
    }
}
