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
    public class ProductListViewModel : CoreListViewModel
    {
        [Display(Name = "Product_List_DN_Code", ResourceType = typeof(ProductStrings))]
        public string ProductCode { get; set; }

        [Display(Name = "Product_List_DN_Name", ResourceType = typeof(ProductStrings))]
        public string ProductName { get; set; }

        [Display(Name = "Product_List_DN_IntRate", ResourceType = typeof(ProductStrings))]
        public decimal InterestRate { get; set; }

        public int ProductTypeId { get; set; }

        [Display(Name = "Product_List_DN_ProductType", ResourceType = typeof(ProductStrings))]
        public string ProductType { get; set; }
    }
}
