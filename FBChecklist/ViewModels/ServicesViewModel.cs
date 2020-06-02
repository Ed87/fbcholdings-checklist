using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FBChecklist.ViewModels
{
    public class ServicesViewModel
    {
        public ServicesViewModel(Service service)
        {
            ServiceId = service.ServiceId;
            CreatedBy = service.CreatedBy;
            ServerId = service.ServerId;
            ServiceName = service.ServiceName;         
            CreatedOn = service.CreatedOn;
            ShortName = service.ShortName;
        }

        public ServicesViewModel()
        {
        }

        public int ServiceId { get; set; }
        public int? ServerId { get; set; }    
        public string ServiceName { get; set; }       
        public string CreatedBy { get; set; }
        public string ShortName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public IEnumerable<SelectListItem> Servers { get; internal set; }

        public void UpdateModel(Service service)
        {
            service.ServiceId= ServiceId;
            service.ServiceName = ServiceName;
            service.ServerId = ServerId;          
            service.CreatedBy = CreatedBy;
            service.CreatedOn = CreatedOn;
            service.ShortName = ShortName;
        }
    }
}