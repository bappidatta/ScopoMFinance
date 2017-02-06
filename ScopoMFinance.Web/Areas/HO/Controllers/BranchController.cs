using ScopoMFinance.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Areas.HO.Controllers
{
    [Authorize(Roles = AppRoles.SuperUser)]
    public class BranchController : Controller
    {
        // GET: HO/Branch
        public ActionResult Index()
        {
            return View();
        }
    }
}