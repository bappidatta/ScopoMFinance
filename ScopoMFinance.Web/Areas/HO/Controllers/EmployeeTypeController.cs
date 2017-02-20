using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using NtitasCommon.Localization;
using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.ViewModels.Employee;
using ScopoMFinance.Localization;
using ScopoMFinance.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Areas.HO.Controllers
{
    [SystemMessages]
    [Authorize(Roles = AppRoles.SuperUser)]
    [LoginAudit]
    public class EmployeeTypeController : Controller
    {
        private IEmployeeTypeService _employeeTypeService;
        private IUserHelper _userHelper;

        public EmployeeTypeController(IEmployeeTypeService employeeTypeService, IUserHelper userHelper)
        {
            _employeeTypeService = employeeTypeService;
            _userHelper = userHelper;
        }

        // GET: HO/EmployeeType
        [HttpGet]
        public ActionResult Index(int index = 0, SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
            int pageSize = _userHelper.PagerSize;

            Expression<Func<EmployeeType, object>> orderBy = null;
            switch (sortCol)
            {
                case 0:
                default:
                    orderBy = x => x.Name;
                    break;
                case 1:
                    orderBy = x => x.IsActive;
                    break;
            }

            PList<EmployeeTypeListViewModel> employeeTypeList = _employeeTypeService.GetEmployeeTypeList(index, pageSize, orderBy, sortDir);

            if (employeeTypeList != null)
            {
                string urlFormat = "/HO/EmployeeType?index={0}";
                employeeTypeList.Pager.URLFormat = urlFormat;
            }

            return View(employeeTypeList);
        }

        [HttpGet]
        public ActionResult Setup(int? id)
        {
            EmployeeTypeEditViewModel vm = null;

            if (!id.HasValue)
            {
                ViewBag.Title = "Create Employee Type";
                vm = new EmployeeTypeEditViewModel() { IsActive = true };
            }
            else
            {
                ViewBag.Title = "Edit Employee Type";
                vm = _employeeTypeService.GetEmployeeTypeById(id.Value);
            }

            if (vm == null)
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return RedirectToAction("Index");
            }

            return PartialView("_Setup", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setup(EmployeeTypeEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    vm.UserId = _userHelper.Get().UserId;
                    vm.SystemDate = _userHelper.Get().DayOpenClose.SystemDate;
                    
                    if (vm.Id > 0)
                    {
                        _employeeTypeService.UpdateEmployeeType(vm);
                        SystemMessages.Add(EmployeeTypeStrings.EmployeeType_Edit_Update_Success_Msg, false, true);
                    }
                    else
                    {
                        _employeeTypeService.CreateEmployeeType(vm);
                        SystemMessages.Add(EmployeeTypeStrings.EmployeeType_Edit_Create_Success_Msg, false, true);
                    }

                    return new XHR_JSON_Redirect();
                }
                catch (Exception ex)
                {
                    SystemMessages.Add(CommonStrings.Server_Error, true, true);
                }
            }

            return PartialView("_Setup", vm);
        }
    }
}