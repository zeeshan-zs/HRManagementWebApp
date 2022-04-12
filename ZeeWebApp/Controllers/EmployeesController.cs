using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZeeWebApp.Models;

namespace ZeeWebApp.Controllers
{
    [Authorize(Roles ="Admin, Student, Faculty")]
    public class EmployeesController : Controller
    {
        private MVC5DBContext db = new MVC5DBContext();

        // GET: Employees
        public ActionResult Index(string searchBy, string searchText, int? page, string sortBy)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortGenderParameter = sortBy == "Gender" ? "Gender Desc" : "Gender";

            //var employees = db.Employees.Include(e => e.Department);

            var employees = db.Employees.AsQueryable();

            if (searchBy == "Gender")
            {

                employees = employees.Where(x => x.Gender.Trim() == searchText || searchText == null);
            }
            else
            {

                employees = employees.Where(x => x.Name.Trim().StartsWith(searchText) || searchText == null);
            }

            switch(sortBy)
            {
                case "Name desc":
                    employees = employees.OrderByDescending(x => x.Name);
                    break;
                case "Gender Desc":
                    employees = employees.OrderByDescending(x => x.Gender);
                    break;
                case "Gender":
                    employees = employees.OrderBy(x => x.Gender);
                    break;
                default:
                    employees = employees.OrderBy(x => x.Name);
                    break;
            }

            return View(employees.ToPagedList(page ?? 1, 5));
            
        }

        [HttpPost]
        public ActionResult DeleteFromListofEmps(IEnumerable<int> employeeIdsToDelete)
        {
            db.Employees.Where(x => employeeIdsToDelete.Contains(x.EmployeeID))
                .ToList()
                .ForEach(y => db.Employees.Remove(y));
            db.SaveChanges();
            
           return RedirectToAction("Index");
        }
        public ActionResult EmployeesByDepartment()
        {
            var employees = db.Employees.Include(e => e.Department)
                                .GroupBy(x => x.Department.Name)
                                .Select(y => new DepartmentTotals
                                 {
                                     Name = y.Key,
                                     Total = y.Count()
                                 }).ToList(); ;
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,Name,Gender,City,DepartmentID")] Employee employee)
        {
            if(string.IsNullOrEmpty(employee.Name))
            {
                ModelState.AddModelError("Name","Bruh, the name is requried! Enter a name!");
            }
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "Name", employee.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            employee.Gender = employee.Gender.Trim(); //remove whitespace from the retrieved string
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "Name", employee.DepartmentID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Name")] Employee employee)
        {
            Employee EmployeeFromDB = db.Employees.Single(x => x.EmployeeID == employee.EmployeeID);
            EmployeeFromDB.Gender = employee.Gender;
            EmployeeFromDB.City = employee.City;
            EmployeeFromDB.DepartmentID = employee.DepartmentID;
            employee.Name = EmployeeFromDB.Name;

            if (ModelState.IsValid)
            {
                db.Entry(EmployeeFromDB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "Name", employee.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
