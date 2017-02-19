using NtitasCommon.Core.Common;
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
        private IUserHelper _userHelper;

        public OrganizationController(
            IOrganizationService orgService, 
            IOrgCategoryService orgCategoryService,
            IGenderService genderService,
            IColcOptionService colcOptionService,
            IUserHelper userHelper)
        {
            _orgService = orgService;
            _orgCategoryService = orgCategoryService;
            _genderService = genderService;
            _colcOptionService = colcOptionService;
            _userHelper = userHelper;
        }

        [HttpGet]
        public ActionResult Index(int index = 0, SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
            int pageSize = _userHelper.PagerSize;
            int branchId = _userHelper.Get().BranchId;

            Expression<Func<Organization, bool>> filter = x => x.BranchId == branchId && x.IsDeleted == false;

            PList<OrganizationListViewModel> orgList = _orgService.GetOrganizationList(index, pageSize, sortDir, sortCol, filter);

            if (orgList != null)
            {
                string urlFormat = "/Branch/Organization?index={0}";
                orgList.Pager.URLFormat = urlFormat;
            }

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
                ViewBag.LoanCollectionOptionDropDown = new SelectList(_colcOptionService.GetColcOptionDropDown(), "Value", "Text");
                ViewBag.SavingsCollectionOptionDropDown = new SelectList(_colcOptionService.GetColcOptionDropDown(), "Value", "Text");

                return View(new OrganizationEditViewModel() {
                    SetupDate = _userHelper.Get().DayOpenClose.SystemDate,
                    FirstLoanColcDate = _userHelper.Get().DayOpenClose.SystemDate,
                    FirstSavColcDate = _userHelper.Get().DayOpenClose.SystemDate,
                    IsActive = true,
                    BranchId = _userHelper.Get().BranchId
                });
            }

            OrganizationEditViewModel vm = _orgService.GetOrganizationById(id.Value, _userHelper.Get().BranchId);

            if (vm != null)
            {
                ViewBag.Title = OrganizationStrings.Organization_Edit_Update_Title;

                ViewBag.OrgCategoryDropDown = new SelectList(_orgCategoryService.GetOrgCategoryDropDown(), "Value", "Text", vm.OrgCategoryId);
                ViewBag.GenderDropDown = new SelectList(_genderService.GetGenderDropDown(), "Value", "Text", vm.GenderId);
                ViewBag.LoanCollectionOptionDropDown = new SelectList(_colcOptionService.GetColcOptionDropDown(), "Value", "Text", vm.LoanColcOptionId);
                ViewBag.SavingsCollectionOptionDropDown = new SelectList(_colcOptionService.GetColcOptionDropDown(), "Value", "Text", vm.SavColcOptionId);

                return View(vm);
            }
            else
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(OrganizationEditViewModel vm)
        {
            if (ModelState.IsValid)
            { 
                try
                {
                    vm.BranchId = _userHelper.Get().BranchId;
                    vm.SetupDate = _userHelper.Get().DayOpenClose.SystemDate;
                    vm.CreatedBy = _userHelper.Get().UserId;

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
            ViewBag.LoanCollectionOptionDropDown = new SelectList(_colcOptionService.GetColcOptionDropDown(), "Value", "Text", vm.LoanColcOptionId);
            ViewBag.SavingsCollectionOptionDropDown = new SelectList(_colcOptionService.GetColcOptionDropDown(), "Value", "Text", vm.SavColcOptionId);

            return View(vm);
        }

        [HttpGet]
        public ActionResult IsOrgNoAvailable(string organizationNo, int id)
        {
            return Json(!string.IsNullOrWhiteSpace(organizationNo) && _orgService.IsOrgNoAvailable(organizationNo, _userHelper.Get().BranchId, id) == true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                OrganizationEditViewModel vm = _orgService.GetOrganizationById(id, _userHelper.Get().BranchId);
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
        public ActionResult Delete(int id, FormCollection fc)
        {
            if (id > 0)
            {
                try
                {
                    _orgService.DeleteOrganization(id, _userHelper.Get().BranchId);
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
    }
}