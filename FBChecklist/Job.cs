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
    
    public partial class Job
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> FrequencyId { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public Nullable<int> IsActive { get; set; }
    
        public virtual Frequency Frequency { get; set; }
    }
}