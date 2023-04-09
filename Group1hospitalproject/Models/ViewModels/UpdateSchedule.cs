using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group1hospitalproject.Models.ViewModels
{
    public class UpdateSchedule
    {
        // this view model is a class which stores info we need to present to /parkingschedule/update/{id}
        // the existing schedule info

        public ParkingScheduleDto SelectedSchedule { get; set; }



        // also like to include all spots for updating schedule

        public IEnumerable<ParkingSpotDto> SpotOptions { get; set; }

        // also like to include all cars for updating schedule
        public IEnumerable<ParkingCarDto> CarOptions { get; set; }
    }
}