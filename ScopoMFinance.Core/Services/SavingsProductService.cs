using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface ISavingsProductService
    {
        List<DropDownViewModel> GetSavingsProductDropDown();
    }

    public class SavingsProductService : ISavingsProductService
    {
        private UnitOfWork _uow;

        public SavingsProductService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownViewModel> GetSavingsProductDropDown()
        {
            var savingsProductDropDown = from c in _uow.SavingsProductRepository.Get(x => x.IsActive == true)
                                      select new DropDownViewModel()
                                      {
                                          Value = c.Id,
                                          Text = "(" + c.ProductCode + ") " + c.ProductName
                                      };

            return savingsProductDropDown.ToList();
        }
    }
}
