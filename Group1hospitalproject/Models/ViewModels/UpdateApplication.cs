using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group1hospitalproject.Models.ViewModels
{
    public class UpdateApplication
    {
        public ApplicationDto SelectedApplication { get; set; }
        public IEnumerable<JobDto> JobOptions { get; set; }
    }
}