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
    public class LoanProductListViewModel : CoreListViewModel
    {
        [Display(Name = "Ln_Product_List_DN_Code", ResourceType = typeof(ProductStrings))]
        public string ProductCode { get; set; }

        [Display(Name = "Ln_Product_List_DN_Name", ResourceType = typeof(ProductStrings))]
        public string ProductName { get; set; }

        [Display(Name = "Ln_Product_List_DN_IntRate", ResourceType = typeof(ProductStrings))]
        public decimal InterestRate { get; set; }
    }
}
