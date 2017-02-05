﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScopoMFinance.Domain.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ScopoMFinanceEntities : DbContext
    {
        public ScopoMFinanceEntities()
            : base("name=ScopoMFinanceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<BranchWiseProjectMapping> BranchWiseProjectMappings { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrgCategory> OrgCategories { get; set; }
        public virtual DbSet<OrgCreditOfficer> OrgCreditOfficers { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectType> ProjectTypes { get; set; }
        public virtual DbSet<SysColcOption> SysColcOptions { get; set; }
        public virtual DbSet<SysDistrict> SysDistricts { get; set; }
        public virtual DbSet<SysDivision> SysDivisions { get; set; }
        public virtual DbSet<SysDonor> SysDonors { get; set; }
        public virtual DbSet<SysGender> SysGenders { get; set; }
        public virtual DbSet<SysThana> SysThanas { get; set; }
        public virtual DbSet<SysUnion> SysUnions { get; set; }
        public virtual DbSet<SysUpazila> SysUpazilas { get; set; }
        public virtual DbSet<SysVillage> SysVillages { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
    }
}
