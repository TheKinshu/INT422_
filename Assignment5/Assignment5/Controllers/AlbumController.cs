using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class AlbumController : Controller
    {
        private Manager m = new Manager();
        // GET: Album
        public ActionResult Index()
        {
            var o = m.AlbumGetAll();
            return View(o);
        }

        // GET: Album/Details/5
        public ActionResult Details(int id)
        {
            var r = m.AlbumGetById(id);

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
