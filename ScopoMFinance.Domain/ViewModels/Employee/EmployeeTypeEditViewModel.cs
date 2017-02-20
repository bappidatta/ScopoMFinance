using ScopoMFinance.Domain.Models;
using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ScopoMFinance.Domain.ViewModels.Employee
{
    public class EmployeeTypeEditViewModel
    {
        [UIHint("Hidden")]
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [Display(Name = "EmployeeType_Edit_DN_Name", ResourceType = typeof(EmployeeTypeStrings))]
        public string Name { get; set; }

        [Display(Name = "EmployeeType_Edit_DN_IsActive", ResourceType = typeof(EmployeeTypeStrings))]
        public bool IsActive { get; set; }

        [UIHint("Hidden")]
        [HiddenInput]
        public bool IsDeleted { get; set; }

        [UIHint("Hidden")]
        [HiddenInput]
        public string CreatedBy { get; set; }

        [UIHint("Hidden")]
        [HiddenInput]
        public System.DateTime CreatedOn { get; set; }

        [UIHint("Hidden")]
        [HiddenInput]
        public string UpdatedBy { get; set; }

        [UIHint("Hidden")]
        [HiddenInput]
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
