using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IGenderService
    {
        List<DropDownViewModel> GetGenderDropDown();
    }

    public class GenderService : IGenderService
    {
        private UnitOfWork _uow;

        public GenderService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownViewModel> GetGenderDropDown()
        {
            var genderDropDown = from c in _uow.GenderRepository.Get(x => x.IsActive == true)
                                 select new DropDownViewModel()
                                 {
                                     Value = c.Id,
                                     Text = c.Name
                                 };

            return genderDropDown.ToList();
        }
    }
}
