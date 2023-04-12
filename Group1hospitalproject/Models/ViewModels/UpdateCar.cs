using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group1hospitalproject.Models.ViewModels
{
    public class UpdateParkingCar
    {
        // this view model is a class which stores info we need to present tp /parkingcar/update/{id}
        // the existing car info

        public ParkingCarDto SelectedCar { get; set; }

        // also like to include all doctors for updating cars 

        public IEnumerable<DoctorDto> DoctorOptions { get; set; }
    }
}