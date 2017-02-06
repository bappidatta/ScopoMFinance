using ScopoMFinance.Core.Common;
using ScopoMFinance.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles = AppRoles.BranchUser)]
    [LoginAudit]
    public class BranchUserController : Controller
    {
        // GET: Dashboard/BranchUser
        public ActionResult Index()
        {
            return View();
        }
    }
}