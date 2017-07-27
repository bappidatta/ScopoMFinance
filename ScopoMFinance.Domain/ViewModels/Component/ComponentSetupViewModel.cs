using ScopoMFinance.Domain.ViewModels.Common;
using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ScopoMFinance.Domain.ViewModels.Component
{
    public class ComponentSetupViewModel : CoreEditViewModel
    {
        [Required]
        [Display(Name = "Component_Setup_DN_Code", ResourceType = typeof(ComponentStrings))]
        [Remote("IsComponentCodeAvailable", "Component", "HO", ErrorMessageResourceName = "Component_Edit_Validation_ComponentCodeUnavailable", ErrorMessageResourceType = typeof(ComponentStrings), AdditionalFields = "Id")]
        public string ComponentCode { get; set; }

        [Required]
        [Display(Name = "Component_Setup_DN_Name", ResourceType = typeof(ComponentStrings))]
        public string Name { get; set; }

        [Display(Name = "Component_Setup_DN_Duration", ResourceType = typeof(ComponentStrings))]
        public Nullable<System.DateTime> Duration { get; set; }

        [Required]
        [Display(Name = "Component_Setup_DN_ComponentTypeName", ResourceType = typeof(ComponentStrings))]
        [Remote("IsComponentTypeActive", "Component", "HO", ErrorMessageResourceName = "Component_Edit_Validation_ComponentTypeInactive", ErrorMessageResourceType = typeof(ComponentStrings), AdditionalFields = "Id")]
        public int ComponentTypeId { get; set; }

        [Display(Name = "Component_Setup_DN_DonorName", ResourceType = typeof(ComponentStrings))]
        public Nullable<int> DonorId { get; set; }
    }
}
