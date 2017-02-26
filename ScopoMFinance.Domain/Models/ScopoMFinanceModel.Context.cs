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
    
        public virtual DbSet<AccDayCloseProcess> AccDayCloseProcesses { get; set; }
        public virtual DbSet<AccDayCloseProgress> AccDayCloseProgresses { get; set; }
        public virtual DbSet<AccDayOpenClose> AccDayOpenCloses { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<ComponentType> ComponentTypes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<GlobalPolicy> GlobalPolicies { get; set; }
        public virtual DbSet<LoanProduct> LoanProducts { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrgCategory> OrgCategories { get; set; }
        public virtual DbSet<SavingsProduct> SavingsProducts { get; set; }
        public virtual DbSet<SysColcOption> SysColcOptions { get; set; }
        public virtual DbSet<SysDistrict> SysDistricts { get; set; }
        public virtual DbSet<SysDivision> SysDivisions { get; set; }
        public virtual DbSet<SysDonor> SysDonors { get; set; }
        public virtual DbSet<SysGender> SysGenders { get; set; }
        public virtual DbSet<SysThana> SysThanas { get; set; }
        public virtual DbSet<SysUnion> SysUnions { get; set; }
        public virtual DbSet<SysUpazila> SysUpazilas { get; set; }
        public virtual DbSet<SysVillage> SysVillages { get; set; }
        public virtual DbSet<UserBranch> UserBranches { get; set; }
        public virtual DbSet<UserLoginAudit> UserLoginAudits { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<OrgCreditOfficer> OrgCreditOfficers { get; set; }
    }
}
