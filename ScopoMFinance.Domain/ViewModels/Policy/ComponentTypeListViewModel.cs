using ScopoMFinance.Domain.ViewModels.Common;
using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Policy
{
    public class ComponentTypeListViewModel : CoreListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "ComponentType_List_DN_Name", ResourceType = typeof(ComponentStrings))]
        public string Name { get; set; }

        [Display(Name = "ComponentType_List_DN_NoOfMaxLoan", ResourceType = typeof(ComponentStrings))]
        public int NoOfMaxLoan { get; set; }
    }
}
