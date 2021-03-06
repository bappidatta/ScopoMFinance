﻿using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using NtitasCommon.Localization;
using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.ViewModels.Org;
using ScopoMFinance.Localization;
using ScopoMFinance.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Areas.Branch.Controllers
{
    [SystemMessages]
    [Authorize(Roles = AppRoles.SuperUser + "," + AppRoles.BranchManager + "," + AppRoles.BranchUser)]
    [LoginAudit]
    public class OrganizationController : Controller
    {
        private IOrganizationService _orgService;
        private IOrgCategoryService _orgCategoryService;
        private IGenderService _genderService;
        private IColcOptionService _colcOptionService;
        private IEmployeeService _employeeService;
        private IUserHelper _userHelper;

        public OrganizationController(
            IOrganizationService orgService, 
            IOrgCategoryService orgCategoryService,
            IGenderService genderService,
            IColcOptionService colcOptionService,
            IEmployeeService employeeService,
            IUserHelper userHelper)
        {
            _orgService = orgService;
            _orgCategoryService = orgCategoryService;
            _genderService = genderService;
            _colcOptionService = colcOptionService;
            _employeeService = employeeService;
            _userHelper = userHelper;
        }

        [HttpGet]
        public ActionResult Index(int index = 0, SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
            int branchId = _userHelper.Get().BranchId;

            Expression<Func<Organization, bool>> filter = x => x.BranchId == branchId;

            Expression<Func<Organization, object>> orderBy = null;
            switch (sortCol)
            {
                case 0:
                default:
                    orderBy = x => x.OrgCategory.CategoryName;
                    break;
                case 1:
                    orderBy = x => x.OrganizationNo;
                    break;
                case 2:
                    orderBy = x => x.OrganizationName;
                    break;
                case 3:
                    orderBy = x => x.Employee.EmployeeName;
                    break;
                case 4:
                    orderBy = x => x.SysGender.Name;
                    break;
                case 5:
                    orderBy = x => x.SetupDate;
                    break;
                case 6:
                    orderBy = x => x.SysColcOption.Name;
                    break;
                case 7:
                    orderBy = x => x.MeetingDate;
                    break;
                case 8:
                    orderBy = x => x.IsActive;
                    break;
            };

            PList<OrganizationListViewModel> orgList = _orgService.GetOrganizationList(index, orderBy, sortDir, filter);

            if (orgList != null)
            {
                string urlFormat = "/Branch/Organization?index={0}";
                orgList.Pager.URLFormat = urlFormat;
            }

            ViewBag.Title = OrganizationStrings.Organization_List_Title;
            return View(orgList);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.Title = OrganizationStrings.Organization_Edit_Add_Title;

                ViewBag.OrgCategoryDropDown = new SelectList(_orgCategoryService.GetOrgCategoryDropDown(), "Value", "Text");
                ViewBag.GenderDropDown = new SelectList(_genderService.GetGenderDropDown(), "Value", "Text");
                ViewBag.CollectionOptionDropDown = new SelectList(_colcOptionService.GetColcOptionDropDown(), "Value", "Text");

                DateTime systemDate = _userHelper.Get().DayOpenClose.SystemDate;
                return View(new OrganizationEditViewModel() {
                    SetupDate = systemDate,
                    MeetingDate = systemDate,
                    IsActive = true
                });
            }

            OrganizationEditViewModel vm = _orgService.GetOrganizationById(id.Value);

            if (vm != null)
            {
                ViewBag.Title = OrganizationStrings.Organization_Edit_Update_Title;

                ViewBag.OrgCategoryDropDown = new SelectList(_orgCategoryService.GetOrgCategoryDropDown(), "Value", "Text", vm.OrgCategoryId);
                ViewBag.GenderDropDown = new SelectList(_genderService.GetGenderDropDown(), "Value", "Text", vm.GenderId);
                ViewBag.CollectionOptionDropDown = new SelectList(_colcOptionService.GetColcOptionDropDown(), "Value", "Text", vm.MeetingFrequencyId);

                return View(vm);
            }
            else
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrganizationEditViewModel vm)
        {
            if (ModelState.IsValid)
            { 
                try
                {
                    if (vm.Id > 0)
                    {
                        _orgService.UpdateOrganization(vm);
                        SystemMessages.Add(OrganizationStrings.Organization_Edit_Update_Success_Msg, false, true);
                    }
                    else 
                    {
                        _orgService.CreateOrganization(vm);
                        SystemMessages.Add(OrganizationStrings.Organization_Edit_Add_Success_Msg, false, true);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    SystemMessages.Add(CommonStrings.Server_Error, true, true);
                }
            }

            ViewBag.OrgCategoryDropDown = new SelectList(_orgCategoryService.GetOrgCategoryDropDown(), "Value", "Text", vm.OrgCategoryId);
            ViewBag.GenderDropDown = new SelectList(_genderService.GetGenderDropDown(), "Value", "Text", vm.GenderId);
            ViewBag.CollectionOptionDropDown = new SelectList(_colcOptionService.GetColcOptionDropDown(), "Value", "Text", vm.MeetingFrequencyId);

            return View(vm);
        }

        [HttpGet]
        public ActionResult IsOrgNoAvailable(string organizationNo, int id)
        {
            return Json(!string.IsNullOrWhiteSpace(organizationNo) && _orgService.IsOrgNoAvailable(organizationNo, id) == true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                OrganizationEditViewModel vm = _orgService.GetOrganizationById(id);
                if (vm == null)
                {
                    SystemMessages.Add(CommonStrings.No_Record, SystemMessageType.Error, true);
                    return RedirectToAction("Index");
                }
                return PartialView("_Delete", vm);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection fc)
        {
            if (id > 0)
            {
                try
                {
                    _orgService.DeleteOrganization(id);
                    SystemMessages.Add(OrganizationStrings.Organization_Delete_Success_Msg, false, true);
                }
                catch (Exception ex)
                {
                    SystemMessages.Add(CommonStrings.Server_Error, true, true);
                }
            }
            else
            {
                SystemMessages.Add(CommonStrings.POST_NoID, SystemMessageType.Error, true);
            }

            return new XHR_JSON_Redirect();
        }

        [HttpGet]
        public ActionResult MapCreditOfficer(int? id)
        {
            int branchId = _userHelper.Get().BranchId;

            if (id.HasValue && _employeeService.GetEmployeeList(filter: x => x.BranchId == branchId && x.Id == id && x.IsActive && x.IsCreditOfficer).Count == 0)
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return RedirectToAction("Index");
            }

            ViewBag.CreditOfficerDropDown = new SelectList(_employeeService.GetEmployeeDropDown(x => x.BranchId == branchId && x.IsCreditOfficer && x.IsActive), "Value", "Text", id);

            ViewBag.Title = OrganizationStrings.Organization_Credit_Officer_Title;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MapCreditOfficer(OrgCOMappingViewModel vm)
        {
            int branchId = _userHelper.Get().BranchId;

            if (ModelState.IsValid)
            {
                if (_employeeService.GetEmployeeList(filter: x => x.BranchId == branchId && x.Id == vm.CreditOfficerId && x.IsActive && x.IsCreditOfficer).Count == 0)
                {
                    SystemMessages.Add(CommonStrings.No_Record, true, true);
                    return RedirectToAction("Index");
                }

                string validationMessage = String.Empty;
                if (!_orgService.MapCreditOfficer(vm, out validationMessage))
                {
                    SystemMessages.Add(validationMessage, true);
                }
                else
                {
                    SystemMessages.Add(OrganizationStrings.Organization_CO_Map_Successfull_Msg, false, true);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CreditOfficerDropDown = new SelectList(_employeeService.GetEmployeeDropDown(x => x.BranchId == branchId && x.IsCreditOfficer && x.IsActive), "Value", "Text");

            ViewBag.Title = OrganizationStrings.Organization_Credit_Officer_Title;
            return View();
        }

        [HttpGet]
        public ActionResult CreditOfficerMappedList(int id)
        {
            int branchId = _userHelper.Get().BranchId;

            if (_employeeService.GetEmployeeList(filter: x => x.BranchId == branchId && x.Id == id && x.IsActive && x.IsCreditOfficer).Count == 0)
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return new XHR_JSON_Redirect();
            }

            var mappedList = _orgService.GetMappedOrganizationList(id);

            return PartialView("CreditOfficerMappedList", mappedList);
        }
    }
}