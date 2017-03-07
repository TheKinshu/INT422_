using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class ArtistController : Controller
    {
        private Manager m = new Manager();
        // GET: Album
        public ActionResult Index()
        {
            var o = m.ArtistGetAll();
            return View(o);
        }

        // GET: Album/Details/5
        public ActionResult Details(int id)
        {
            var r = m.ArtistGetById(id);

            if (r == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(r);
            }
        }
    }
}
