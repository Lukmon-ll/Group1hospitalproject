using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group1hospitalproject.Models.ViewModels
{
    public class UpdateDoctor
    {
        public DoctorDto SelectedDoctor { get; set; }
        public IEnumerable<DepartmentDto> DepartmentOptions { get; set; }
    }
}