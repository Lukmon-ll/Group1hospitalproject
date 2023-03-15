using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Group1hospitalproject.Models
{
    public class ParkingCar
    {
        [Key]
        public int ParkingCarID { get; set; }

        public string LicencePlate { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

    }
}