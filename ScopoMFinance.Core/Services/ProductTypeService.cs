using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IProductTypeService
    {
        List<DropDownViewModel> GetProductTypeDropDown();
    }

    public class ProductTypeService : IProductTypeService
    {
        private UnitOfWork _uow;

        public ProductTypeService(UnitOfWork uow)
        {
            _uow = uow;
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
    }
}
