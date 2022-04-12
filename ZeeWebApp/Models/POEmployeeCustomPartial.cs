using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeeWebApp.Models
{
    [MetadataType(typeof(POEmployeeMetaData))]
    public partial class POEmployee
    {
        //public HttpPostedFileBase PassportAtchmtObj { get; set; }
        //public HttpPostedFileBase VisaAtchmtObj { get; set; }
        //public HttpPostedFileBase EmiratesIdAtchmtObj { get; set; }
    }

    public class POEmployeeMetaData
    {
        [Required]
        [Display(Name = "Entity")]
        public int EntityID { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusID { get; set; }

        [Required]
        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }
        
        [Required]
        [MaxLength(15,ErrorMessage ="Title cannot be longer than 5 characters!")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(15, ErrorMessage = "First name canot exceed 50 characters!")]
        [Required]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public int NationalityID { get; set; }

        [Required]
        [Display(Name = "Date of Joining")]
        public DateTime? DateOfJoining { get; set; }

        [Required]
        [Display(Name = "Last Working Day")]
        public DateTime? LastWorkingDay { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Required]
        [Display(Name = "Company Transport")]
        public string CompanyTransport { get; set; }

        [Required]
        public string Education { get; set; }

        [Required]
        [Display(Name = "Visa Designation")]
        public string VisaDesignation { get; set; }

        [Required]
        [Display(Name = "Passport No")]
        public string PassportNo { get; set; }

        [Required]
        [Display(Name = "Passport Issue Date")]
        public DateTime? PassportIssueDt { get; set; }

        [Required]
        [Display(Name = "Passport Expiry Date")]
        public DateTime? PassportExpiryDt { get; set; }

        //[Required]
        [Display(Name = "Passport Attachment")]
        public string PassportAtchmt { get; set; }

        [Required]
        [Display(Name = "Visa No")]
        public string VisaNo { get; set; }

        [Required]
        [Display(Name = "Visa Issue Date")]
        public DateTime? VisaIssueDt { get; set; }

        [Required]
        [Display(Name = "Visa Expiry Date")]
        public DateTime? VisaExpiryDt { get; set; }

        //[Required]
        [Display(Name = "Visa Attachment")]
        public string VisaAtchmt { get; set; }

        [Required]
        [Display(Name = "UID No")]
        public string UIDNo { get; set; }

        [Required]
        [Display(Name = "Emirates Id No")]
        public string EmiratesIdNo { get; set; }

        [Required]
        [Display(Name = "Emirates Id Issue Date")]
        public DateTime? EmiratesIdIssueDt { get; set; }

        [Required]
        [Display(Name = "Emirates Id Expiry Date")]
        public DateTime? EmiratesIdExpiryDt { get; set; }

        //[Required]
        [Display(Name = "Emirates Id Attachment")]
        public string EmiratesIdAtchmt { get; set; }

        [Required]
        [Display(Name = "House/Flat/Villa No")]
        public string HouseFlatVillaNo { get; set; }

        [Required]
        [Display(Name = "Street/Area Name")]
        public string StreetAreaName { get; set; }

        [Required]
        [Display(Name = "Nearest Landmark")]
        public string NearestLandmark { get; set; }

        [Required]
        [Display(Name = "Residing Emirate")]
        public int ResidingEmirateID { get; set; }

        [Required]
        [Display(Name = "Mobile No")]
        public string EmpMobNo { get; set; }

        [Required]
        [Display(Name = "Emergency Contact Name")]
        public string EmergencyContactName { get; set; }

        [Required]
        [Display(Name = "Emergency Contact No")]
        public string EmergencyContactNumber { get; set; }

        [Required]
        [Display(Name = "Emergency Contact Name (Home Country")]
        public string EmergencyContactNameHmCtry { get; set; }

        [Required]
        [Display(Name = "Emergency Contact No (Home Country)")]
        public string EmergencyContactNumberHmCtry { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Address (Home Country)")]
        public string AddressHmCtry { get; set; }
    }

    /* 
     
        Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        EntityID int,
        StatusID int,
        EmployeeID VARCHAR(50),
        Title VARCHAR(10),
        FirstName VARCHAR(50),
        MiddleName VARCHAR(50),
        LastName VARCHAR(50),
        Gender VARCHAR(10),
        MaritalStatus VARCHAR(15),
        NationalityID int,
        DateOfJoining DATE,
        LastWorkingDay DATE,
        Designation VARCHAR(50),
        BloodGroup VARCHAR(50),
        CompanyTransport VARCHAR(50),
        Education VARCHAR(50),
        VisaDesignation VARCHAR(50),
        PassportNo VARCHAR(50),
        PassportIssueDt DATE,
        PassportExpiryDt DATE,
        PassportAtchmt VARCHAR(50),
        VisaNo VARCHAR(50),
        VisaIssueDt DATE,
        VisaExpiryDt DATE,
        VisaAtchmt VARCHAR(50),
        UIDNo VARCHAR(50),
        EmiratesIdNo VARCHAR(50),
        EmiratesIdIssueDt DATE,
        EmiratesIdExpiryDt DATE,
        EmiratesIdAtchmt VARCHAR(50),
        HouseFlatVillaNo VARCHAR(50),
        StreetAreaName VARCHAR(50),
        NearestLandmark VARCHAR(50),
        ResidingEmirateID int,
        EmpMobNo VARCHAR(50),
        EmergencyContactName VARCHAR(50),
        EmergencyContactNumber VARCHAR(50),
        EmergencyContactNameHmCtry VARCHAR(50),
        EmergencyContactNumberHmCtry VARCHAR(50),
        Email VARCHAR(50),
        AddressHmCtry VARCHAR(50),
     
     
     */
}