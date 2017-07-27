using NtitasCommon.Core.Common;
using NtitasCommon.Localization;
using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Enums;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.ViewModels.Component;
using ScopoMFinance.Localization;
using ScopoMFinance.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Areas.HO.Controllers
{
    [SystemMessages]
    [Authorize(Roles = AppRoles.SuperUser)]
    [LoginAudit]
    public class ComponentController : Controller
    {
        private IComponentTypeService _componentTypeService;
        private IComponentService _componentService;
        private IDonorService _donorService;
        private IBranchService _branchService;
        private IUserHelper _userHelper;

        public ComponentController(IComponentTypeService componentTypeService, IComponentService componentService, IDonorService donorService, IBranchService branchService, IUserHelper userHelper)
        {
            _componentTypeService = componentTypeService;
            _componentService = componentService;
            _donorService = donorService;
            _branchService = branchService;
            _userHelper = userHelper;
        }

        [HttpGet]
        public ActionResult Index(int index = 0, SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
            Expression<Func<Component, object>> orderBy = null;
            Expression<Func<Component, bool>> filter = null;

            switch (sortCol)
            {
                case 0:
                    orderBy = x => x.ComponentCode;
                    break;
                case 1:
                default:
                    orderBy = x => x.Name;
                    break;
                case 2:
                    orderBy = x => x.Duration;
                    break;
                case 3:
                    orderBy = x => x.ComponentType.Name;
                    break;
                case 4:
                    orderBy = x => x.SysDonor.Name;
                    break;
                case 5:
                    orderBy = x => x.IsActive;
                    break;
            }

            PList<ComponentListViewModel> componentList = _componentService.GetComponentList(index, orderBy, sortDir, filter);

            if (componentList != null)
            {
                string urlFormat = "/HO/Component?index={0}";
                componentList.Pager.URLFormat = urlFormat;
            }

            ViewBag.Title = ComponentStrings.Component_List_Title;
            return View(componentList);
        }

        [HttpGet]
        public ActionResult Setup(int? id)
        {
            ComponentSetupViewModel vm = null;

            if (!id.HasValue)
            {
                ViewBag.Title = ComponentStrings.Component_Create_Title;
                vm = new ComponentSetupViewModel()
                {
                    IsActive = true
                };
            }
            else
            {
                ViewBag.Title = ComponentStrings.Component_Edit_Title;
                vm = _componentService.GetComponentById(id.Value);
            }

            if (vm == null)
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return RedirectToAction("Index");
            }

            ViewBag.ComponentTypeDropDown = new SelectList(_componentTypeService.GetComponentTypeDropDown(), "Value", "Text");
            ViewBag.DonorDropDown = new SelectList(_donorService.GetDonorDropDown(), "Value", "Text");

            return View("Setup", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setup(ComponentSetupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //if (!_componentTypeService.IsComponentTypeActive(vm.ComponentTypeId))
                //{
                //    ViewBag.ComponentTypeDropDown = new SelectList(_componentTypeService.GetComponentTypeDropDown(), "Value", "Text", vm.ComponentTypeId);
                //    ViewBag.DonorDropDown = new SelectList(_donorService.GetDonorDropDown(), "Value", "Text", vm.DonorId);
                //    SystemMessages.Add(ComponentStrings.Component_Edit_Validation_ComponentTypeInactive, true, true);
                //    return View("Setup", vm);
                //}

                try
                {
                    if (vm.Id > 0)
                    {
                        ViewBag.Title = ComponentStrings.Component_Edit_Title;

                        if (_componentService.UpdateComponent(vm))
                            SystemMessages.Add(ComponentStrings.Component_Update_Success_Msg, false, true);
                        else
                            SystemMessages.Add(CommonStrings.No_Record, true, true);
                    }
                    else
                    {
                        ViewBag.Title = ComponentStrings.Component_Create_Title;

                        _componentService.CreateComponent(vm);
                        SystemMessages.Add(ComponentStrings.Component_Create_Success_Msg, false, true);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    SystemMessages.Add(CommonStrings.Server_Error, true, true);
                }
            }

            ViewBag.ComponentTypeDropDown = new SelectList(_componentTypeService.GetComponentTypeDropDown(), "Value", "Text", vm.ComponentTypeId);
            ViewBag.DonorDropDown = new SelectList(_donorService.GetDonorDropDown(), "Value", "Text", vm.DonorId);

            return View("Setup", vm);
        }

        [HttpGet]
        public ActionResult IsComponentCodeAvailable(string componentCode, int id)
        {
            return Json(!string.IsNullOrWhiteSpace(componentCode) && _componentService.IsComponentCodeAvailable(componentCode, id) == true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IsComponentTypeActive(int componentTypeId, int id)
        {
            return Json(_componentTypeService.IsComponentTypeActive(componentTypeId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult MapBranch()
        {
            ViewBag.Title = ComponentStrings.Component_Map_Branch_Setup_Title;
            ViewBag.BranchDropDown = new SelectList(_branchService.GetBranchDropDown(x => x.IsActive && !x.IsHeadOffice), "Value", "Text");

            return View("MapBranch");
        }
    }
}