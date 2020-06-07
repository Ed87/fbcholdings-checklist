using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FBChecklist.ViewModels
{
    public class ServiceMonitorViewModel
    {
        public ServiceMonitorViewModel(ServiceMonitor monitor)
        {

            ServerId = monitor.ServerId;
            Id = monitor.Id;
            Status = monitor.Status;
            ServiceId= monitor.ServiceId;
            ApplicationId = monitor.ApplicationId;
            RunDate = monitor.RunDate;
            CreatedBy = monitor.CreatedBy;
        }



        public ServiceMonitorViewModel()
        {

        }


        public int Id { get; set; }
        public int? ApplicationId { get; set; }
        public int? ServiceId { get; set; }
        public string Status { get; set; }
        public DateTime? RunDate { get; set; }
        public int? ServerId { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> Applications { get; internal set; }
        public void UpdateModel(ServiceMonitor monitor)
        {
            monitor.Id = Id;
            monitor.ApplicationId = ApplicationId;
            monitor.ServerId = ServerId;
            monitor.ServiceId = ServiceId;
            monitor.Status = Status;
            monitor.RunDate = RunDate;
            monitor.CreatedBy = CreatedBy;
        }
    }
}