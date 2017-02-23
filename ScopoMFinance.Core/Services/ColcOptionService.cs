using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IColcOptionService
    {
        List<DropDownHelper> GetColcOptionDropDown();
    }
    public class ColcOptionService : IColcOptionService
    {
        private UnitOfWork _uow;

        public ColcOptionService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownHelper> GetColcOptionDropDown()
        {
            var colcOptionDropDown = from c in _uow.ColcOptionRepository.Get(x => x.IsActive == true)
                                 select new DropDownHelper()
                                 {
                                     Value = c.Id,
                                     Text = c.Name
                                 };

            return colcOptionDropDown.ToList();
        }
    }
}
