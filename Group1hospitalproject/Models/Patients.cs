using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Group1hospitalproject.Models
{
    public class Patients
    {
        [Key]
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public int PhoneNumber { get; set; }
        
        public string Email { get; set; }


    }
}