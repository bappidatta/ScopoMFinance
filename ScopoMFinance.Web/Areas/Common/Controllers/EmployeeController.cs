using NtitasCommon.Core.Common;
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
        private IUserHelper _userHelper;

        public EmployeeController(IEmployeeService employeeService, IUserHelper userHelper)
        {
            _employeeService = employeeService;
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
                    orderBy = x => x.Address;
                    break;
                case 9:
                    orderBy = x => x.PhoneNo;
                    break;
                case 10:
                    orderBy = x => x.Remarks;
                    break;
                case 11:
                    orderBy = x => x.IsActive;
                    break;
            }

            if (_userHelper.Get().IsHeadOffice)
                filter = x => x.IsDeleted == false;
            else
            {
                int branchId = _userHelper.Get().BranchId;
                filter = x => x.IsDeleted == false && x.BranchId == branchId;
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
    }
}