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

        public EmployeeTypeController(IEmployeeTypeService employeeTypeService)
        {
            _employeeTypeService = employeeTypeService;
        }

        // GET: HO/EmployeeType
        [HttpGet]
        public ActionResult Index(int index = 0, SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
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

            PList<EmployeeTypeListViewModel> employeeTypeList = _employeeTypeService.GetEmployeeTypeList(index, orderBy, sortDir);

            if (employeeTypeList != null)
            {
                string urlFormat = "/HO/EmployeeType?index={0}";
                employeeTypeList.Pager.URLFormat = urlFormat;
            }

            ViewBag.Title = EmployeeTypeStrings.List_Title;
            return View(employeeTypeList);
        }

        [HttpGet]
        public ActionResult Setup(int? id)
        {
            EmployeeTypeEditViewModel vm = null;

            if (!id.HasValue)
            {
                ViewBag.Title = EmployeeTypeStrings.Create_Title;
                vm = new EmployeeTypeEditViewModel() { IsActive = true };
            }
            else
            {
                ViewBag.Title = EmployeeTypeStrings.Edit_Title;
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
                    if (vm.Id > 0)
                    {
                        if(_employeeTypeService.UpdateEmployeeType(vm))
                            SystemMessages.Add(EmployeeTypeStrings.EmployeeType_Edit_Update_Success_Msg, false, true);
                        else
                            SystemMessages.Add(CommonStrings.No_Record, true, true);
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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                EmployeeTypeEditViewModel vm = _employeeTypeService.GetEmployeeTypeById(id);
                if (vm == null)
                {
                    SystemMessages.Add(CommonStrings.No_Record, SystemMessageType.Error, true);
                    return RedirectToAction("Index");
                }

                ViewBag.Title = EmployeeTypeStrings.Delete_Title;
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
                    if(_employeeTypeService.DeleteEmployeeType(id))
                        SystemMessages.Add(EmployeeTypeStrings.Employee_Type_Delete_Success_Msg, SystemMessageType.Success, true);
                    else
                        SystemMessages.Add(CommonStrings.No_Record, SystemMessageType.Error, true);
                }
                catch (Exception ex)
                {
                    SystemMessages.Add(CommonStrings.Server_Error, SystemMessageType.Error, true);
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