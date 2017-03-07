using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
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

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            var o = m.EmployeeGetById(id);

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = AutoMapper.Mapper.Map<EmployeeBase, EmployeeEditForm>(o);

                return View(editForm);
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployeeEdit collection)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("edit", new { id = collection.EmployeeId});
            }
            if (id != collection.EmployeeId)
            {
                return RedirectToAction("index");
            }

            var editedItem = m.EmployeeEdit(collection);

            if (editedItem == null)
            {
                return RedirectToAction("edit", new { id = collection.EmployeeId });
            }
            else
            {
                return RedirectToAction("index");
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
