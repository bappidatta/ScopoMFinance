using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IProjectTypeService
    {
        List<DropDownHelper> GetProjectTypeDropDown();
    }

    public class ProjectTypeService : IProjectTypeService
    {
        private UnitOfWork _uow;

        public ProjectTypeService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownHelper> GetProjectTypeDropDown()
        {
            var projectTypeDropDown = from c in _uow.ProjectTypeRepository.Get(x => x.IsActive == true && x.IsDeleted == false)
                                       select new DropDownHelper()
                                       {
                                           Value = c.Id,
                                           Text = c.Name
                                       };

            return projectTypeDropDown.ToList();
        }
    }
}
