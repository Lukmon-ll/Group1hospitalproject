using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group1hospitalproject.Models
{

    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }

        public string DoctorDescription { get; set; }

        public string DoctorEmail { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
    }
    public class DoctorDto
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }

        public string DoctorDescription { get; set; }

        public string DoctorEmail { get; set; }
        public string DepartmentName { get; set; }
    }
}