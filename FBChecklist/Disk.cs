//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FBChecklist
{
    using System;
    using System.Collections.Generic;
    
    public partial class Disk
    {
        public int DiskId { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public Nullable<int> ServerId { get; set; }
        public Nullable<decimal> FreeSpace { get; set; }
        public Nullable<decimal> UsedSpace { get; set; }
        public Nullable<decimal> TotalSpace { get; set; }
        public string DiskName { get; set; }
        public Nullable<System.DateTime> RunDate { get; set; }
        public string Memory { get; set; }
        public string CPU { get; set; }
        public string VolumeName { get; set; }
        public string DeviceId { get; set; }
        public string SystemName { get; set; }
        public Nullable<int> PercentageUsed { get; set; }
    
        public virtual Application Application { get; set; }
        public virtual Server Server { get; set; }
    }
}
