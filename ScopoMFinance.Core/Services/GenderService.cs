using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IGenderService
    {
        List<DropDownHelper> GetGenderDropDown();
    }

    public class GenderService : IGenderService
    {
        private UnitOfWork _uow;

        public GenderService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownHelper> GetGenderDropDown()
        {
            var genderDropDown = from c in _uow.GenderRepository.Get(x => x.IsActive == true && x.IsDeleted == false)
                                 select new DropDownHelper()
                                 {
                                     Value = c.Id,
                                     Text = c.Name
                                 };

            return genderDropDown.ToList();
        }
    }
}
