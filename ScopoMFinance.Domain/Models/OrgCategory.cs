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
    
    public partial class OrgCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrgCategory()
        {
            this.Organizations = new HashSet<Organization>();
        }
    
        public int Id { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public System.DateTime SystemDate { get; set; }
        public System.DateTime SetDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
