using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using ScopoMFinance.Domain.ViewModels.Component;
using ScopoMFinance.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IProductService
    {
        List<DropDownViewModel> GetProductDropDown();
        ProductSetupViewModel GetProductById(int productId);
        PList<ProductListViewModel> GetProductList(int pageNumber, Expression<Func<Product, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc, Expression<Func<Product, bool>> filter = null);
        List<ProductListViewModel> GetProductTypeList(Expression<Func<Product, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc, Expression<Func<Product, bool>> filter = null);
        void CreateProduct(ProductSetupViewModel vm);
        bool UpdateProduct(ProductSetupViewModel vm);
        bool IsProductActive(int productId);
        bool IsProductCodeAvailable(string productCode, int productId);
        List<MappedComponentViewModel> GetMappedComponentList(int id);
        bool MapComponent(ProductComponentMappingViewModel vm);
    }

    public class ProductService : IProductService
    {
        private UnitOfWork _uow;
        private IUserHelper _userHelper;

        public ProductService(UnitOfWork uow, IUserHelper userHelper)
        {
            _uow = uow;
            _userHelper = userHelper;
        }

        private Product GetProduct(int id)
        {
            return (from c in _uow.ProductRepository
                                      .Get(x => x.Id == id)
                    select c).SingleOrDefault();
        }

        public List<DropDownViewModel> GetProductDropDown()
        {
            var loanProductDropDown = from c in _uow.ProductRepository.Get(x => x.IsActive == true)
                                      select new DropDownViewModel()
                                      {
                                          Value = c.Id,
                                          Text = "(" + c.ProductCode + ") " + c.ProductName
                                      };

            return loanProductDropDown.ToList();
        }

        public ProductSetupViewModel GetProductById(int productId)
        {
            return (from c in _uow.ProductRepository.Get(x => x.Id == productId)
                    select new ProductSetupViewModel
                    {
                        Id = c.Id,
                        ProductCode = c.ProductCode,
                        ProductName = c.ProductName,
                        ProductTypeId = c.ProductTypeId,
                        InterestRate = c.InterestRate,
                        IsActive = c.IsActive,
                        UserId = c.UserId,
                        SetDate = c.SetDate,
                        SystemDate = c.SystemDate
                    }).SingleOrDefault();
        }

        public PList<ProductListViewModel> GetProductList(int pageNumber, Expression<Func<Product, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc, Expression<Func<Product, bool>> filter = null)
        {
            PagerSettings psettings = null;
            int pageSize = _userHelper.PagerSize;

            var product = (from c in _uow.ProductRepository.Get(filter).Order(orderBy, sortDir)
                               select new ProductListViewModel
                               {
                                   Id = c.Id,
                                   ProductCode = c.ProductCode,
                                   ProductName = c.ProductName,
                                   ProductTypeId = c.ProductTypeId,
                                   ProductType = c.ProductType.Name,
                                   InterestRate = c.InterestRate,
                                   IsActive = c.IsActive,
                                   UserId = c.UserId,
                                   SetDate = c.SetDate,
                                   SystemDate = c.SystemDate
                               }).Page(pageNumber, pageSize, out psettings);

            return product.ToPList(psettings);
        }

        public List<ProductListViewModel> GetProductTypeList(Expression<Func<Product, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc, Expression<Func<Product, bool>> filter = null)
        {
            var product = (from c in _uow.ProductRepository.Get(filter).Order(orderBy, sortDir)
                               select new ProductListViewModel
                               {
                                   Id = c.Id,
                                   ProductCode = c.ProductCode,
                                   ProductName = c.ProductName,
                                   ProductTypeId = c.ProductTypeId,
                                   ProductType = c.ProductType.Name,
                                   InterestRate = c.InterestRate,
                                   IsActive = c.IsActive,
                                   UserId = c.UserId,
                                   SetDate = c.SetDate,
                                   SystemDate = c.SystemDate
                               });

            return product.ToList();
        }

        public void CreateProduct(ProductSetupViewModel vm)
        {
            Product product = new Product
            {
                ProductCode = vm.ProductCode,
                ProductName = vm.ProductName,
                ProductTypeId = vm.ProductTypeId,
                InterestRate = vm.InterestRate,
                IsActive = vm.IsActive,
                UserId = _userHelper.Get().UserId,
                SystemDate = _userHelper.Get().DayOpenClose.SystemDate,
                SetDate = DateTime.Now
            };

            _uow.ProductRepository.Insert(product);
            _uow.Save();
        }

        public bool UpdateProduct(ProductSetupViewModel vm)
        {
            Product model = GetProduct(vm.Id);

            if (model == null)
                return false;

            model.ProductCode = vm.ProductCode;
            model.ProductName = vm.ProductName;
            model.ProductTypeId = vm.ProductTypeId;
            model.InterestRate = vm.InterestRate;
            model.IsActive = vm.IsActive;
            model.UserId = _userHelper.Get().UserId;
            model.SystemDate = _userHelper.Get().DayOpenClose.SystemDate;
            model.SetDate = DateTime.Now;

            _uow.ProductRepository.Update(model);
            _uow.Save();

            return true;
        }

        public bool IsProductActive(int productId)
        {
            Product model = GetProduct(productId);

            if (model == null)
                return false;

            if (model.IsActive)
                return true;

            return false;
        }

        public bool IsProductCodeAvailable(string productCode, int productId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productCode))
                    return false;

                return !_uow.ProductRepository.Get().Any(x => x.Id != productId && x.ProductCode == productCode);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<MappedComponentViewModel> GetMappedComponentList(int id)
        {
            try
            {
                var mappedList = (from c in _uow.ComponentRepository.Get(x => x.IsActive).Order(x => x.Name, SortDirection.Asc)
                                  select new MappedComponentViewModel
                                  {
                                      ComponentId = c.Id,
                                      ComponentName = c.Name,
                                      Checked = c.Products.Any(x => x.Id == id)
                                  }).ToList();

                return mappedList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool MapComponent(ProductComponentMappingViewModel vm)
        {
            Product product = GetProduct(vm.ProductId);

            if (product != null)
            {
                foreach (var i in vm.MappedComponentList)
                {
                    Component component = _uow.ComponentRepository.Get(x => x.Id == i.ComponentId && x.IsActive).SingleOrDefault();
                    if (component != null)
                    {
                        if (i.Checked)
                            product.Components.Add(component);

                        if (!i.Checked)
                            product.Components.Remove(component);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            _uow.Save();

            return true;
        }
    }
}
