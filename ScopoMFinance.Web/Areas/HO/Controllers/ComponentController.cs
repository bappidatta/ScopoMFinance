using NtitasCommon.Core.Common;
using ScopoMFinance.Core.Common;
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
        private IUserHelper _userHelper;

        public ComponentController(IComponentTypeService componentTypeService, IComponentService componentService, IDonorService donorService, IUserHelper userHelper)
        {
            _componentTypeService = componentTypeService;
            _componentService = componentService;
            _donorService = donorService;
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
    }
}