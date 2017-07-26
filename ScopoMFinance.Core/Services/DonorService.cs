using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IDonorService
    {
        List<DropDownViewModel> GetDonorDropDown();
    }

    public class DonorService : IDonorService
    {
        private UnitOfWork _uow;

        public DonorService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownViewModel> GetDonorDropDown()
        {
            var dropDown = from c in _uow.DonorRepository.Get(x => x.IsActive == true)
                                 select new DropDownViewModel()
                                 {
                                     Value = c.Id,
                                     Text = c.Name
                                 };

            return dropDown.ToList();
        }
    }
}
