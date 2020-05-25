using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AzureSQLDemo.Models;

namespace AzureSQLDemo.Controllers
{
    public class HomeController : Controller
    {
        rmgspdbEntities db = new rmgspdbEntities();
        public ActionResult Index()
        {
            List<Employee> empList = db.Employees.ToList();

            return View(empList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            Employee emp = db.Employees.Find(id);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            Employee emp = db.Employees.Find(id);
            return View(emp);
        }

        public ActionResult Delete(int? id)
        {
            Employee emp = db.Employees.Find(id);

            return View(emp);
        }

        [HttpPost]
        public ActionResult Delete(Employee employee)
        {
            Employee emp = db.Employees.Find(employee.EmpId);
            db.Employees.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}