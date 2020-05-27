using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBChecklist.ViewModels
{
    public class JobViewModel
    {
        public JobViewModel(Job job)
        {
            JobId = job.JobId;
            CreatedBy = job.CreatedBy;
            JobName = job.JobName;
            FrequencyId = job.FrequencyId;                  
            CreatedOn = job.CreatedOn;
            Url = job.Url;
            Path = job.Path;
            IsActive = job.IsActive;
        }

        public JobViewModel()
        {

        }

        public int JobId { get; set; }            
        public string JobName { get; set; }
        public string Path { get; set; }
        public string CreatedBy { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? IsActive { get; set; }
        public int? FrequencyId { get; set; }
        public IEnumerable<SelectListItem> Frequencies { get; internal set; }

        public void UpdateModel(Job job)
        {
          
            job.CreatedBy = CreatedBy;
            job.JobName = JobName;
            job.Path = Path;
            job.Url = Url;
            job.FrequencyId = FrequencyId;
            job.CreatedOn = CreatedOn;
            job.IsActive = IsActive;

        }
    }
}