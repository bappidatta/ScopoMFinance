using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Policy
{
    public class BranchEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Branch_List_DN_BranchName", ResourceType = typeof(BranchStrings))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Branch_List_DN_BranchOpenDate", ResourceType = typeof(BranchStrings))]
        public DateTime OpenDate { get; set; }

        [Display(Name = "Branch_List_DN_HeadOffice", ResourceType = typeof(BranchStrings))]
        public bool IsHeadOffice { get; set; }

        [Display(Name = "Branch_List_DN_BranchStatus", ResourceType = typeof(BranchStrings))]
        public bool Status { get; set; }
    }
}
