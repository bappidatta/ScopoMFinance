using NtitasCommon.Core.Common;
using ScopoMFinance.Core.Common;
using ScopoMFinance.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Areas.Branch.Controllers
{
    [SystemMessages]
    [Authorize(Roles = AppRoles.SuperUser + "," + AppRoles.BranchManager + "," + AppRoles.BranchUser)]
    [LoginAudit]
    public class OrganizationController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}