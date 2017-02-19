using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IEmployeeTypeService
    {
        List<DropDownHelper> GetEmployeeTypeDropDown();
    }

    public class EmployeeTypeService : IEmployeeTypeService
    {
        private UnitOfWork _uow;

        public EmployeeTypeService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownHelper> GetEmployeeTypeDropDown()
        {
            var employeeTypeDropDown = from c in _uow.EmployeeTypeRepository.Get(x => x.IsActive == true && x.IsDeleted == false)
                                 select new DropDownHelper()
                                 {
                                     Value = c.Id,
                                     Text = c.Name
                                 };

            return employeeTypeDropDown.ToList();
        }
    }
}
