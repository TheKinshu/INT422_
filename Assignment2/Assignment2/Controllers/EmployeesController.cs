using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class EmployeesController : Controller
    {
        private Manager m = new Manager();
        // GET: Employees
        public ActionResult Index()
        {
            var l = m.EmployeeGetAll();
            return View(l);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            var o = m.EmployeeGetById(id);

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View(new EmployeeAdd());
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAdd newItem)
        {
            // TODO: Add insert logic here
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            var o = m.EmployeeAdd(newItem);

            if ( o == null )
            {
                return View(o);
            }
            else
            {
                return RedirectToAction("Details", new { id = o.EmployeeId});
            }

        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
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

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
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
