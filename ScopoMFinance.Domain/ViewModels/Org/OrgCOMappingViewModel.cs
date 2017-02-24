using ScopoMFinance.Domain.ViewModels.Common;
using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Org
{
    public class OrgCOMappingViewModel
    {
        [Required]
        [Display(Name = "Organization_CO_DN_CreditOfficer", ResourceType = typeof(OrganizationStrings))]
        public int CreditOfficerId { get; set; }

        public List<MappedOrganizationViewModel> MappedOrganizationList { get; set; }
    }

    public class MappedOrganizationViewModel
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public bool Checked { get; set; }
    }
}
