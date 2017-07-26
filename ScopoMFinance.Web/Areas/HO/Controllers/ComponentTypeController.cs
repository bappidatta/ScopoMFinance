using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using NtitasCommon.Localization;
using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.ViewModels.Policy;
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
    public class ComponentTypeController : Controller
    {
        private IComponentTypeService _componentTypeService;

        public ComponentTypeController(IComponentTypeService componentTypeService)
        {
            _componentTypeService = componentTypeService;
        }

        // GET: HO/ComponentType
        [HttpGet]
        public ActionResult Index(int index = 0, SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
            Expression<Func<ComponentType, object>> orderBy = null;
            switch (sortCol)
            {
                case 0:
                default:
                    orderBy = x => x.Name;
                    break;
                case 1:
                    orderBy = x => x.NoOfMaxLoan;
                    break;
                case 2:
                    orderBy = x => x.IsActive;
                    break;
            }

            PList<ComponentTypeListViewModel> componentTypeList = _componentTypeService.GetComponentTypeList(index, orderBy, sortDir);

            if(componentTypeList != null)
            {
                string urlFormat = "/HO/ComponentType?index={0}";
                componentTypeList.Pager.URLFormat = urlFormat;
            }

            ViewBag.Title = ComponentStrings.ComponentType_List_Title;
            return View(componentTypeList);
        }

        [HttpGet]
        public ActionResult Setup(int? id)
        {
            ComponentTypeSetupViewModel vm = null;

            if (!id.HasValue)
            {
                ViewBag.Title = ComponentStrings.ComponentType_Create_Title;
                vm = new ComponentTypeSetupViewModel() { IsActive = true };
            }
            else
            {
                ViewBag.Title = ComponentStrings.ComponentType_Edit_Title;
                vm = _componentTypeService.GetComponentTypeById(id.Value);
            }

            if (vm == null)
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return RedirectToAction("Index");
            }

            return PartialView("_Setup", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setup(ComponentTypeSetupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (vm.Id > 0)
                    {
                        if (_componentTypeService.UpdateComponentType(vm))
                            SystemMessages.Add(ComponentStrings.ComponentType_Edit_Update_Success_Msg, false, true);
                        else
                            SystemMessages.Add(CommonStrings.No_Record, true, true);
                    }
                    else
                    {
                        _componentTypeService.CreateComponentType(vm);
                        SystemMessages.Add(ComponentStrings.ComponentType_Edit_Create_Success_Msg, false, true);
                    }

                    return new XHR_JSON_Redirect();
                }
                catch (Exception ex)
                {
                    SystemMessages.Add(CommonStrings.Server_Error, true, true);
                }
            }

            return PartialView("_Setup", vm);
        }
    }
}