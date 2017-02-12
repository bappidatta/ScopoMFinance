using ScopoMFinance.Core.Common;
using ScopoMFinance.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles = AppRoles.SuperUser)]
    [LoginAudit]
    public class SuperUserController : Controller
    {
        // GET: Dashboard/SuperUser
        public ActionResult Index()
        {
            return View();
        }
    }
}