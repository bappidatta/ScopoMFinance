using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IProductTypeService
    {
        List<DropDownHelper> GetProductTypeDropDown();
    }

    public class ProductTypeService : IProductTypeService
    {
        private UnitOfWork _uow;

        public ProductTypeService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownHelper> GetProductTypeDropDown()
        {
            var productTypeDropDown = from c in _uow.ProductTypeRepository.Get(x => x.IsActive == true && x.IsDeleted == false)
                                      select new DropDownHelper()
                                      {
                                          Value = c.Id,
                                          Text = c.Name
                                      };

            return productTypeDropDown.ToList();
        }
    }
}
