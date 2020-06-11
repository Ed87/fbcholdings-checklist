using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBChecklist.ViewModels
{
    public class SolarisViewModel
    {
        public SolarisViewModel(Solaris disk)
        {
            Id = disk.Id;
            RunDate = disk.RunDate;
            Blocks = disk.Blocks;
            Capacity = disk.Capacity;
            ApplicationId = disk.ApplicationId;
            Mount= disk.Mount;
            Filesystem = disk.Filesystem;
            Used = disk.Used;
            Available = disk.Available;
        }

        public SolarisViewModel()
        {

        }

        public int Id { get; set; }
        public string Mount { get; set; }
        public string Capacity { get; set; }
        public string Filesystem { get; set; }
        public decimal? Blocks { get; set; }
        public decimal? Used { get; set; }
        public decimal? Available { get; set; }
        public DateTime? RunDate { get; set; }      
        public int? ApplicationId { get; set; }
        public IEnumerable<SelectListItem> Applications { get; internal set; }
    }
}