using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class AlbumController : Controller
    {
        Manager m = new Manager();

        [AllowAnonymous]
        // GET: Album
        public ActionResult Index()
        {
            var o = m.AlbumGetAll();
            return View(o);
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.AlbumGetByIdWithDetail(id.GetValueOrDefault());
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

        // GET: Album/Create
        [Authorize(Roles = "Clerk")]

        [Route("album/{id}/addtrack")]
        public ActionResult AddTrack(int? id)
        {
            var a = m.AlbumGetByIdWithDetail(id.GetValueOrDefault());
            var form = new TrackAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");
            form.AlbumName = a.Name;
            return View(form);
        }

        // POST: Album/Create
        [Authorize(Roles = "Clerk")]

        [ValidateInput(false)]
        [Route("album/{id}/addtrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAdd newItem)
        {
            newItem.Clerk = HttpContext.User.Identity.Name;

            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.TrackAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("../track/details", new { id = addedItem.Id });
            }
        }

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Album/Edit/5
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

        // GET: Album/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Album/Delete/5
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
