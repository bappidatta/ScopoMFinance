using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IColcOptionService
    {
        List<DropDownViewModel> GetColcOptionDropDown();
    }
    public class ColcOptionService : IColcOptionService
    {
        private UnitOfWork _uow;

        public ColcOptionService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownViewModel> GetColcOptionDropDown()
        {
            var colcOptionDropDown = from c in _uow.ColcOptionRepository.Get(x => x.IsActive == true)
                                 select new DropDownViewModel()
                                 {
                                     Value = c.Id,
                                     Text = c.Name
                                 };

            return colcOptionDropDown.ToList();
        }
    }
}
