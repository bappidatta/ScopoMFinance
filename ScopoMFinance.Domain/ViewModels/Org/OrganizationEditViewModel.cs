using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ScopoMFinance.Domain.ViewModels.Org
{
    public class OrganizationEditViewModel
    {
        public int Id { get; set; }

        public int BranchId { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_OrgNo", ResourceType = typeof(OrganizationStrings))]
        [Remote("IsOrgNoAvailable", "Organization", "Branch", ErrorMessageResourceName = "Organization_Edit_Validation_OrgNoUnavailable", ErrorMessageResourceType = typeof(OrganizationStrings), AdditionalFields = "Id")]
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
        [Display(Name = "Organization_Edit_DN_MeetingFrequency", ResourceType = typeof(OrganizationStrings))]
        public int MeetingFrequencyId { get; set; }

        [Required]
        [Display(Name = "Organization_Edit_DN_MeetingDate", ResourceType = typeof(OrganizationStrings))]
        public DateTime MeetingDate { get; set; }

        [Display(Name = "Organization_Edit_DN_OrgVillage", ResourceType = typeof(OrganizationStrings))]
        public Nullable<int> VillageId { get; set; }

        [Display(Name = "Organization_Edit_DN_IsActive", ResourceType = typeof(OrganizationStrings))]
        public bool IsActive { get; set; }

        public string UserId { get; set; }
        public System.DateTime SystemDate { get; set; }
        public System.DateTime SetDate { get; set; }
    }
}
