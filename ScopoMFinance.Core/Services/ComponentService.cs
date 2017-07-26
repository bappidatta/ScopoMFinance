using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using ScopoMFinance.Domain.ViewModels.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IComponentService
    {
        List<DropDownViewModel> GetComponentDropDown(Expression<Func<Component, bool>> filter = null);

        PList<ComponentListViewModel> GetComponentList(
            int pageNumber,
            Expression<Func<Component, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<Component, bool>> filter = null);

        List<ComponentListViewModel> GetComponentList(
            Expression<Func<Component, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<Component, bool>> filter = null);

        ComponentSetupViewModel GetComponentById(int id);

        void CreateComponent(ComponentSetupViewModel vm);

        bool UpdateComponent(ComponentSetupViewModel vm);

        bool DeleteComponent(int id);

        bool IsComponentCodeAvailable(string componentCode, int componentId);
    }

    public class ComponentService : IComponentService
    {
        private UnitOfWork _uow;
        private IUserHelper _userHelper;

        public ComponentService(UnitOfWork uow, IUserHelper userHelper)
        {
            _uow = uow;
            _userHelper = userHelper;
        }

        private Component GetComponent(int id)
        {
            return (from c in _uow.ComponentRepository.Get(x => x.Id == id)
                    select c).SingleOrDefault();
        }

        public List<DropDownViewModel> GetComponentDropDown(Expression<Func<Component, bool>> filter = null)
        {
            var dropDown = from c in _uow.ComponentRepository.Get(filter)
                                   select new DropDownViewModel()
                                   {
                                       Value = c.Id,
                                       Text = c.Name
                                   };

            return dropDown.ToList();
        }

        public PList<ComponentListViewModel> GetComponentList(
            int pageNumber,
            Expression<Func<Component, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<Component, bool>> filter = null)
        {
            PagerSettings psettings = null;
            int pageSize = _userHelper.PagerSize;

            var component = (from c in _uow.ComponentRepository.Get(filter).Order(orderBy, sortDir)
                            select new ComponentListViewModel
                            {
                                Id = c.Id,
                                ComponentCode = c.ComponentCode,
                                Name = c.Name,
                                Duration = c.Duration,
                                ComponentTypeId = c.ComponentTypeId,
                                ComponentTypeName = c.ComponentType.Name,
                                DonorId = c.DonorId,
                                DonorName = c.SysDonor.Name,
                                IsActive = c.IsActive,
                                UserId = c.UserId,
                                SystemDate = c.SystemDate,
                                SetDate = c.SetDate
                            }).Page(pageNumber, pageSize, out psettings);

            return component.ToPList(psettings);
        }

        public List<ComponentListViewModel> GetComponentList(
            Expression<Func<Component, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<Component, bool>> filter = null)
        {
            var component = (from c in _uow.ComponentRepository.Get(filter).Order(orderBy, sortDir)
                             select new ComponentListViewModel
                             {
                                 Id = c.Id,
                                 ComponentCode = c.ComponentCode,
                                 Name = c.Name,
                                 Duration = c.Duration,
                                 ComponentTypeId = c.ComponentTypeId,
                                 ComponentTypeName = c.ComponentType.Name,
                                 DonorId = c.DonorId,
                                 DonorName = c.SysDonor.Name,
                                 IsActive = c.IsActive,
                                 UserId = c.UserId,
                                 SystemDate = c.SystemDate,
                                 SetDate = c.SetDate
                             });

            return component.ToList();
        }

        public ComponentSetupViewModel GetComponentById(int id)
        {
            var component = (from c in _uow.ComponentRepository.Get(x => x.Id == id)
                            select new ComponentSetupViewModel
                            {
                                Id = c.Id,
                                ComponentCode = c.ComponentCode,
                                Name = c.Name,
                                Duration = c.Duration,
                                ComponentTypeId = c.ComponentTypeId,
                                DonorId = c.DonorId,
                                IsActive = c.IsActive,
                                UserId = c.UserId,
                                SystemDate = c.SystemDate,
                                SetDate = c.SetDate
                            });

            return component.SingleOrDefault();
        }

        public void CreateComponent(ComponentSetupViewModel vm)
        {
            Component model = new Component
            {
                ComponentCode = vm.ComponentCode,
                Name = vm.Name,
                Duration = vm.Duration,
                ComponentTypeId = vm.ComponentTypeId,
                DonorId = vm.DonorId,
                IsActive = vm.IsActive,
                UserId = _userHelper.Get().UserId,
                SystemDate = _userHelper.Get().DayOpenClose.SystemDate,
                SetDate = DateTime.Now
            };

            _uow.ComponentRepository.Insert(model);
            _uow.Save();
        }

        public bool UpdateComponent(ComponentSetupViewModel vm)
        {
            Component model = GetComponent(vm.Id);

            if (model == null)
                return false;

            model.ComponentCode = vm.ComponentCode;
            model.Name = vm.Name;
            model.Duration = vm.Duration;
            model.ComponentTypeId = vm.ComponentTypeId;
            model.DonorId = vm.DonorId;
            model.IsActive = vm.IsActive;
            model.UserId = _userHelper.Get().UserId;
            model.SystemDate = _userHelper.Get().DayOpenClose.SystemDate;
            model.SetDate = DateTime.Now;

            _uow.ComponentRepository.Update(model);
            _uow.Save();

            return true;
        }

        public bool DeleteComponent(int id)
        {
            Component model = GetComponent(id);

            if (model == null)
                return false;

            _uow.ComponentRepository.Delete(model);
            _uow.Save();

            return true;
        }

        public bool IsComponentCodeAvailable(string componentCode, int componentId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(componentCode))
                    return false;

                return !_uow.ComponentRepository.Get().Any(x => x.Id != componentId && x.ComponentCode == componentCode);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
