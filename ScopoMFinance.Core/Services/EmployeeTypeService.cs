using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IEmployeeTypeService
    {
        List<DropDownHelper> GetEmployeeTypeDropDown();

        PList<EmployeeTypeListViewModel> GetEmployeeTypeList(
            int pageNumber,
            Expression<Func<EmployeeType, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<EmployeeType, bool>> filter = null);

        List<EmployeeTypeListViewModel> GetEmployeeTypeList(
            Expression<Func<EmployeeType, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<EmployeeType, bool>> filter = null);

        EmployeeTypeEditViewModel GetEmployeeTypeById(int id);

        void CreateEmployeeType(EmployeeTypeEditViewModel vm);
        bool UpdateEmployeeType(EmployeeTypeEditViewModel vm);
        bool DeleteEmployeeType(int id);
    }

    public class EmployeeTypeService : IEmployeeTypeService
    {
        private UnitOfWork _uow;
        private IUserHelper _userHelper;

        public EmployeeTypeService(UnitOfWork uow, IUserHelper userHelper)
        {
            _uow = uow;
            _userHelper = userHelper;
        }

        private EmployeeType GetEmployeeType(int id)
        {
            return (from c in _uow.EmployeeTypeRepository
                                      .Get(x => x.Id == id && x.IsDeleted == false)
                    select c).SingleOrDefault();
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

        public PList<EmployeeTypeListViewModel> GetEmployeeTypeList(
            int pageNumber,
            Expression<Func<EmployeeType, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<EmployeeType, bool>> filter = null)
        {
            PagerSettings psettings = null;
            int pageSize = _userHelper.PagerSize;

            var employeeType = (from c in _uow.EmployeeTypeRepository.Get(filter).Order(orderBy, sortDir)
                                select new EmployeeTypeListViewModel
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    IsActive = c.IsActive,
                                    IsDeleted = c.IsDeleted,
                                    UserId = c.UserId,
                                    SetDate = c.SetDate,
                                    SystemDate = c.SystemDate
                                }).Page(pageNumber, pageSize, out psettings);

            return employeeType.ToPList(psettings);
        }

        public List<EmployeeTypeListViewModel> GetEmployeeTypeList(
            Expression<Func<EmployeeType, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<EmployeeType, bool>> filter = null)
        {
            var employeeType = (from c in _uow.EmployeeTypeRepository.Get(filter).Order(orderBy, sortDir)
                                select new EmployeeTypeListViewModel
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    IsActive = c.IsActive,
                                    IsDeleted = c.IsDeleted,
                                    UserId = c.UserId,
                                    SetDate = c.SetDate,
                                    SystemDate = c.SystemDate
                                });

            return employeeType.ToList();
        }

        public EmployeeTypeEditViewModel GetEmployeeTypeById(int id)
        {
            return (from c in _uow.EmployeeTypeRepository.Get(x=>x.Id == id && x.IsDeleted == false)
                    select new EmployeeTypeEditViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        IsActive = c.IsActive,
                        IsDeleted = c.IsDeleted,
                        UserId = c.UserId,
                        SetDate = c.SetDate,
                        SystemDate = c.SystemDate
                    }).SingleOrDefault();
        }

        public void CreateEmployeeType(EmployeeTypeEditViewModel vm)
        {
            EmployeeType model = new EmployeeType
            {
                Name = vm.Name,
                IsActive = vm.IsActive,
                UserId = _userHelper.Get().UserId,
                SystemDate = _userHelper.Get().DayOpenClose.SystemDate,
                SetDate = DateTime.Now
            };

            _uow.EmployeeTypeRepository.Insert(model);
            _uow.Save();
        }

        public bool UpdateEmployeeType(EmployeeTypeEditViewModel vm)
        {
            EmployeeType model = GetEmployeeType(vm.Id);

            if (model == null)
                return false;

            model.Name = vm.Name;
            model.IsActive = vm.IsActive;
            model.UserId = _userHelper.Get().UserId;
            model.SystemDate = _userHelper.Get().DayOpenClose.SystemDate;
            model.SetDate = DateTime.Now;

            _uow.EmployeeTypeRepository.Update(model);
            _uow.Save();

            return true;
        }

        public bool DeleteEmployeeType(int id)
        {
            EmployeeType model = GetEmployeeType(id);

            if (model == null)
                return false;

            _uow.EmployeeTypeRepository.Delete(model);
            _uow.Save();

            return true;
        }
    }
}
