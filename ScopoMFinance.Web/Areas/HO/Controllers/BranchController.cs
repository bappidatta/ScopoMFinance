using NtitasCommon.Core.Common;
using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Helpers;
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
        private IUserHelper _userHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchService"></param>
        public BranchController(IBranchService branchService, IUserHelper userHelper)
        {
            _branchService = branchService;
            _userHelper = userHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int index = 0, SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
            int pageSize = _userHelper.PagerSize;

            PList<BranchListViewModel> branchList = _branchService.GetBranchList(index, pageSize, sortDir, sortCol);

            if (branchList != null)
            {
                string urlFormat = "/HO/Branch?index={0}";
                branchList.Pager.URLFormat = urlFormat;
            }

            return View(branchList);
        }
    }
}