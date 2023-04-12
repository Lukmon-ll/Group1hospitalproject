using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group1hospitalproject.Models.ViewModels
{
    public class DetailsCar
    {
        public ParkingCarDto selectedParkingCar { get; set; }
        public IEnumerable<ParkingScheduleDto> RelatedSchedules { get; set; }
    }
}