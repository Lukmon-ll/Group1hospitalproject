using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group1hospitalproject.Models
{
    public class ParkingSchedule
    {
        [Key]
        public int ParkingScheduleID { get; set; }

        [ForeignKey("ParkingSpot")]
        public int ParkingSpotID { get; set; }
        public virtual ParkingSpot ParkingSpot { get; set; }

        [ForeignKey("ParkingCar")]
        public int ParkingCarID { get; set; }
        public virtual ParkingCar ParkingCar { get; set; }

        public DateTime DateTimeIn { get; set; }
        public DateTime DateTimeOut { get; set; }


    }
}