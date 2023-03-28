using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Group1hospitalproject.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }

        public string DepartmentNews { get; set; }
    }
    public class DepartmentDto {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }

        public string DepartmentNews { get; set; }
    }
}