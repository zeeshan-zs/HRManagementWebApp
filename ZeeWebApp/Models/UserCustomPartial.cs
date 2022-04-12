using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ZeeWebApp.Models
{
    [MetadataType(typeof(UserDepartmentMetaData))]

    public partial class User
    {
    }

    public class UserDepartmentMetaData
    {
        [Display(Name = "Username")]
        public string username { get; set; }

        [Display(Name = "Password")]
        public string pwd { get; set; }
    }
}