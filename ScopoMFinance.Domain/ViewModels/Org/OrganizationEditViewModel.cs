using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Org
{
    public class OrganizationEditViewModel
    {
        public int Id { get; set; }

        public int BranchId { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_OrgNo", ResourceType = typeof(OrganizationStrings))]
        public string OrganizationNo { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_OrgName", ResourceType = typeof(OrganizationStrings))]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_OrgCategory", ResourceType = typeof(OrganizationStrings))]
        public int OrgCategoryId { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_OrgGender", ResourceType = typeof(OrganizationStrings))]
        public int GenderId { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_OrgSetupDate", ResourceType = typeof(OrganizationStrings))]
        public DateTime SetupDate { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_OrgLoanColcOption", ResourceType = typeof(OrganizationStrings))]
        public int LoanColcOptionId { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_OrgSavColcOption", ResourceType = typeof(OrganizationStrings))]
        public int SavColcOptionId { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_OrgFirstLoanColcDate", ResourceType = typeof(OrganizationStrings))]
        public DateTime FirstLoanColcDate { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_OrgFirstSavColcDate", ResourceType = typeof(OrganizationStrings))]
        public DateTime FirstSavColcDate { get; set; }

        [Display(Name = "Organization_Edit_DN_OrgVillage", ResourceType = typeof(OrganizationStrings))]
        public Nullable<int> VillageId { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
    }
}
