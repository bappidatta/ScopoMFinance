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
            switch (sortCol)
            {
                case 0:
                default:
                    orderBy = x => x.EmployeeNo;
                    break;
                case 1:
                    orderBy = x => x.EmployeeName;
                    break;
            }

            PList<EmployeeListViewModel> employeeList = _employeeService.GetEmployeeList(index, orderBy, sortDir);

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