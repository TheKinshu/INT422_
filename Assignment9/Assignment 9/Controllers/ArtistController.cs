using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class ArtistController : Controller
    {
        Manager m = new Manager();
        // GET: Artist
        [AllowAnonymous]

        public ActionResult Index()
        {
            var o = m.ArtistGetAll();
            return View(o);
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.ArtistGetByIdWithMediaItemInfo(id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }
        [Authorize(Roles = "Executive")]
        // GET: Album/Create
        public ActionResult Create()
        {
            var form = new ArtistAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

            return View(form);
        }

        // POST: Album/Create\
        [Authorize(Roles = "Executive")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ArtistAdd newItem)
        {
            newItem.Executive = HttpContext.User.Identity.Name;

            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ArtistAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

        // GET: Album/Create
        public ActionResult AddMediaItem(int? id)
        {
            var a = m.ArtistGetByIdWithDetail(id.GetValueOrDefault());
            var form = new MediaItemAddForm();
            form.ArtistId = a.Id;
            return View(form);
        }

        // POST: Album/Create\
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddMediaItem(int? id, MediaItemAdd newItem)
        {
            var a = m.ArtistGetByIdWithDetail(id.GetValueOrDefault());
            newItem.ArtistId = a.Id;
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.MediaItemAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("../artist/details", new { id = addedItem.Id });
            }
        }

        [Authorize(Roles = "Coordinator")]
        [Route("artist/{id}/addAlbum")]
        // GET: Album/Create
        public ActionResult addAlbum(int? id)
        {
            var a = m.ArtistGetByIdWithDetail(id.GetValueOrDefault());
            var form = new AlbumAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");
            form.ArtistName = a.Name;
            form.ArtistId = a.Id;
            return View(form);
        }

        // POST: Album/Create
        [Authorize(Roles = "Coordinator")]
        [ValidateInput(false)]
        [Route("artist/{id}/addAlbum")]
        [HttpPost]
        public ActionResult addAlbum(AlbumAdd newItem)
        {
            newItem.Coordinator = HttpContext.User.Identity.Name;

            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.AlbumAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("../album/details", new { id = addedItem.Id });
            }
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
