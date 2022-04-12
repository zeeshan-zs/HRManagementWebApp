using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeeWebApp.Models
{
    public class DepartmentTotals
    {
        [Key]
        public string Name { get; set; }
        public int Total { get; set; }
    }
}