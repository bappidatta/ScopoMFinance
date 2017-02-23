using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Org
{
    public class OrganizationListViewModel
    {
        public int Id { get; set; }
        public int BranchId { get; set; }

        [Display(Name = "Organization_List_DN_OrgNo", ResourceType = typeof(OrganizationStrings))]
        public string OrganizationNo { get; set; }

        [Display(Name = "Organization_List_DN_OrgName", ResourceType = typeof(OrganizationStrings))]
        public string OrganizationName { get; set; }

        public int OrgCategoryId { get; set; }

        [Display(Name = "Organization_List_DN_OrgCategory", ResourceType = typeof(OrganizationStrings))]
        public string OrgCategoryName { get; set; }

        public int GenderId { get; set; }

        [Display(Name = "Organization_List_DN_OrgGender", ResourceType = typeof(OrganizationStrings))]
        public string Gender { get; set; }

        [Display(Name = "Organization_List_DN_OrgSetupDate", ResourceType = typeof(OrganizationStrings))]
        public DateTime SetupDate { get; set; }

        public int MeetingFrequencyId { get; set; }

        [Display(Name = "Organization_List_DN_MeetingFrequency", ResourceType = typeof(OrganizationStrings))]
        public string MeetingFrequency { get; set; }

        [Display(Name = "Organization_List_DN_MeetingDate", ResourceType = typeof(OrganizationStrings))]
        public System.DateTime MeetingDate { get; set; }

        public Nullable<int> VillageId { get; set; }
        public string Village { get; set; }

        [Display(Name = "Organization_List_DN_IsActive", ResourceType = typeof(OrganizationStrings))]
        public bool IsActive { get; set; }

        public string UserId { get; set; }
        public System.DateTime SystemDate { get; set; }
        public System.DateTime SetDate { get; set; }
    }
}
