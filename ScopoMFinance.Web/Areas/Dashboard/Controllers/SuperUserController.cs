using ScopoMFinance.Core.Common;
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
        // GET: Dashboard/SuperAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}