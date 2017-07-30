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
    public class ProductSetupViewModel : CoreEditViewModel
    {
        [Required]
        [Display(Name = "Product_Setup_DN_Code", ResourceType = typeof(ProductStrings))]
        [Remote("IsProductCodeAvailable", "Product", "HO", ErrorMessageResourceName = "Product_Setup_Validation_DuplicateCode", ErrorMessageResourceType = typeof(ProductStrings), AdditionalFields = "Id")]
        public string ProductCode { get; set; }

        [Required]
        [Display(Name = "Product_Setup_DN_Name", ResourceType = typeof(ProductStrings))]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product_Setup_DN_IntRate", ResourceType = typeof(ProductStrings))]
        public decimal InterestRate { get; set; }

        [Required]
        [Display(Name = "Product_Setup_DN_ProductType", ResourceType = typeof(ProductStrings))]
        public int ProductTypeId { get; set; }
    }
}
