using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group1hospitalproject.Models.ViewModels
{
    public class DetailsDoctor
    {
        public DoctorDto SelectedDoctor { get; set; }

        public IEnumerable<ParkingCarDto> RelatedCars { get; set; }
        public IEnumerable<ParkingCarDto> RelatedSpots { get; set; }
    }
}