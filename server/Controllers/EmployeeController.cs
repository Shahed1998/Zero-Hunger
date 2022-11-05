using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace server.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public ActionResult All(Zero_hungerEntities3 db)
        {
            var employees = db.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Zero_hungerEntities3 db, Employee employee, string Name)
        {
            employee.Name = Name;
            employee.isAvailable = 1;
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("All");
        }

        [HttpGet]
        public ActionResult Details(Zero_hungerEntities3 db, int id)
        {
            var employee = (from row in db.Employees
                            where row.Id == id
                            select row).SingleOrDefault();
            return View(employee);
        }

        [HttpGet]
        public ActionResult Edit(Zero_hungerEntities3 db, int id)
        {
            var employee = (from row in db.Employees
                            where row.Id == id
                            select row).SingleOrDefault();

            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Zero_hungerEntities3 db, Employee employee, string Name, int Id)
        {
            

            var employee2 = (from row in db.Employees
                             where row.Id == Id
                             select row).SingleOrDefault();

            employee.Name = Name;
            employee.isAvailable = employee2.isAvailable;

            db.Entry(employee2).CurrentValues.SetValues(employee);
            db.SaveChanges();
            TempData["editMsg"] = "Employee update successful";
            return RedirectToAction("Edit", new {id = employee2.Id});
        }

        [HttpGet]
        public ActionResult Delete(Zero_hungerEntities3 db, int id)
        {
            var employee = (from row in db.Employees
                            where row.Id == id
                            select row).FirstOrDefault();
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("All");
        }

    }
}