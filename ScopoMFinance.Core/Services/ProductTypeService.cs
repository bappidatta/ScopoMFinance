using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using ScopoMFinance.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IProductTypeService
    {
        List<DropDownViewModel> GetProductTypeDropDown();
        ProductTypeSetupViewModel GetProductTypeById(int productTypeId);
        PList<ProductTypeListViewModel> GetProductTypeList(int pageNumber, Expression<Func<ProductType, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc, Expression<Func<ProductType, bool>> filter = null);
        List<ProductTypeListViewModel> GetProductTypeList(Expression<Func<ProductType, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc, Expression<Func<ProductType, bool>> filter = null);
        void CreateProductType(ProductTypeSetupViewModel vm);
        bool UpdateProductType(ProductTypeSetupViewModel vm);
        bool IsProductTypeActive(int productTypeId);
    }

    public class ProductTypeService : IProductTypeService
    {
        private UnitOfWork _uow;
        private IUserHelper _userHelper;

        public ProductTypeService(UnitOfWork uow, IUserHelper userHelper)
        {
            _uow = uow;
            _userHelper = userHelper;
        }

        private ProductType GetProductType(int id)
        {
            return (from c in _uow.ProductTypeRepository
                                      .Get(x => x.Id == id)
                    select c).SingleOrDefault();
        }

        public List<DropDownViewModel> GetProductTypeDropDown()
        {
            var productTypeDropDown = from c in _uow.ProductTypeRepository.Get(x => x.IsActive == true)
                                      select new DropDownViewModel()
                                      {
                                          Value = c.Id,
                                          Text = c.Name
                                      };

            return productTypeDropDown.ToList();
        }

        public ProductTypeSetupViewModel GetProductTypeById(int productTypeId)
        {
            return (from c in _uow.ProductTypeRepository.Get(x => x.Id == productTypeId)
                    select new ProductTypeSetupViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        IsActive = c.IsActive,
                        UserId = c.UserId,
                        SetDate = c.SetDate,
                        SystemDate = c.SystemDate
                    }).SingleOrDefault();
        }

        public PList<ProductTypeListViewModel> GetProductTypeList(int pageNumber, Expression<Func<ProductType, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc, Expression<Func<ProductType, bool>> filter = null)
        {
            PagerSettings psettings = null;
            int pageSize = _userHelper.PagerSize;

            var productType = (from c in _uow.ProductTypeRepository.Get(filter).Order(orderBy, sortDir)
                               select new ProductTypeListViewModel
                               {
                                   Id = c.Id,
                                   Name = c.Name,
                                   IsActive = c.IsActive,
                                   UserId = c.UserId,
                                   SetDate = c.SetDate,
                                   SystemDate = c.SystemDate
                               }).Page(pageNumber, pageSize, out psettings);

            return productType.ToPList(psettings);
        }

        public List<ProductTypeListViewModel> GetProductTypeList(Expression<Func<ProductType, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc, Expression<Func<ProductType, bool>> filter = null)
        {
            var productType = (from c in _uow.ProductTypeRepository.Get(filter).Order(orderBy, sortDir)
                               select new ProductTypeListViewModel
                               {
                                   Id = c.Id,
                                   Name = c.Name,
                                   IsActive = c.IsActive,
                                   UserId = c.UserId,
                                   SetDate = c.SetDate,
                                   SystemDate = c.SystemDate
                               });

            return productType.ToList();
        }

        public void CreateProductType(ProductTypeSetupViewModel vm)
        {
            ProductType productType = new ProductType
            {
                Name = vm.Name,
                IsActive = vm.IsActive,
                UserId = _userHelper.Get().UserId,
                SystemDate = _userHelper.Get().DayOpenClose.SystemDate,
                SetDate = DateTime.Now
            };

            _uow.ProductTypeRepository.Insert(productType);
            _uow.Save();
        }

        public bool UpdateProductType(ProductTypeSetupViewModel vm)
        {
            ProductType model = GetProductType(vm.Id);

            if (model == null)
                return false;

            model.Name = vm.Name;
            model.IsActive = vm.IsActive;
            model.UserId = _userHelper.Get().UserId;
            model.SystemDate = _userHelper.Get().DayOpenClose.SystemDate;
            model.SetDate = DateTime.Now;

            _uow.ProductTypeRepository.Update(model);
            _uow.Save();

            return true;
        }

        public bool IsProductTypeActive(int productTypeId)
        {
            ProductType model = GetProductType(productTypeId);

            if (model == null)
                return false;

            if (model.IsActive)
                return true;

            return false;
        }
    }
}
