﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Server
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MediCppEntities1 : DbContext
    {
        public MediCppEntities1()
            : base("name=MediCppEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<IllnessHistory> IllnessHistories { get; set; }
        public virtual DbSet<IllnessHistoryHasMedicine> IllnessHistoryHasMedicines { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Pacient> Pacients { get; set; }
        public virtual DbSet<PacientHasIllnesHistory> PacientHasIllnesHistories { get; set; }
    }
}
