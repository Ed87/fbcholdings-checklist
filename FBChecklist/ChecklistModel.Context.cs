﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AppEntities : DbContext
    {
        public AppEntities()
            : base("name=AppEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Frequency> Frequencies { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Scheduler> Schedulers { get; set; }
        public virtual DbSet<DomainController> DomainControllers { get; set; }
        public virtual DbSet<WebLogic> WebLogics { get; set; }
        public virtual DbSet<Health> Healths { get; set; }
    }
}
