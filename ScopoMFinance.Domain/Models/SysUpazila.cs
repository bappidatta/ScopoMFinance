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
    
    public partial class SysUpazila
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysUpazila()
        {
            this.SysUnions = new HashSet<SysUnion>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int DistrictId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        public virtual SysDistrict SysDistrict { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysUnion> SysUnions { get; set; }
    }
}
