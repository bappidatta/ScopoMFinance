using ScopoMFinance.Domain.ViewModels.Component;
using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Product
{
    public class ProductComponentMappingViewModel
    {
        [Required]
        [Display(Name = "Product_Component_DN_Product", ResourceType = typeof(ProductStrings))]
        public int ProductId { get; set; }

        public List<MappedComponentViewModel> MappedComponentList { get; set; }
    }
}
