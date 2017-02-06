using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Helpers;
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

        public HomeController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
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
    }
}