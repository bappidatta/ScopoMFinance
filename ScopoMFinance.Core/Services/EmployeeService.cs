using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Common;
using ScopoMFinance.Domain.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IEmployeeService
    {
        List<DropDownViewModel> GetEmployeeDropDown(Expression<Func<Employee, bool>> filter = null);

        PList<EmployeeListViewModel> GetEmployeeList(
            int pageNumber,
            Expression<Func<Employee, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<Employee, bool>> filter = null);

        List<EmployeeListViewModel> GetEmployeeList(
            Expression<Func<Employee, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<Employee, bool>> filter = null);

        EmployeeEditViewModel GetEmployeeById(int id);

        void CreateEmployee(EmployeeEditViewModel vm);

        bool UpdateEmployee(EmployeeEditViewModel vm);

        bool DeleteEmployee(int id);

        bool IsEmployeeNoAvailable(string employeeNo, int employeeId);
    }

    public class EmployeeService : IEmployeeService
    {
        private UnitOfWork _uow;
        private IUserHelper _userHelper;

        public EmployeeService(UnitOfWork uow, IUserHelper userHelper)
        {
            _uow = uow;
            _userHelper = userHelper;
        }

        private Employee GetEmployee(int id)
        {
            Expression<Func<Employee, bool>> filter = null;

            if (_userHelper.Get().IsHeadOffice)
                filter = x => x.Id == id;
            else
            {
                int branchId = _userHelper.Get().BranchId;
                filter = x => x.Id == id && x.BranchId == branchId;
            }

            return (from c in _uow.EmployeeRepository.Get(filter)
                    select c).SingleOrDefault();
        }

        public List<DropDownViewModel> GetEmployeeDropDown(Expression<Func<Employee, bool>> filter = null)
        {
            var employeeDropDown = from c in _uow.EmployeeRepository.Get(filter)
                                       select new DropDownViewModel()
                                       {
                                           Value = c.Id,
                                           Text = c.EmployeeName
                                       };

            return employeeDropDown.ToList();
        }

        public PList<EmployeeListViewModel> GetEmployeeList(
            int pageNumber,
            Expression<Func<Employee, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<Employee, bool>> filter = null)
        {
            PagerSettings psettings = null;
            int pageSize = _userHelper.PagerSize;

            var employee = (from c in _uow.EmployeeRepository.Get(filter).Order(orderBy, sortDir)
                                select new EmployeeListViewModel
                                {
                                    Id = c.Id,
                                    BranchId = c.BranchId,
                                    Branch = c.Branch.Name,
                                    EmployeeNo = c.EmployeeNo,
                                    EmployeeName = c.EmployeeName,
                                    IsCreditOfficer = c.IsCreditOfficer,
                                    JoiningDate = c.JoiningDate,
                                    ResignDate = c.ResignDate,
                                    GenderId = c.GenderId,
                                    Gender = c.SysGender.Name,
                                    EmployeeTypeId = c.EmployeeTypeId,
                                    EmployeeType = c.EmployeeType.Name,
                                    Address = c.Address,
                                    PhoneNo = c.PhoneNo,
                                    Remarks = c.Remarks,
                                    IsActive = c.IsActive
                                }).Page(pageNumber, pageSize, out psettings);

            return employee.ToPList(psettings);
        }

        public List<EmployeeListViewModel> GetEmployeeList(
            Expression<Func<Employee, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<Employee, bool>> filter = null)
        {
            var employee = (from c in _uow.EmployeeRepository.Get(filter).Order(orderBy, sortDir)
                            select new EmployeeListViewModel
                            {
                                Id = c.Id,
                                BranchId = c.BranchId,
                                Branch = c.Branch.Name,
                                EmployeeNo = c.EmployeeNo,
                                EmployeeName = c.EmployeeName,
                                IsCreditOfficer = c.IsCreditOfficer,
                                JoiningDate = c.JoiningDate,
                                ResignDate = c.ResignDate,
                                GenderId = c.GenderId,
                                Gender = c.SysGender.Name,
                                EmployeeTypeId = c.EmployeeTypeId,
                                EmployeeType = c.EmployeeType.Name,
                                Address = c.Address,
                                PhoneNo = c.PhoneNo,
                                Remarks = c.Remarks,
                                IsActive = c.IsActive
                            });

            return employee.ToList();
        }

        public EmployeeEditViewModel GetEmployeeById(int id)
        {
            var employee = (from c in _uow.EmployeeRepository.Get(x => x.Id == id)
                            select new EmployeeEditViewModel
                            {
                                Id = c.Id,
                                BranchId = c.BranchId,
                                EmployeeNo = c.EmployeeNo,
                                EmployeeName = c.EmployeeName,
                                IsCreditOfficer = c.IsCreditOfficer,
                                JoiningDate = c.JoiningDate,
                                ResignDate = c.ResignDate,
                                GenderId = c.GenderId,
                                EmployeeTypeId = c.EmployeeTypeId,
                                Address = c.Address,
                                PhoneNo = c.PhoneNo,
                                Remarks = c.Remarks,
                                IsActive = c.IsActive
                            });

            return employee.SingleOrDefault();
        }

        public void CreateEmployee(EmployeeEditViewModel vm)
        {
            int branchId = 0;
            if (_userHelper.Get().IsHeadOffice)
                branchId = vm.BranchId;
            else
                branchId = _userHelper.Get().BranchId;

            Employee model = new Employee
            {
                BranchId = branchId,
                EmployeeNo = vm.EmployeeNo,
                EmployeeName = vm.EmployeeName,
                IsCreditOfficer = vm.IsCreditOfficer,
                JoiningDate = vm.JoiningDate,
                GenderId = vm.GenderId,
                EmployeeTypeId = vm.EmployeeTypeId,
                Address = vm.Address,
                PhoneNo = vm.PhoneNo,
                Remarks = vm.Remarks,
                IsActive = vm.IsActive,
                UserId = _userHelper.Get().UserId,
                SystemDate = _userHelper.Get().DayOpenClose.SystemDate,
                SetDate = DateTime.Now
            };

            _uow.EmployeeRepository.Insert(model);
            _uow.Save();
        }

        public bool UpdateEmployee(EmployeeEditViewModel vm)
        {
            Employee model = GetEmployee(vm.Id);

            if (model == null)
                return false;

            model.EmployeeNo = vm.EmployeeNo;
            model.EmployeeName = vm.EmployeeName;
            model.IsCreditOfficer = vm.IsCreditOfficer;
            model.JoiningDate = vm.JoiningDate;
            model.GenderId = vm.GenderId;
            model.EmployeeTypeId = vm.EmployeeTypeId;
            model.Address = vm.Address;
            model.PhoneNo = vm.PhoneNo;
            model.Remarks = vm.Remarks;
            model.IsActive = vm.IsActive;
            model.UserId = _userHelper.Get().UserId;
            model.SystemDate = _userHelper.Get().DayOpenClose.SystemDate;
            model.SetDate = DateTime.Now;

            _uow.EmployeeRepository.Update(model);
            _uow.Save();

            return true;
        }

        public bool DeleteEmployee(int id)
        {
            Employee model = GetEmployee(id);

            if (model == null)
                return false;

            _uow.EmployeeRepository.Delete(model);
            _uow.Save();

            return true;
        }

        public bool IsEmployeeNoAvailable(string employeeNo, int employeeId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(employeeNo))
                    return false;

                return !_uow.EmployeeRepository.Get().Any(x => x.Id != employeeId && x.EmployeeNo == employeeNo);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
