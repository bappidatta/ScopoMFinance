using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using ScopoMFinance.Core.Helpers;
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
        PList<ComponentTypeListViewModel> GetComponentTypeList(int pageNumber, Expression<Func<ComponentType, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc, Expression<Func<ComponentType, bool>> filter = null);
        void CreateComponentType(ComponentTypeSetupViewModel vm);
        bool UpdateComponentType(ComponentTypeSetupViewModel vm);
    }

    public class ComponentTypeService : IComponentTypeService
    {
        private UnitOfWork _uow;
        private IUserHelper _userHelper;

        public ComponentTypeService(UnitOfWork uow, IUserHelper userHelper)
        {
            _uow = uow;
            _userHelper = userHelper;
        }

        private ComponentType GetComponentType(int id)
        {
            return (from c in _uow.ComponentTypeRepository
                                      .Get(x => x.Id == id)
                    select c).SingleOrDefault();
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
            return (from c in _uow.ComponentTypeRepository.Get(x => x.Id == componentTypeId)
                    select new ComponentTypeSetupViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        NoOfMaxLoan = c.NoOfMaxLoan,
                        IsActive = c.IsActive,
                        UserId = c.UserId,
                        SetDate = c.SetDate,
                        SystemDate = c.SystemDate
                    }).SingleOrDefault();
        }

        public PList<ComponentTypeListViewModel> GetComponentTypeList(int pageNumber, Expression<Func<ComponentType, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc, Expression<Func<ComponentType, bool>> filter = null)
        {
            PagerSettings psettings = null;
            int pageSize = _userHelper.PagerSize;

            var componentType = (from c in _uow.ComponentTypeRepository.Get(filter).Order(orderBy, sortDir)
                                select new ComponentTypeListViewModel
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    NoOfMaxLoan = c.NoOfMaxLoan,
                                    IsActive = c.IsActive,
                                    UserId = c.UserId,
                                    SetDate = c.SetDate,
                                    SystemDate = c.SystemDate
                                }).Page(pageNumber, pageSize, out psettings);

            return componentType.ToPList(psettings);
        }
        
        public void CreateComponentType(ComponentTypeSetupViewModel vm)
        {
            ComponentType componentType = new ComponentType
            {

            };

            _uow.ComponentTypeRepository.Insert(componentType);
            _uow.Save();
        }

        public bool UpdateComponentType(ComponentTypeSetupViewModel vm)
        {
            ComponentType model = GetComponentType(vm.Id);

            if (model == null)
                return false;
            
            model.IsActive = vm.IsActive;
            model.NoOfMaxLoan = vm.NoOfMaxLoan;
            model.UserId = _userHelper.Get().UserId;
            model.SystemDate = _userHelper.Get().DayOpenClose.SystemDate;
            model.SetDate = DateTime.Now;

            _uow.ComponentTypeRepository.Update(model);
            _uow.Save();

            return true;
        }
    }
}
