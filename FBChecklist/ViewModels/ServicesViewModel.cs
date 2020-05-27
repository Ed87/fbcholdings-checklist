using System;
using System.Collections.Generic;

namespace FBChecklist.ViewModels
{
    public class ServicesViewModel
    {
        public ServicesViewModel(Service service)
        {
            ServiceId = service.ServiceId;
            CreatedBy = service.CreatedBy;
            Url = service.Url;
            ServiceName = service.ServiceName;
            IsActive = service.IsActive;
            CreatedOn = service.CreatedOn;
        }

        public ServicesViewModel()
        {
        }

        public int ServiceId { get; set; }
        public int? IsActive { get; set; }    
        public string ServiceName { get; set; }
        public string Url { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
       
        public void UpdateModel(Service service)
        {
            service.ServiceId= ServiceId;
            service.ServiceName = ServiceName;
            service.IsActive = IsActive;
            service.Url = Url;
            service.CreatedBy = CreatedBy;
            service.CreatedOn = CreatedOn;
        }
    }
}