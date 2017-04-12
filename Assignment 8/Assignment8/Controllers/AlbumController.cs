using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class AlbumController : Controller
    {

        Manager m = new Manager();

        [AllowAnonymous]
        // GET: Album
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

       [Authorize(Roles = "Clerk")]
        [Route("album/{id}/addtrack")]
        // GET: Track/Create
        public ActionResult AddTrack(int id)
        {
            var form = new TrackAddForm();
            form.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");
            form.AlbumId = id;
            form.AlbumName = m.AlbumGetByIdWithDetail(id).Name;
            return View(form);
        }

        [Authorize(Roles = "Clerk")]
        [Route("album/{id}/addtrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAdd newItem, int id)
        {
            newItem.Clerk = HttpContext.User.Identity.Name;
            
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }
            var aid = id;
            var addedItem = m.TrackAdd(newItem, id);
            
            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("../Track/details", new { id = addedItem.Id });
            }
            
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
                return View(o);
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
