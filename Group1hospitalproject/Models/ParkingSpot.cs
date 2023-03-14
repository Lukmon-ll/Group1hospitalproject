using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Group1hospitalproject.Models
{
    public class ParkingSpot
    {
        [Key]
        public int ParkingSpotID { get; set; }
        public int SpotNumber { get; set; }

    }
}