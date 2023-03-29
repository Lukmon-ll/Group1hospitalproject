using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group1hospitalproject.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Patients")]
        public int PatientID { get; set; }
        public virtual Patients Patients { get; set; }

        [ForeignKey("Doctors")]
        public int DoctorID { get; set; }
        public virtual Doctor Doctors { get; set; }

        public DateTime AppointDate { get; set; }


    }

    public class AppointmentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string DoctorName { get; set; }
        public DateTime AppointDate { get; set; }


    }
}