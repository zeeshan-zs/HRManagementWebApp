using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZeeWebApp.Models;

namespace ZeeWebApp.ViewModels
{
    public class CreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Entity")]
        public int EntityID { get; set; }

        [Display(Name = "Status")]
        public int StatusID { get; set; }

        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Nationality")]
        public int NationalityID { get; set; }

        [Display(Name = "Date of Joining")]
        public DateTime? DateOfJoining { get; set; }

        [Display(Name = "Last Working Day")]
        public DateTime? LastWorkingDay { get; set; }

        public string Designation { get; set; }

        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "Company Transport")]
        public string CompanyTransport { get; set; }

        public string Education { get; set; }

        [Display(Name = "Visa Designation")]
        public string VisaDesignation { get; set; }

        [Display(Name = "Passport No")]
        public string PassportNo { get; set; }

        [Display(Name = "Passport Issue Date")]
        public DateTime? PassportIssueDt { get; set; }

        [Display(Name = "Passport Expiry Date")]
        public DateTime? PassportExpiryDt { get; set; }

        [Display(Name = "Passport Attachment")]
        public string PassportAtchmt { get; set; }

        [Display(Name = "Visa No")]
        public string VisaNo { get; set; }

        [Display(Name = "Visa Issue Date")]
        public DateTime? VisaIssueDt { get; set; }

        [Display(Name = "Visa Expiry Date")]
        public DateTime? VisaExpiryDt { get; set; }

        [Display(Name = "Visa Attachment")]
        public string VisaAtchmt { get; set; }

        [Display(Name = "UID No")]
        public string UIDNo { get; set; }

        [Display(Name = "Emirates Id No")]
        public string EmiratesIdNo { get; set; }

        [Display(Name = "Emirates Id Issue Date")]
        public DateTime? EmiratesIdIssueDt { get; set; }

        [Display(Name = "Emirates Id Expiry Date")]
        public DateTime? EmiratesIdExpiryDt { get; set; }

        [Display(Name = "Emirates Id Attachment")]
        public string EmiratesIdAtchmt { get; set; }

        [Display(Name = "House/Flat/Villa No")]
        public string HouseFlatVillaNo { get; set; }

        [Display(Name = "Street/Area Name")]
        public string StreetAreaName { get; set; }

        [Display(Name = "Nearest Landmark")]
        public string NearestLandmark { get; set; }

        [Display(Name = "Residing Emirate")]
        public int ResidingEmirateID { get; set; }

        [Display(Name = "Mobile No")]
        public string EmpMobNo { get; set; }

        [Display(Name = "Emergency Contact Name")]
        public string EmergencyContactName { get; set; }

        [Display(Name = "Emergency Contact No")]
        public string EmergencyContactNumber { get; set; }

        [Display(Name = "Emergency Contact Name (Home Country")]
        public string EmergencyContactNameHmCtry { get; set; }

        [Display(Name = "Emergency Contact No (Home Country)")]
        public string EmergencyContactNumberHmCtry { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Address (Home Country)")]
        public string AddressHmCtry { get; set; }

        public HttpPostedFileBase PassportAtchmtPath { get; set; }

        public virtual Country Country { get; set; }
        public virtual Emirate Emirate { get; set; }
        public virtual Entity Entity { get; set; }
        public virtual Statustbl Statustbl { get; set; }
    }
}