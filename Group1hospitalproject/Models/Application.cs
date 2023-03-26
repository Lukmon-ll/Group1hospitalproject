using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group1hospitalproject.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Job Job { get; set; }


        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

    }


    public class ApplicationDto
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; }
    }

}