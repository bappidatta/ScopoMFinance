using NtitasCommon.Core.Common;
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

namespace ScopoMFinance.Web.Areas.Common.Controllers
{
    [SystemMessages]
    [Authorize(Roles = AppRoles.SuperUser + "," + AppRoles.BranchUser)]
    [LoginAudit]
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private IGenderService _genderService;
        private IBranchService _branchService;
        private IEmployeeTypeService _employeeTypeService;
        private IUserHelper _userHelper;

        public EmployeeController(
            IEmployeeService employeeService,
            IGenderService genderService,
            IBranchService branchService,
            IEmployeeTypeService employeeTypeService,
            IUserHelper userHelper)
        {
            _employeeService = employeeService;
            _genderService = genderService;
            _branchService = branchService;
            _employeeTypeService = employeeTypeService;
            _userHelper = userHelper;
        }

        [HttpGet]
        public ActionResult Index(int index = 0, SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
            Expression<Func<Employee, object>> orderBy = null;
            Expression<Func<Employee, bool>> filter = null;

            switch (sortCol)
            {
                case 0:
                    orderBy = x => x.Branch.Name;
                    break;
                case 1:
                default:
                    orderBy = x => x.EmployeeNo;
                    break;
                case 2:
                    orderBy = x => x.EmployeeName;
                    break;
                case 3:
                    orderBy = x => x.IsCreditOfficer;
                    break;
                case 4:
                    orderBy = x => x.JoiningDate;
                    break;
                case 5:
                    orderBy = x => x.ResignDate;
                    break;
                case 6:
                    orderBy = x => x.SysGender.Name;
                    break;
                case 7:
                    orderBy = x => x.EmployeeType.Name;
                    break;
                case 8:
                    orderBy = x => x.IsActive;
                    break;
            }

            if (!_userHelper.Get().IsHeadOffice)
            {
                int branchId = _userHelper.Get().BranchId;
                filter = x => x.BranchId == branchId;
            }

            PList<EmployeeListViewModel> employeeList = _employeeService.GetEmployeeList(index, orderBy, sortDir, filter);

            if (employeeList != null)
            {
                string urlFormat = "/Common/Employee?index={0}";
                employeeList.Pager.URLFormat = urlFormat;
            }

            ViewBag.Title = EmployeeStrings.List_Title;
            return View(employeeList);
        }

        [HttpGet]
        public ActionResult Setup(int? id)
        {
            EmployeeEditViewModel vm = null;

            if (!id.HasValue)
            {
                ViewBag.Title = EmployeeStrings.Create_Title;
                vm = new EmployeeEditViewModel() {
                    JoiningDate = _userHelper.Get().DayOpenClose.SystemDate,
                    IsActive = true
                };

                if (_userHelper.Get().IsHeadOffice)
                    vm.BranchId = _userHelper.Get().BranchId;
            }
            else
            {
                ViewBag.Title = EmployeeStrings.Edit_Title;
                vm = _employeeService.GetEmployeeById(id.Value);
            }

            if (vm == null)
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return RedirectToAction("Index");
            }

            ViewBag.GenderDropDown = new SelectList(_genderService.GetGenderDropDown(), "Value", "Text");
            ViewBag.EmployeeTypeDropDown = new SelectList(_employeeTypeService.GetEmployeeTypeDropDown(), "Value", "Text");

            if (_userHelper.Get().IsHeadOffice)
                ViewBag.BranchDropDown = new SelectList(_branchService.GetBranchDropDown(), "Value", "Text");

            return View("Setup", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setup(EmployeeEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (vm.Id > 0)
                    {
                        ViewBag.Title = EmployeeStrings.Edit_Title;

                        if (_employeeService.UpdateEmployee(vm))
                            SystemMessages.Add(EmployeeStrings.Employee_Edit_Update_Success_Msg, false, true);
                        else
                            SystemMessages.Add(CommonStrings.No_Record, true, true);
                    }
                    else
                    {
                        ViewBag.Title = EmployeeStrings.Create_Title;
                        
                        _employeeService.CreateEmployee(vm);
                        SystemMessages.Add(EmployeeStrings.Employee_Edit_Create_Success_Msg, false, true);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    SystemMessages.Add(CommonStrings.Server_Error, true, true);
                }
            }

            ViewBag.GenderDropDown = new SelectList(_genderService.GetGenderDropDown(), "Value", "Text");
            ViewBag.EmployeeTypeDropDown = new SelectList(_employeeTypeService.GetEmployeeTypeDropDown(), "Value", "Text");

            if (_userHelper.Get().IsHeadOffice)
                ViewBag.BranchDropDown = new SelectList(_branchService.GetBranchDropDown(), "Value", "Text");

            return View("Setup", vm);
        }

        [HttpGet]
        public ActionResult IsEmployeeNoAvailable(string employeeNo, int id)
        {
            return Json(!string.IsNullOrWhiteSpace(employeeNo) && _employeeService.IsEmployeeNoAvailable(employeeNo, id) == true, JsonRequestBehavior.AllowGet);
        }
        
    }
}