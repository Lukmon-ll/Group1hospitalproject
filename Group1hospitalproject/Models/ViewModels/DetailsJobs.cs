using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group1hospitalproject.Models.ViewModels
{
    public class DetailsJobs
    {
        public JobDto SelectedJob { get; set; }
        public IEnumerable<ApplicationDto> RelatedApplications { get; set; }
    }
}