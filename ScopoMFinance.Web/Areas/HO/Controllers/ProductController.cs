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
    public class ProductController : Controller
    {
        private IProductTypeService _productTypeService;
        private IProductService _productService;
        private IComponentService _componentService;

        public ProductController(IProductTypeService productTypeService, IProductService productService, IComponentService componentService)
        {
            _productTypeService = productTypeService;
            _productService = productService;
            _componentService = componentService;
        }

        [HttpGet]
        public ActionResult Index(int index = 0, SortDirection sortDir = SortDirection.Asc, int sortCol = 0)
        {
            Expression<Func<Product, object>> orderBy = null;
            switch (sortCol)
            {
                case 0:
                default:
                    orderBy = x => x.ProductType.Name;
                    break;
                case 1:
                    orderBy = x => x.ProductCode;
                    break;
                case 2:
                    orderBy = x => x.ProductName;
                    break;
                case 3:
                    orderBy = x => x.InterestRate;
                    break;
                case 4:
                    orderBy = x => x.IsActive;
                    break;
            }

            PList<ProductListViewModel> productList = _productService.GetProductList(index, orderBy, sortDir);

            if (productList != null)
            {
                string urlFormat = "/HO/ProductType?index={0}";
                productList.Pager.URLFormat = urlFormat;
            }

            ViewBag.Title = ProductStrings.Product_List_Title;
            return View(productList);
        }

        [HttpGet]
        public ActionResult Setup(int? id)
        {
            ProductSetupViewModel vm = null;

            if (!id.HasValue)
            {
                ViewBag.Title = ProductStrings.Product_Create_Title;
                vm = new ProductSetupViewModel()
                {
                    IsActive = true
                };
            }
            else
            {
                ViewBag.Title = ProductStrings.Product_Edit_Title;
                vm = _productService.GetProductById(id.Value);
            }

            if (vm == null)
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return RedirectToAction("Index");
            }

            ViewBag.ProductTypeDropDown = new SelectList(_productTypeService.GetProductTypeDropDown(), "Value", "Text");

            return View("Setup", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setup(ProductSetupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (vm.Id > 0)
                    {
                        ViewBag.Title = ProductStrings.Product_Edit_Title;

                        if (_productService.UpdateProduct(vm))
                            SystemMessages.Add(ProductStrings.Product_Update_Success_Msg, false, true);
                        else
                            SystemMessages.Add(CommonStrings.No_Record, true, true);
                    }
                    else
                    {
                        ViewBag.Title = ProductStrings.Product_Create_Title;

                        _productService.CreateProduct(vm);
                        SystemMessages.Add(ProductStrings.Product_Create_Success_Msg, false, true);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    SystemMessages.Add(CommonStrings.Server_Error, true, true);
                }
            }

            ViewBag.ProductTypeDropDown = new SelectList(_productTypeService.GetProductTypeDropDown(), "Value", "Text", vm.ProductTypeId);

            return View("Setup", vm);
        }

        [HttpGet]
        public ActionResult IsProductCodeAvailable(string productCode, int id)
        {
            return Json(!string.IsNullOrWhiteSpace(productCode) && _productService.IsProductCodeAvailable(productCode, id) == true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult MapComponent()
        {
            ViewBag.Title = ProductStrings.Product_Component_Map_Setup_Title;
            ViewBag.ProductDropDown = new SelectList(_productService.GetProductDropDown(), "Value", "Text");

            return View("MapComponent");
        }

        [HttpGet]
        public ActionResult ComponentMappedList(int id)
        {
            if (_componentService.GetComponentList(filter: x => x.IsActive).Count == 0)
            {
                SystemMessages.Add(CommonStrings.No_Record, true, true);
                return new XHR_JSON_Redirect();
            }

            var mappedList = _productService.GetMappedComponentList(id);

            return PartialView("_ComponentMappedList", mappedList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MapComponent(ProductComponentMappingViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_componentService.GetComponentList(filter: x => x.IsActive).Count == 0)
                    {
                        SystemMessages.Add(CommonStrings.No_Record, true, true);
                        return RedirectToAction("MapComponent");
                    }

                    ProductSetupViewModel productVM = _productService.GetProductById(vm.ProductId);
                    if (productVM == null || !productVM.IsActive)
                    {
                        SystemMessages.Add(ProductStrings.Product_Component_Map_Validation_InvalidProduct, true, true);
                        return RedirectToAction("MapComponent"); ;
                    }

                    foreach (var i in vm.MappedComponentList)
                    {
                        if (!_componentService.GetComponentById(i.ComponentId).IsActive)
                        {
                            SystemMessages.Add(String.Format(ComponentStrings.Component_Map_Branch_Validation_InvalidComponent, i.ComponentName), true, true);
                            return RedirectToAction("MapComponent");
                        }
                    }

                    if (!_productService.MapComponent(vm))
                    {
                        SystemMessages.Add(CommonStrings.Server_Error, true, true);
                        return RedirectToAction("MapComponent");
                    }
                    else
                    {
                        SystemMessages.Add(ProductStrings.Product_Component_Map_Successfull_Msg, false, true);
                        return RedirectToAction("MapComponent");
                    }
                }
                catch (Exception ex)
                {

                    SystemMessages.Add(CommonStrings.Server_Error, true, true);
                    return RedirectToAction("MapComponent");
                }
            }

            ViewBag.Title = ProductStrings.Product_Component_Map_Setup_Title;
            ViewBag.ProductDropDown = new SelectList(_productService.GetProductDropDown(), "Value", "Text");

            return View("MapComponent");
        }

    }
}