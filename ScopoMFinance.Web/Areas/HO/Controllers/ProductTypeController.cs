using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using NtitasCommon.Localization;
using ScopoMFinance.Core.Common;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.ViewModels.Product;
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
    public class ProductTypeController : Controller
    {
        private IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        public ActionResult Index(int index = 0, SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
            Expression<Func<ProductType, object>> orderBy = null;
            switch (sortCol)
            {
                case 0:
                default:
                    orderBy = x => x.Name;
                    break;
                case 1:
                    orderBy = x => x.IsActive;
                    break;
            }

            PList<ProductTypeListViewModel> productTypeList = _productTypeService.GetProductTypeList(index, orderBy, sortDir);

            if (productTypeList != null)
            {
                string urlFormat = "/HO/ProductType?index={0}";
                productTypeList.Pager.URLFormat = urlFormat;
            }

            ViewBag.Title = ProductStrings.Product_Type_List_Title;
            return View(productTypeList);
        }

        [HttpGet]
        public ActionResult Setup(int? id)
        {
            ProductTypeSetupViewModel vm = null;

            if (!id.HasValue)
            {
                ViewBag.Title = ProductStrings.Product_Type_Create_Title;
                vm = new ProductTypeSetupViewModel() { IsActive = true };
            }
            else
            {
                ViewBag.Title = ProductStrings.Product_Type_Edit_Title;
                vm = _productTypeService.GetProductTypeById(id.Value);
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
        public ActionResult Setup(ProductTypeSetupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (vm.Id > 0)
                    {
                        if (_productTypeService.UpdateProductType(vm))
                            SystemMessages.Add(ProductStrings.Product_Type_Edit_Update_Success_Msg, false, true);
                        else
                            SystemMessages.Add(CommonStrings.No_Record, true, true);
                    }
                    else
                    {
                        _productTypeService.CreateProductType(vm);
                        SystemMessages.Add(ProductStrings.Product_Type_Edit_Create_Success_Msg, false, true);
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