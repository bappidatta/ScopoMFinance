using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.ViewModels.Policy;
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
        public ActionResult Index()
        {
            List<BranchListViewModel> branchList = _branchService.GetBranchList();
            return View(branchList);
        }
    }
}