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
    
    public partial class Solaris
    {
        public int Id { get; set; }
        public string Filesystem { get; set; }
        public Nullable<decimal> Blocks { get; set; }
        public Nullable<decimal> Used { get; set; }
        public Nullable<decimal> Available { get; set; }
        public string Capacity { get; set; }
        public string Mount { get; set; }
        public Nullable<System.DateTime> RunDate { get; set; }
        public Nullable<int> ApplicationId { get; set; }
    
        public virtual Application Application { get; set; }
    }
}
