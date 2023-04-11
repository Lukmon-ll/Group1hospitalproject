using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group1hospitalproject.Models.ViewModels
{
    public class DetailsSpot
    {
        public ParkingSpotDto selectedParkingSpot { get; set; }
        public IEnumerable<DoctorDto> RelatedDoctors { get; set; }
    }
}