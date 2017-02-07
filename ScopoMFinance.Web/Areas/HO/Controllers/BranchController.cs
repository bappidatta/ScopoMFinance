using NtitasCommon.Core.Common;
using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.ViewModels.Policy;
using ScopoMFinance.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Areas.HO.Controllers
{
    [Authorize(Roles = AppRoles.SuperUser)]
    [LoginAudit]
    public class BranchController : Controller
    {
        private IBranchService _branchService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchService"></param>
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
            List<BranchListViewModel> branchList = _branchService.GetBranchList(sortDir, sortCol);
            return View(branchList);
        }
    }
}