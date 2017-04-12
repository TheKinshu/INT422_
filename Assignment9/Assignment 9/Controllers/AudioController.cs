using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_9.Controllers
{
    public class AudioController : Controller
    {
        Manager m = new Manager();
        // GET: Audio
        public ActionResult Index()
        {
            return View();
        }

        [Route("clip/{id}")]

        public ActionResult Details(int? id)
        {
            // Attempt to get the matching 
            var o = m.TrackAudioGetByIdWithDetail(id.GetValueOrDefault());

            if (o == null)
  
            {

                return HttpNotFound();

            }

            else

            {

                // Return a file content result

                // Set the Content-Type header, and return the photo bytes

                return File(o.Audio, o.AudioContentType);

            }

        }
        // GET: Audio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Audio/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Audio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Audio/Edit/5
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

        // GET: Audio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Audio/Delete/5
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
