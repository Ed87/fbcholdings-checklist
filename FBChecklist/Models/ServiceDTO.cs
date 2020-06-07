using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBChecklist.Models
{
    public class ServiceDTO
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? RunDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ServerId { get; set; }
        public string ShortName { get; set; }
        public int? ApplicationId { get; set; }
        public virtual Service Service { get; set; }
        public virtual Server Server { get; set; }
        public virtual Application Application { get; set; }
    }
}