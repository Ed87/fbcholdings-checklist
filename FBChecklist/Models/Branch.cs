using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBChecklist.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public Nullable<int> BranchCode { get; set; }
        public Nullable<int> TimeLevel { get; set; }
        public string EOCStage { get; set; }
        public Nullable<System.DateTime> RunDate { get; set; }
        public Nullable<int> ApplicationId { get; set; }
    }
}