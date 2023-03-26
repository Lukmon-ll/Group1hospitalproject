using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group1hospitalproject.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        
        public string JobTitle { get; set; }

        public string Descriptions { get; set; }

        public string Jobtype { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
    }



    public class JobDto
    {
        public int JobId { get; set; }
        public string Descriptions { get; set; }

        public string Jobtype { get; set; }

        public string DepartmentName { get; set; }
    }
}