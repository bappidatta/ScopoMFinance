using ScopoMFinance.Domain.Models;
using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Employee
{
    public class EmployeeTypeListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "EmployeeType_List_DN_Name", ResourceType = typeof(EmployeeTypeStrings))]
        public string Name { get; set; }

        [Display(Name = "EmployeeType_List_DN_IsActive", ResourceType = typeof(EmployeeTypeStrings))]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
        public System.DateTime SystemDate { get; set; }
        public System.DateTime SetDate { get; set; }
    }
}
