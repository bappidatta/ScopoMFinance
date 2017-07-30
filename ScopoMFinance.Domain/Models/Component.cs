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
    
    public partial class Component
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Component()
        {
            this.Members = new HashSet<Member>();
            this.Branches = new HashSet<Branch>();
            this.Products = new HashSet<Product>();
        }
    
        public int Id { get; set; }
        public string ComponentCode { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Duration { get; set; }
        public int ComponentTypeId { get; set; }
        public Nullable<int> DonorId { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public System.DateTime SystemDate { get; set; }
        public System.DateTime SetDate { get; set; }
    
        public virtual ComponentType ComponentType { get; set; }
        public virtual SysDonor SysDonor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Branch> Branches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
