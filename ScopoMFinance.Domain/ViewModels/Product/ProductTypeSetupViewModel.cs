using ScopoMFinance.Domain.ViewModels.Common;
using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ScopoMFinance.Domain.ViewModels.Product
{
    public class ProductTypeSetupViewModel : CoreEditViewModel
    {
        [Required]
        [Display(Name = "Product_Type_Setup_DN_Name", ResourceType = typeof(ProductStrings))]
        public string Name { get; set; }
    }
}
