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
using ZeeWebApp.ViewModels;
using OfficeOpenXml;
using System.Configuration;
using System.IO;

namespace ZeeWebApp.Controllers
{
    [CustomErrorHandling]
    public class POEmployeesControllerOLD : Controller
    {
        private MVC5DBContext db = new MVC5DBContext();

        // GET: Employees
        public ActionResult Index(string searchBy, string searchText, int? page, string sortBy)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name Desc" : "";
            ViewBag.SortPassportNoParameter = sortBy == "PassportNo" ? "PassportNo Desc" : "PassportNo";

            var poEmployees = db.POEmployees.AsQueryable();

            if (searchBy == "PassportNo")
            {

                poEmployees = poEmployees.Where(x => x.PassportNo.Trim().StartsWith(searchText) || searchText == null);
            }
            else
            {

                poEmployees = poEmployees.Where(x => x.FirstName.Trim().StartsWith(searchText) || searchText == null);
            }

            switch (sortBy)
            {
                case "Name Desc":
                    poEmployees = poEmployees.OrderByDescending(x => x.FirstName);
                    break;
                case "PassportNo Desc":
                    poEmployees = poEmployees.OrderByDescending(x => x.PassportNo);
                    break;
                case "PassportNo":
                    poEmployees = poEmployees.OrderBy(x => x.PassportNo);
                    break;
                default:
                    poEmployees = poEmployees.OrderBy(x => x.FirstName);
                    break;
            }

            ViewBag.EmpCreateSuccess = TempData["EmpCreateSuccess"] as bool?;
            ViewBag.EmpUpdateSuccess = TempData["EmpUpdateSuccess"] as bool?;

            return View(poEmployees.ToPagedList(page ?? 1, 5));

        }

        //MULTIPLE DELETE OPTION
        //[HttpPost]
        //public ActionResult DeleteFromListofEmps(IEnumerable<int> employeeIdsToDelete)
        //{
        //    db.POEmployees.Where(x => employeeIdsToDelete.Contains(x.Id))
        //        .ToList()
        //        .ForEach(y => db.POEmployees.Remove(y));
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POEmployee poEmployee = db.POEmployees.Find(id);
            if (poEmployee == null)
            {
                return HttpNotFound();
            }
            return View(poEmployee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            POEmployeeCreateViewModel poEmpVM = new POEmployeeCreateViewModel();
            poEmpVM.StatusItems = new SelectList(db.Statustbls, "Id", "StatusVal");
            poEmpVM.ResidingEmirateItems = new SelectList(db.Emirates, "Id", "EmirateName");
            poEmpVM.NationalityItems = new SelectList(db.Countries, "Id", "Nationality");
            poEmpVM.EntityItems = new SelectList(db.Entities, "Id", "EntityName");

            return View(poEmpVM);
        }

        //[HttpPost]
        //public ActionResult Create(POEmployeeCreateViewModel poEmployee)
        //{
        //    var test = poEmployee.Title;
        //    return RedirectToAction("Index");
        //}

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult CreateTst(POEmployeeCreateViewModel poEmployeee)
        //{
        //    //POEmployeeCreateViewModel poEmpVM = new POEmployeeCreateViewModel();
        //    poEmployeee.StatusItems = new SelectList(db.Statustbls, "Id", "StatusVal");
        //    poEmployeee.ResidingEmirateItems = new SelectList(db.Emirates, "Id", "EmirateName");
        //    poEmployeee.NationalityItems = new SelectList(db.Countries, "Id", "Nationality");
        //    poEmployeee.EntityItems = new SelectList(db.Entities, "Id", "EntityName");
        //    //poEmployeee.PassportAtchmt = "asdasd/rers";

        //    if (ModelState.IsValid)
        //    {
        //        return View("Create");
        //    }
        //    else
        //    {
        //        var errors = ModelState.Select(x => x.Value.Errors)
        //                   .Where(y => y.Count > 0)
        //                   .ToList();

        //        return View();
        //    }
            
        //}
            [HttpPost]
        public ActionResult Create(POEmployeeCreateViewModel poEmployee)
        {
            POEmployee poEmp = new POEmployee(); 

            if (string.IsNullOrEmpty(poEmployee.FirstName))
            {
                ModelState.AddModelError("Name", "The first name is required. Kindly enter a first name.");
            }

            if (ModelState.IsValid)
            {
                //check file uploads
                if (poEmployee.PassportAtchmtObj != null)
                {
                    poEmployee.PassportAtchmt = SaveFileToSystem(poEmployee.PassportAtchmtObj);
                }

                if (poEmployee.VisaAtchmtObj != null)
                {
                    poEmployee.VisaAtchmt = SaveFileToSystem(poEmployee.VisaAtchmtObj);
                }

                if (poEmployee.EmrtsIdAtchmtObj != null)
                {
                    poEmployee.EmiratesIdAtchmt = SaveFileToSystem(poEmployee.EmrtsIdAtchmtObj);
                }

                //poEmp = poEmployee;
                poEmp.Title = poEmployee.Title;
                poEmp.FirstName = poEmployee.FirstName;
                poEmp.MiddleName = poEmployee.MiddleName;
                poEmp.LastName = poEmployee.LastName;
                poEmp.EntityID = poEmployee.EntityID;
                poEmp.StatusID = poEmployee.StatusID;
                poEmp.EmployeeID = poEmployee.EmployeeID;
                poEmp.Gender = poEmployee.Gender;
                poEmp.MaritalStatus = poEmployee.MaritalStatus;
                poEmp.NationalityID = poEmployee.NationalityID;
                poEmp.DateOfJoining = poEmployee.DateOfJoining;
                poEmp.LastWorkingDay = poEmployee.LastWorkingDay;
                poEmp.Designation = poEmployee.Designation;
                poEmp.BloodGroup = poEmployee.BloodGroup;
                poEmp.CompanyTransport = poEmployee.CompanyTransport;
                poEmp.Education = poEmployee.Education;
                poEmp.VisaDesignation = poEmployee.VisaDesignation;
                poEmp.PassportNo = poEmployee.PassportNo;
                poEmp.PassportIssueDt = poEmployee.PassportIssueDt;
                poEmp.PassportExpiryDt = poEmployee.PassportExpiryDt;
                poEmp.PassportAtchmt = poEmployee.PassportAtchmt;
                poEmp.VisaNo = poEmployee.VisaNo;
                poEmp.VisaIssueDt = poEmployee.VisaIssueDt;
                poEmp.VisaExpiryDt = poEmployee.VisaExpiryDt;
                poEmp.VisaAtchmt = poEmployee.VisaAtchmt;
                poEmp.UIDNo = poEmployee.UIDNo;
                poEmp.EmiratesIdNo = poEmployee.EmiratesIdNo;
                poEmp.EmiratesIdIssueDt = poEmployee.EmiratesIdIssueDt;
                poEmp.EmiratesIdExpiryDt = poEmployee.EmiratesIdExpiryDt;
                poEmp.EmiratesIdAtchmt = poEmployee.EmiratesIdAtchmt;
                poEmp.HouseFlatVillaNo = poEmployee.HouseFlatVillaNo;
                poEmp.StreetAreaName = poEmployee.StreetAreaName;
                poEmp.NearestLandmark = poEmployee.NearestLandmark;
                poEmp.ResidingEmirateID = poEmployee.ResidingEmirateID;
                poEmp.EmpMobNo = poEmployee.EmpMobNo;
                poEmp.EmergencyContactName = poEmployee.EmergencyContactName;
                poEmp.EmergencyContactNumber = poEmployee.EmergencyContactNumber;
                poEmp.EmergencyContactNameHmCtry = poEmployee.EmergencyContactNameHmCtry;
                poEmp.EmergencyContactNumberHmCtry = poEmployee.EmergencyContactNumberHmCtry;
                poEmp.Email = poEmployee.Email;
                poEmp.AddressHmCtry = poEmployee.AddressHmCtry;

                db.POEmployees.Add(poEmp);
                db.SaveChanges();
                TempData["EmpCreateSuccess"] = true;
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            }

            //POEmployeeCreateViewModel poEmpVM = new POEmployeeCreateViewModel();
            poEmployee.StatusItems = new SelectList(db.Statustbls, "Id", "StatusVal", poEmployee.StatusID);
            poEmployee.ResidingEmirateItems = new SelectList(db.Emirates, "Id", "EmirateName", poEmployee.ResidingEmirateID);
            poEmployee.NationalityItems = new SelectList(db.Countries, "Id", "Nationality", poEmployee.NationalityID);
            poEmployee.EntityItems = new SelectList(db.Entities, "Id", "EntityName", poEmployee.EntityID);

            return View(poEmployee);
        }

        // FILE SAVE METHOD
        private string SaveFileToSystem(HttpPostedFileBase fileObj)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileObj.FileName);
            string extension = Path.GetExtension(fileObj.FileName);
            string filename = fileNameWithoutExtension + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + extension;

            string savepath = Server.MapPath("~/UploadedFiles/");
            string fullfilepath = Path.Combine(savepath, filename);
            fileObj.SaveAs(fullfilepath); //save the actual file to the file system

            return "/UploadedFiles/" + filename; //save the URL value to model/db
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POEmployee poemp = new POEmployee();
            POEmployeeEditViewModel poEmpVM = new POEmployeeEditViewModel();

            poemp = db.POEmployees.Find(id);

            poEmpVM.Title = poemp.Title;
            poEmpVM.FirstName = poemp.FirstName;
            poEmpVM.MiddleName = poemp.MiddleName;
            poEmpVM.LastName = poemp.LastName;
            poEmpVM.EntityID = poemp.EntityID;
            poEmpVM.StatusID = poemp.StatusID;
            poEmpVM.EmployeeID = poemp.EmployeeID;
            poEmpVM.Gender = poemp.Gender;
            poEmpVM.MaritalStatus = poemp.MaritalStatus;
            poEmpVM.NationalityID = poemp.NationalityID;
            poEmpVM.DateOfJoining = poemp.DateOfJoining;
            poEmpVM.LastWorkingDay = poemp.LastWorkingDay;
            poEmpVM.Designation = poemp.Designation;
            poEmpVM.BloodGroup = poemp.BloodGroup;
            poEmpVM.CompanyTransport = poemp.CompanyTransport;
            poEmpVM.Education = poemp.Education;
            poEmpVM.VisaDesignation = poemp.VisaDesignation;
            poEmpVM.PassportNo = poemp.PassportNo;
            poEmpVM.PassportIssueDt = poemp.PassportIssueDt;
            poEmpVM.PassportExpiryDt = poemp.PassportExpiryDt;
            poEmpVM.PassportAtchmt = poemp.PassportAtchmt;
            poEmpVM.VisaNo = poemp.VisaNo;
            poEmpVM.VisaIssueDt = poemp.VisaIssueDt;
            poEmpVM.VisaExpiryDt = poemp.VisaExpiryDt;
            poEmpVM.VisaAtchmt = poemp.VisaAtchmt;
            poEmpVM.UIDNo = poemp.UIDNo;
            poEmpVM.EmiratesIdNo = poemp.EmiratesIdNo;
            poEmpVM.EmiratesIdIssueDt = poemp.EmiratesIdIssueDt;
            poEmpVM.EmiratesIdExpiryDt = poemp.EmiratesIdExpiryDt;
            poEmpVM.EmiratesIdAtchmt = poemp.EmiratesIdAtchmt;
            poEmpVM.HouseFlatVillaNo = poemp.HouseFlatVillaNo;
            poEmpVM.StreetAreaName = poemp.StreetAreaName;
            poEmpVM.NearestLandmark = poemp.NearestLandmark;
            poEmpVM.ResidingEmirateID = poemp.ResidingEmirateID;
            poEmpVM.EmpMobNo = poemp.EmpMobNo;
            poEmpVM.EmergencyContactName = poemp.EmergencyContactName;
            poEmpVM.EmergencyContactNumber = poemp.EmergencyContactNumber;
            poEmpVM.EmergencyContactNameHmCtry = poemp.EmergencyContactNameHmCtry;
            poEmpVM.EmergencyContactNumberHmCtry = poemp.EmergencyContactNumberHmCtry;
            poEmpVM.Email = poemp.Email;
            poEmpVM.AddressHmCtry = poemp.AddressHmCtry;

            //populate the dropdownlists with the selected values from employee

            poEmpVM.StatusItems = new SelectList(db.Statustbls, "Id", "StatusVal", poEmpVM.StatusID);
            poEmpVM.ResidingEmirateItems = new SelectList(db.Emirates, "Id", "EmirateName", poEmpVM.ResidingEmirateID);
            poEmpVM.NationalityItems = new SelectList(db.Countries, "Id", "Nationality", poEmpVM.NationalityID);
            poEmpVM.EntityItems = new SelectList(db.Entities, "Id", "EntityName", poEmpVM.EntityID);


            if (poEmpVM == null)
            {
                return HttpNotFound();
            }

            return View(poEmpVM);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(POEmployeeEditViewModel poEmployee)
        {
            POEmployee EmployeeFromDB = db.POEmployees.Single(x => x.Id == poEmployee.Id);

            //check file uploads
            if (poEmployee.PassportAtchmtObj != null)
            {
                EmployeeFromDB.PassportAtchmt = poEmployee.PassportAtchmt = SaveFileToSystem(poEmployee.PassportAtchmtObj);
            }
            else
            {
                poEmployee.PassportAtchmt = EmployeeFromDB.PassportAtchmt;
            }

            if (poEmployee.VisaAtchmtObj != null)
            {
                EmployeeFromDB.VisaAtchmt = poEmployee.VisaAtchmt = SaveFileToSystem(poEmployee.VisaAtchmtObj);
            }
            else
            {
                poEmployee.VisaAtchmt = EmployeeFromDB.VisaAtchmt;
            }

            if (poEmployee.EmrtsIdAtchmtObj != null)
            {
                EmployeeFromDB.EmiratesIdAtchmt = poEmployee.EmiratesIdAtchmt = SaveFileToSystem(poEmployee.EmrtsIdAtchmtObj);
            }
            else
            {
                poEmployee.EmiratesIdAtchmt = EmployeeFromDB.EmiratesIdAtchmt;
            }

            //check file uploads            
            //if (poEmployee.PassportAtchmtObj != null)
            //{
            //    //string uploadedFileName = Path.GetFileName(poEmployee.PassportAtchmtObj.FileName);
                
            //    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(poEmployee.PassportAtchmtObj.FileName);
            //    string extension = Path.GetExtension(poEmployee.PassportAtchmtObj.FileName);
            //    string filename = fileNameWithoutExtension + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + extension;

            //    string savepath = Server.MapPath("~/UploadedFiles/");
            //    string fullfilepath = Path.Combine(savepath, filename);
            //    poEmployee.PassportAtchmtObj.SaveAs(fullfilepath); //save the actual file to the file system

            //    poEmployee.PassportAtchmt = "/UploadedFiles/" + filename; //save the URL value to model/db

            //    EmployeeFromDB.PassportAtchmt = poEmployee.PassportAtchmt;
            //}
            //else
            //{
            //    poEmployee.PassportAtchmt = EmployeeFromDB.PassportAtchmt;
                
            //}

            EmployeeFromDB.EntityID = poEmployee.EntityID;
            EmployeeFromDB.StatusID = poEmployee.StatusID;
            EmployeeFromDB.EmployeeID = poEmployee.EmployeeID;
            EmployeeFromDB.Gender = poEmployee.Gender;
            EmployeeFromDB.MaritalStatus = poEmployee.MaritalStatus;
            EmployeeFromDB.NationalityID = poEmployee.NationalityID;
            EmployeeFromDB.DateOfJoining = poEmployee.DateOfJoining;
            EmployeeFromDB.LastWorkingDay = poEmployee.LastWorkingDay;
            EmployeeFromDB.Designation = poEmployee.Designation;
            EmployeeFromDB.BloodGroup = poEmployee.BloodGroup;
            EmployeeFromDB.CompanyTransport = poEmployee.CompanyTransport;
            EmployeeFromDB.Education = poEmployee.Education;
            EmployeeFromDB.VisaDesignation = poEmployee.VisaDesignation;
            EmployeeFromDB.PassportNo = poEmployee.PassportNo;
            EmployeeFromDB.PassportIssueDt = poEmployee.PassportIssueDt;
            EmployeeFromDB.PassportExpiryDt = poEmployee.PassportExpiryDt;            
            EmployeeFromDB.VisaNo = poEmployee.VisaNo;
            EmployeeFromDB.VisaIssueDt = poEmployee.VisaIssueDt;
            EmployeeFromDB.VisaExpiryDt = poEmployee.VisaExpiryDt;
            EmployeeFromDB.VisaAtchmt = poEmployee.VisaAtchmt;
            EmployeeFromDB.UIDNo = poEmployee.UIDNo;
            EmployeeFromDB.EmiratesIdNo = poEmployee.EmiratesIdNo;
            EmployeeFromDB.EmiratesIdIssueDt = poEmployee.EmiratesIdIssueDt;
            EmployeeFromDB.EmiratesIdExpiryDt = poEmployee.EmiratesIdExpiryDt;
            EmployeeFromDB.EmiratesIdAtchmt = poEmployee.EmiratesIdAtchmt;
            EmployeeFromDB.HouseFlatVillaNo = poEmployee.HouseFlatVillaNo;
            EmployeeFromDB.StreetAreaName = poEmployee.StreetAreaName;
            EmployeeFromDB.NearestLandmark = poEmployee.NearestLandmark;
            EmployeeFromDB.ResidingEmirateID = poEmployee.ResidingEmirateID;
            EmployeeFromDB.EmpMobNo = poEmployee.EmpMobNo;
            EmployeeFromDB.EmergencyContactName = poEmployee.EmergencyContactName;
            EmployeeFromDB.EmergencyContactNumber = poEmployee.EmergencyContactNumber;
            EmployeeFromDB.EmergencyContactNameHmCtry = poEmployee.EmergencyContactNameHmCtry;
            EmployeeFromDB.EmergencyContactNumberHmCtry = poEmployee.EmergencyContactNumberHmCtry;
            EmployeeFromDB.Email = poEmployee.Email;
            EmployeeFromDB.AddressHmCtry = poEmployee.AddressHmCtry;

            // to make modelstate valid
            //poEmployee.Title = EmployeeFromDB.Title;
            //poEmployee.FirstName = EmployeeFromDB.FirstName;
            //poEmployee.MiddleName = EmployeeFromDB.MiddleName;
            //poEmployee.LastName = EmployeeFromDB.LastName;

            //populate the dropdownlists with the selected values from employee            
            poEmployee.StatusItems = new SelectList(db.Statustbls, "Id", "StatusVal", poEmployee.StatusID);
            poEmployee.ResidingEmirateItems = new SelectList(db.Emirates, "Id", "EmirateName", poEmployee.ResidingEmirateID);
            poEmployee.NationalityItems = new SelectList(db.Countries, "Id", "Nationality", poEmployee.NationalityID);
            poEmployee.EntityItems = new SelectList(db.Entities, "Id", "EntityName", poEmployee.EntityID);

            if (ModelState.IsValid)
            {
                db.Entry(EmployeeFromDB).State = EntityState.Modified;
                db.SaveChanges();
                TempData["EmpUpdateSuccess"] = true;
                return RedirectToAction("Index");
            }

            return View(poEmployee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POEmployee poEmployee = db.POEmployees.Find(id);
            if (poEmployee == null)
            {
                return HttpNotFound();
            }
            return View(poEmployee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POEmployee poEmployee = db.POEmployees.Find(id);
            db.POEmployees.Remove(poEmployee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void DownloadExcel()
        {
            // constr = ConfigurationManager.AppSettings["connectionString"];
            //var Client = new MongoClient(constr);
            //var db = Client.GetDatabase("Employee");


            //Id, TextInfo, NationalityId, CountryId, EmpNo, EmpName, RollNo, PassportNo, Residence,
            //IssueDate, ExpiryDate, VisaCancelledDate, DateofBirth, JobNo, EmiratesId, JobName, SRNo,
            //RefundAmtRcvd, VisaDepdRfnd

            
            var poEmployees = db.POEmployees.ToList();

            //var collection = db.GetCollection<EmployeeDetails>("EmployeeDetails").Find(new BsonDocument()).ToList();


            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("All Employees");
            Sheet.Cells["A1"].Value = "EntityID";
            Sheet.Cells["B1"].Value = "StatusID";
            Sheet.Cells["C1"].Value = "EmployeeID";
            Sheet.Cells["D1"].Value = "Title";
            Sheet.Cells["E1"].Value = "FirstName";
            Sheet.Cells["F1"].Value = "MiddleName";
            Sheet.Cells["G1"].Value = "LastName";
            Sheet.Cells["H1"].Value = "Gender";
            Sheet.Cells["I1"].Value = "MaritalStatus";
            Sheet.Cells["J1"].Value = "NationalityID";
            Sheet.Cells["K1"].Value = "DateOfJoining";
            Sheet.Cells["L1"].Value = "LastWorkingDay";
            Sheet.Cells["M1"].Value = "Designation";
            Sheet.Cells["N1"].Value = "BloodGroup";
            Sheet.Cells["O1"].Value = "CompanyTransport";
            Sheet.Cells["P1"].Value = "Education";
            Sheet.Cells["Q1"].Value = "VisaDesignation";
            Sheet.Cells["R1"].Value = "PassportNo";
            Sheet.Cells["S1"].Value = "PassportIssueDt";
            Sheet.Cells["T1"].Value = "PassportExpiryDt";
            Sheet.Cells["U1"].Value = "PassportAtchmt";
            Sheet.Cells["V1"].Value = "VisaNo";
            Sheet.Cells["W1"].Value = "VisaIssueDt";
            Sheet.Cells["X1"].Value = "VisaExpiryDt";
            Sheet.Cells["Y1"].Value = "VisaAtchmt";
            Sheet.Cells["Z1"].Value = "UIDNo";
            Sheet.Cells["AA1"].Value = "EmiratesIdNo";
            Sheet.Cells["AB1"].Value = "EmiratesIdIssueDt";
            Sheet.Cells["AC1"].Value = "EmiratesIdExpiryDt";
            Sheet.Cells["AD1"].Value = "EmiratesIdAtchmt";
            Sheet.Cells["AE1"].Value = "HouseFlatVillaNo";
            Sheet.Cells["AF1"].Value = "StreetAreaName";
            Sheet.Cells["AG1"].Value = "NearestLandmark";
            Sheet.Cells["AH1"].Value = "ResidingEmirateID";
            Sheet.Cells["AI1"].Value = "EmpMobNo";
            Sheet.Cells["AJ1"].Value = "EmergencyContactName";
            Sheet.Cells["AK1"].Value = "EmergencyContactNumber";
            Sheet.Cells["AL1"].Value = "EmergencyContactNameHmCtry";
            Sheet.Cells["AM1"].Value = "EmergencyContactNumberHmCtry";
            Sheet.Cells["AN1"].Value = "Email";
            Sheet.Cells["AO1"].Value = "AddressHmCtry";

            int row = 2;
            foreach (var item in poEmployees)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.Entity.EntityName;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Statustbl.StatusVal;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.EmployeeID;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Title;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.FirstName;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.MiddleName;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.LastName;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.Gender;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.MaritalStatus;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.Country.Nationality;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.DateOfJoining;
                Sheet.Cells[string.Format("L{0}", row)].Value = item.LastWorkingDay;
                Sheet.Cells[string.Format("M{0}", row)].Value = item.Designation;
                Sheet.Cells[string.Format("N{0}", row)].Value = item.BloodGroup;
                Sheet.Cells[string.Format("O{0}", row)].Value = item.CompanyTransport;
                Sheet.Cells[string.Format("P{0}", row)].Value = item.Education;
                Sheet.Cells[string.Format("Q{0}", row)].Value = item.VisaDesignation;
                Sheet.Cells[string.Format("R{0}", row)].Value = item.PassportNo;
                Sheet.Cells[string.Format("S{0}", row)].Value = item.PassportIssueDt;
                Sheet.Cells[string.Format("T{0}", row)].Value = item.PassportExpiryDt;
                Sheet.Cells[string.Format("U{0}", row)].Value = item.PassportAtchmt;
                Sheet.Cells[string.Format("V{0}", row)].Value = item.VisaNo;
                Sheet.Cells[string.Format("W{0}", row)].Value = item.VisaIssueDt;
                Sheet.Cells[string.Format("X{0}", row)].Value = item.VisaExpiryDt;
                Sheet.Cells[string.Format("Y{0}", row)].Value = item.VisaAtchmt;
                Sheet.Cells[string.Format("Z{0}", row)].Value = item.UIDNo;
                Sheet.Cells[string.Format("AA{0}", row)].Value = item.EmiratesIdNo;
                Sheet.Cells[string.Format("AB{0}", row)].Value = item.EmiratesIdIssueDt;
                Sheet.Cells[string.Format("AC{0}", row)].Value = item.EmiratesIdExpiryDt;
                Sheet.Cells[string.Format("AD{0}", row)].Value = item.EmiratesIdAtchmt;
                Sheet.Cells[string.Format("AE{0}", row)].Value = item.HouseFlatVillaNo;
                Sheet.Cells[string.Format("AF{0}", row)].Value = item.StreetAreaName;
                Sheet.Cells[string.Format("AG{0}", row)].Value = item.NearestLandmark;
                Sheet.Cells[string.Format("AH{0}", row)].Value = item.ResidingEmirateID;
                Sheet.Cells[string.Format("AI{0}", row)].Value = item.EmpMobNo;
                Sheet.Cells[string.Format("AJ{0}", row)].Value = item.EmergencyContactName;
                Sheet.Cells[string.Format("AK{0}", row)].Value = item.EmergencyContactNumber;
                Sheet.Cells[string.Format("AL{0}", row)].Value = item.EmergencyContactNameHmCtry;
                Sheet.Cells[string.Format("AM{0}", row)].Value = item.EmergencyContactNumberHmCtry;
                Sheet.Cells[string.Format("AN{0}", row)].Value = item.Email;
                Sheet.Cells[string.Format("AO{0}", row)].Value = item.AddressHmCtry;
                row++;
            }

            string excelFN = "PlanetOneEmployeesList" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + ".xlsx";
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();            
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + excelFN);
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
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