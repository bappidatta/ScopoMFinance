using NtitasCommon.Core.Common;
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

        public ProductController(IProductTypeService productTypeService, IProductService productService)
        {
            _productTypeService = productTypeService;
            _productService = productService;
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
    }
}