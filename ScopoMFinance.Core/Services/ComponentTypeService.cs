using NtitasCommon.Core.Common;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using ScopoMFinance.Domain.ViewModels.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IComponentTypeService
    {
        List<DropDownViewModel> GetComponentTypeDropDown();
        ComponentTypeSetupViewModel GetComponentTypeById(int componentTypeId);
        PList<ComponentTypeListViewModel> GetComponentTypeList(int pageNumber, int pageSize, Expression<Func<ComponentType, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc);
        void CreateComponentType(ComponentTypeSetupViewModel vm);
        void UpdateComponentType(ComponentTypeSetupViewModel vm);
    }

    public class ComponentTypeService : IComponentTypeService
    {
        private UnitOfWork _uow;

        public ComponentTypeService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownViewModel> GetComponentTypeDropDown()
        {
            var componentTypeDropDown = from c in _uow.ComponentTypeRepository.Get(x => x.IsActive == true)
                                      select new DropDownViewModel()
                                      {
                                          Value = c.Id,
                                          Text = c.Name
                                      };

            return componentTypeDropDown.ToList();
        }

        public ComponentTypeSetupViewModel GetComponentTypeById(int componentTypeId)
        {
            throw new NotImplementedException();
        }

        public PList<ComponentTypeListViewModel> GetComponentTypeList(int pageNumber, int pageSize, Expression<Func<ComponentType, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc)
        {
            throw new NotImplementedException();
        }
        
        public void CreateComponentType(ComponentTypeSetupViewModel vm)
        {
            ComponentType componentType = new ComponentType
            {

            };

            _uow.ComponentTypeRepository.Insert(componentType);
            _uow.Save();
        }

        public void UpdateComponentType(ComponentTypeSetupViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
