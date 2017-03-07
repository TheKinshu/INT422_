using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class MediaTypeController : Controller
    {
        private Manager m = new Manager();
        // GET: Album
        public ActionResult Index()
        {
            var o = m.MediaTypeGetAll();
            return View(o);
        }

        // GET: Album/Details/5
        public ActionResult Details(int id)
        {
            var r = m.MediaTypeGetById(id);

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
