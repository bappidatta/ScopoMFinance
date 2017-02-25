//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Organizations = new HashSet<Organization>();
            this.OrgCreditOfficers = new HashSet<OrgCreditOfficer>();
        }
    
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public bool IsCreditOfficer { get; set; }
        public System.DateTime JoiningDate { get; set; }
        public Nullable<System.DateTime> ResignDate { get; set; }
        public int GenderId { get; set; }
        public int EmployeeTypeId { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public System.DateTime SystemDate { get; set; }
        public System.DateTime SetDate { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public virtual SysGender SysGender { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Organization> Organizations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrgCreditOfficer> OrgCreditOfficers { get; set; }
    }
}
