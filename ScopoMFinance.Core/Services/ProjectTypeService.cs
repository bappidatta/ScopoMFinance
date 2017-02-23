using NtitasCommon.Core.Common;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IProjectTypeService
    {
        List<DropDownHelper> GetProjectTypeDropDown();
        ProjectTypeSetupViewModel GetProjectTypeById(int projectTypeId);
        PList<ProjectTypeListViewModel> GetProjectTypeList(int pageNumber, int pageSize, Expression<Func<ProjectType, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc);
        void CreateProjectType(ProjectTypeSetupViewModel vm);
        void UpdateProjectType(ProjectTypeSetupViewModel vm);
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
            var projectTypeDropDown = from c in _uow.ProjectTypeRepository.Get(x => x.IsActive == true)
                                      select new DropDownHelper()
                                      {
                                          Value = c.Id,
                                          Text = c.Name
                                      };

            return projectTypeDropDown.ToList();
        }

        public ProjectTypeSetupViewModel GetProjectTypeById(int projectTypeId)
        {
            throw new NotImplementedException();
        }

        public PList<ProjectTypeListViewModel> GetProjectTypeList(int pageNumber, int pageSize, Expression<Func<ProjectType, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc)
        {
            throw new NotImplementedException();
        }
        
        public void CreateProjectType(ProjectTypeSetupViewModel vm)
        {
            ProjectType projectType = new ProjectType
            {

            };

            _uow.ProjectTypeRepository.Insert(projectType);
            _uow.Save();
        }

        public void UpdateProjectType(ProjectTypeSetupViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
