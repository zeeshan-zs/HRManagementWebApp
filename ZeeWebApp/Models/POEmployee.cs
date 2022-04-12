//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZeeWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class POEmployee
    {
        public int Id { get; set; }
        public Nullable<int> EntityID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string EmployeeID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public Nullable<int> NationalityID { get; set; }
        public Nullable<System.DateTime> DateOfJoining { get; set; }
        public Nullable<System.DateTime> LastWorkingDay { get; set; }
        public string Designation { get; set; }
        public string BloodGroup { get; set; }
        public string CompanyTransport { get; set; }
        public string Education { get; set; }
        public string VisaDesignation { get; set; }
        public string PassportNo { get; set; }
        public Nullable<System.DateTime> PassportIssueDt { get; set; }
        public Nullable<System.DateTime> PassportExpiryDt { get; set; }
        public string PassportAtchmt { get; set; }
        public string VisaNo { get; set; }
        public Nullable<System.DateTime> VisaIssueDt { get; set; }
        public Nullable<System.DateTime> VisaExpiryDt { get; set; }
        public string VisaAtchmt { get; set; }
        public string UIDNo { get; set; }
        public string EmiratesIdNo { get; set; }
        public Nullable<System.DateTime> EmiratesIdIssueDt { get; set; }
        public Nullable<System.DateTime> EmiratesIdExpiryDt { get; set; }
        public string EmiratesIdAtchmt { get; set; }
        public string HouseFlatVillaNo { get; set; }
        public string StreetAreaName { get; set; }
        public string NearestLandmark { get; set; }
        public Nullable<int> ResidingEmirateID { get; set; }
        public string EmpMobNo { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyContactNameHmCtry { get; set; }
        public string EmergencyContactNumberHmCtry { get; set; }
        public string Email { get; set; }
        public string AddressHmCtry { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Emirate Emirate { get; set; }
        public virtual Entity Entity { get; set; }
        public virtual Statustbl Statustbl { get; set; }
    }
}