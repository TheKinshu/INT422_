using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class TrackController : Controller
    {

        Manager m = new Manager();

        [AllowAnonymous]
        // GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Track/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.TrackGetByIdWithDetail(id.GetValueOrDefault());

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
       

        // GET: Track/Edit/5
        [Authorize(Roles = "Coordinator")]
        public ActionResult Edit(int? id)
        {
            var o = m.TrackGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = Mapper.Map<TrackWithDetail, TrackEditForm>(o);

                return View(form);
            }
        }

        // POST: Track/Edit/5
        [HttpPost]
        [Authorize(Roles = "Coordinator")]
        public ActionResult Edit(int? id, TrackEdit newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.Id });
            }

            if (id.GetValueOrDefault() != newItem.Id)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("index");
            }

            // Attempt to do the update
            var editedItem = m.TrackEdit(newItem);

            if (editedItem == null)
            {
                // There was a problem updating the object
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("edit", new { id = newItem.Id });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("details", new { id = newItem.Id });
            }
        }

        [Authorize(Roles = "Coordinator")]
        // GET: Track/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.TrackGetByIdWithDetail(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        [Authorize(Roles = "Coordinator")]
        // POST: Track/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.TrackDelete(id.GetValueOrDefault());

            // "result" will be true or false
            // We probably won't do much with the result, because 
            // we don't want to leak info about the delete attempt

            // In the end, we should just redirect to the list view
            return RedirectToAction("index");
        }
    }
}
