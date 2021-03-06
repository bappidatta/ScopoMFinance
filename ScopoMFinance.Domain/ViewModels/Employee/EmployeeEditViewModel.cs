﻿using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ScopoMFinance.Domain.ViewModels.Employee
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Employee_List_DN_Branch", ResourceType = typeof(EmployeeStrings))]
        public int BranchId { get; set; }

        [Display(Name = "Employee_List_DN_EmployeeNo", ResourceType = typeof(EmployeeStrings))]
        [Required]
        [MaxLength(10, ErrorMessageResourceName = "Employee_Edit_Validation_EmployeeNoMaxLength", ErrorMessageResourceType = typeof(EmployeeStrings))]
        [Remote("IsEmployeeNoAvailable", "Employee", "Common", ErrorMessageResourceName = "Employee_Edit_Validation_EmployeeNoUnavailable", ErrorMessageResourceType = typeof(EmployeeStrings), AdditionalFields = "Id")]
        public string EmployeeNo { get; set; }

        [Display(Name = "Employee_List_DN_EmployeeName", ResourceType = typeof(EmployeeStrings))]
        [Required]
        public string EmployeeName { get; set; }

        [Display(Name = "Employee_List_DN_IsCO", ResourceType = typeof(EmployeeStrings))]
        public bool IsCreditOfficer { get; set; }

        [Display(Name = "Employee_List_DN_JoiningDate", ResourceType = typeof(EmployeeStrings))]
        [Required]
        public System.DateTime JoiningDate { get; set; }

        [Display(Name = "Employee_List_DN_ResignDate", ResourceType = typeof(EmployeeStrings))]
        public Nullable<System.DateTime> ResignDate { get; set; }

        [Display(Name = "Employee_List_DN_Gender", ResourceType = typeof(EmployeeStrings))]
        [Required]
        public int GenderId { get; set; }

        [Display(Name = "Employee_List_DN_EmployeeType", ResourceType = typeof(EmployeeStrings))]
        [Required]
        public int EmployeeTypeId { get; set; }

        [Display(Name = "Employee_List_DN_Address", ResourceType = typeof(EmployeeStrings))]
        public string Address { get; set; }

        [Display(Name = "Employee_List_DN_PhoneNo", ResourceType = typeof(EmployeeStrings))]
        public string PhoneNo { get; set; }

        [Display(Name = "Employee_List_DN_Remarks", ResourceType = typeof(EmployeeStrings))]
        public string Remarks { get; set; }

        [Display(Name = "Employee_List_DN_IsActive", ResourceType = typeof(EmployeeStrings))]
        public bool IsActive { get; set; }
    }
}
