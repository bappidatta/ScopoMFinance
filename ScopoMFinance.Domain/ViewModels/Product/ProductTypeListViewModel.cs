using ScopoMFinance.Domain.ViewModels.Common;
using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Product
{
    public class ProductTypeListViewModel : CoreListViewModel
    {
        [Display(Name = "Product_Type_List_DN_Name", ResourceType = typeof(ProductStrings))]
        public string Name { get; set; }
    }
}
