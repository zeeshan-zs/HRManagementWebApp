using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeeWebApp.Models;
namespace ZeeWebApp.ViewModels
{    
    public class POEmployeeCreateViewModel : POEmployee
    {
        //public POEmployee poEmp { get; set; }
        public HttpPostedFileBase PassportAtchmtObj { get; set; }
        public HttpPostedFileBase VisaAtchmtObj { get; set; }
        public HttpPostedFileBase EmrtsIdAtchmtObj { get; set; }
        public SelectList StatusItems { get; set; }
        public SelectList ResidingEmirateItems { get; set; }
        public SelectList NationalityItems { get; set; }
        public SelectList EntityItems { get; set; }

    }
}