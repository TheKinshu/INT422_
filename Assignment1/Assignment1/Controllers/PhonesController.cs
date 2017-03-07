using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class PhonesController : Controller
    {

        // Collection of phones
        private List<PhoneBase> Phones;

        public PhonesController()
        {
            Phones = new List<PhoneBase>();

            Phones.Add(new PhoneBase
            {
                Id = 1,
                PhoneName = "iPhone 8",
                Manufacturer = "Apple",
                DateReleased = new DateTime(2017, 9, 1),
                MSRP = 800,
                ScreenSize = 5.5
            });

            Phones.Add(new PhoneBase
            {
                Id = 3,
                PhoneName = "Galaxy Note 8",
                Manufacturer = "Samsung",
                DateReleased = new DateTime(2017, 8, 1),
                MSRP = 749,
                ScreenSize = 5.7
            });

            Phones.Add(new PhoneBase
            {
                Id = 3,
                PhoneName = "Surface Phone",
                Manufacturer = "Microsoft",
                DateReleased = new DateTime(2017, 3, 1),
                MSRP = 800,
                ScreenSize = 5.5
            });
        }

        // GET: Phones
        public ActionResult Index()
        {
            return View(Phones);
        }

        // GET: Phones/Details/5
        public ActionResult Details(int id)
        {
            return View(Phones[id - 1]);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            return View(new PhoneBase());
        }

        // POST: Phones/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                var newItem = new PhoneBase();

                newItem.Id = Phones.Count + 1;

                newItem.PhoneName = collection["PhoneName"];
                newItem.Manufacturer = collection["Manufacturer"];

                newItem.DateReleased = Convert.ToDateTime(collection["DateReleased"]);


                int msrp = 0;
                double ss = 0;
                bool isNumber;

                isNumber = Int32.TryParse(collection["MSRP"], out msrp);
                newItem.MSRP = msrp;

                isNumber = double.TryParse(collection["ScreenSize"], out ss);
                newItem.ScreenSize = ss;

                Phones.Add(newItem);

                return View("Details", newItem);
            }
            catch
            {
                return View();
            }
        }
        /*
        // GET: Phones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Phones/Edit/5
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

        // GET: Phones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Phones/Delete/5
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
        */
    }
}
