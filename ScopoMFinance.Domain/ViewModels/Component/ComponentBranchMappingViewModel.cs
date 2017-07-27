using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Component
{
    public class ComponentBranchMappingViewModel
    {
        [Required]
        [Display(Name = "Component_Branch_DN_Branch", ResourceType = typeof(ComponentStrings))]
        public int BranchId { get; set; }

        public List<MappedComponentViewModel> MappedComponentList { get; set; }
    }

    public class MappedComponentViewModel
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public bool Checked { get; set; }
    }
}
