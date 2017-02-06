using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles = AppRoles.SuperUser)]
    public class SuperUserController : Controller
    {
        private IUserHelper _userHelper;

        public SuperUserController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        // GET: Dashboard/SuperAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}