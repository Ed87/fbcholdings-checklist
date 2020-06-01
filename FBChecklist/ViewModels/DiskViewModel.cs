using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBChecklist.ViewModels
{
    public class DiskViewModel
    {
        public DiskViewModel(Disk disk)
        {
            DiskId = disk.DiskId;
            RunDate = disk.RunDate;
            VolumeName = disk.VolumeName;
            DeviceId = disk.DeviceId;
            ApplicationId = disk.ApplicationId;
            ServerId = disk.ServerId;
            FreeSpace = disk.FreeSpace;
            UsedSpace = disk.UsedSpace;
            TotalSpace = disk.TotalSpace;
        }

        public DiskViewModel()
        {
        }

        public int DiskId { get; set; }        
        public string Memory { get; set; }
        public string CPU { get; set; }
        public string VolumeName { get; set; }
        public string DeviceId { get; set; }
        public string SystemName { get; set; }
        public decimal? FreeSpace { get; set; }
        public decimal? UsedSpace { get; set; }
        public decimal? TotalSpace { get; set; }
        public DateTime? RunDate { get; set; }
        public int? ServerId { get; set; }
        public int? ApplicationId { get; set; }
        public IEnumerable<SelectListItem> Applications { get; internal set; }
    }
}