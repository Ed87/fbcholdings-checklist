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
    
    public partial class ServiceMonitor
    {
        public int Id { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> RunDate { get; set; }
        public Nullable<int> ServerId { get; set; }
        public string CreatedBy { get; set; }
        public string ServiceName { get; set; }
    
        public virtual Application Application { get; set; }
        public virtual Server Server { get; set; }
        public virtual Service Service { get; set; }
    }
}
