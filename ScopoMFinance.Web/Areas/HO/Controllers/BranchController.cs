using NtitasCommon.Core.Common;
using NtitasCommon.Localization;
using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.ViewModels.Policy;
using ScopoMFinance.Localization;
using ScopoMFinance.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Areas.HO.Controllers
{
    [SystemMessages]
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
        [HttpGet]
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.Title = "Create Branch";
                return View(new BranchEditViewModel() { OpenDate = DateTime.Now, Status = true });
            }

            BranchEditViewModel vm = _branchService.GetBranchById(id.Value);

            if (vm != null)
            {
                ViewBag.Title = "Edit Branch";
                return View(vm);
            }
            else
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(BranchEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (vm.Id > 0)
                    {
                        _branchService.UpdateBranch(vm);
                        SystemMessages.Add(BranchStrings.Branch_Update_Success_Msg, false, true);
                    }
                    else
                    {
                        _branchService.SaveBranch(vm);
                        SystemMessages.Add(BranchStrings.Branch_Add_Success_Msg, false, true);
                    }
                    
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    SystemMessages.Add(CommonStrings.Server_Error, true, true);
                }
            }

            return View(vm);
        }
    }
}