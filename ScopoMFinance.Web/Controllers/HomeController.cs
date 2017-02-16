using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.ViewModels.Acc;
using ScopoMFinance.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IUserHelper _userHelper;
        private IDayOpenCloseService _dayOpenCloseService;
        private IUserLoginAuditService _loginAuditService;

        public HomeController(IUserHelper userHelper, IDayOpenCloseService dayOpenCloseService, IUserLoginAuditService loginAuditService)
        {
            _userHelper = userHelper;
            _dayOpenCloseService = dayOpenCloseService;
            _loginAuditService = loginAuditService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            if (_userHelper.Get() != null)
            {
                if (Request.IsAuthenticated && User.IsInRole(AppRoles.SuperUser))
                {
                    return RedirectToAction("Index", "SuperUser", new { Area = "Dashboard" });
                }
                if (Request.IsAuthenticated && User.IsInRole(AppRoles.BranchManager))
                {
                    return RedirectToAction("Index", "BranchManager", new { Area = "Dashboard" });
                }
                if (Request.IsAuthenticated && User.IsInRole(AppRoles.BranchUser))
                {
                    return RedirectToAction("Index", "BranchUser", new { Area = "Dashboard" });
                }
                if (Request.IsAuthenticated && User.IsInRole(AppRoles.AreaCoordinator))
                {
                    return RedirectToAction("Index", "AreaCoordinator", new { Area = "Dashboard" });
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = AppRoles.SuperUser + "," + AppRoles.BranchUser + "," + AppRoles.BranchManager + "," + AppRoles.AreaCoordinator)]
        [LoginAudit]
        [HttpGet]
        public ActionResult DayOpenClose()
        {
            DayOpenCloseViewModel vm = _dayOpenCloseService.GetDayOpenClose(_userHelper.Get().BranchId);

            ViewBag.Title = "Day Open Close";
            return View(vm);
        }

        [Authorize(Roles = AppRoles.SuperUser + "," + AppRoles.BranchUser + "," + AppRoles.BranchManager + "," + AppRoles.AreaCoordinator)]
        [LoginAudit]
        [HttpPost]
        public ActionResult DayCloseRequest()
        {
            _dayOpenCloseService.DayCloseRequest(_userHelper.Get().BranchId, _userHelper.Get().UserId);
            _userHelper.InvalidateCache(User.Identity.Name);
            _userHelper.Get();

            return RedirectToAction("DayOpenClose");
        }

        [Authorize(Roles = AppRoles.SuperUser + "," + AppRoles.BranchUser + "," + AppRoles.BranchManager + "," + AppRoles.AreaCoordinator)]
        [LoginAudit]
        [HttpPost]
        public ActionResult CloseDay()
        {
            _dayOpenCloseService.CloseDay(_userHelper.Get().BranchId);
            _userHelper.InvalidateCache(User.Identity.Name);
            _userHelper.Get();

            return RedirectToAction("DayOpenClose");
        }

        [Authorize(Roles = AppRoles.SuperUser + "," + AppRoles.BranchUser + "," + AppRoles.BranchManager + "," + AppRoles.AreaCoordinator)]
        [LoginAudit]
        [HttpPost]
        public ActionResult OpenDay()
        {
            _dayOpenCloseService.OpenDay(_userHelper.Get().BranchId);
            _userHelper.InvalidateCache(User.Identity.Name);
            _userHelper.Get();

            return RedirectToAction("DayOpenClose");
        }
    }
}