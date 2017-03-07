using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            var l = m.TrackGetAll();
            return View(l);
        }

        // GET: Tracks/Edit/5
        public ActionResult PopTracks()
        {
            var l = m.TrackGetAllPop();
            return View(l);
        }

        // POST: Tracks/Edit/5
        public ActionResult DeepPurple()
        {
            var l = m.TrackGetAllDeepPurple();
            return View(l);
        }

        // GET: Tracks/Delete/5
        public ActionResult LongestTracks()
        {
            var l = m.TrackGetAllTop100Longest();
            return View(l);
        }
    }
}
