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
    
    public partial class BizTalk
    {
        public int Id { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public Nullable<int> ServerId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Uri { get; set; }
        public string TransportType { get; set; }
        public string ReceivePort { get; set; }
        public string ReceiveHandler { get; set; }
        public string InstanceName { get; set; }
        public Nullable<System.DateTime> RunDate { get; set; }
    
        public virtual Application Application1 { get; set; }
        public virtual Server Server { get; set; }
    }
}
