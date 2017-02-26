using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
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
    public interface IBranchService
    {
        List<DropDownViewModel> GetBranchDropDown();
        PList<BranchListViewModel> GetBranchList(int pageNumber, int pageSize, Expression<Func<Branch, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc);
        void SaveBranch(BranchEditViewModel vm);
        void UpdateBranch(BranchEditViewModel vm);
        BranchEditViewModel GetBranchById(int branchId);
    }

    public class BranchService : IBranchService
    {
        private UnitOfWork _uow;

        public BranchService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DropDownViewModel> GetBranchDropDown()
        {
            var branchDropDown = from c in _uow.BranchRepository.Get(c => c.IsActive == true)
                                 select new DropDownViewModel()
                                 {
                                     Value = c.Id,
                                     Text = c.Name
                                 };

            return branchDropDown.ToList();
        }

        public PList<BranchListViewModel> GetBranchList(int pageNumber, int pageSize, Expression<Func<Branch, object>> orderBy = null, SortDirection sortDir = SortDirection.Asc)
        {
            PagerSettings psettings = null;

            IQueryable<BranchListViewModel> branchList = (from c in _uow.BranchRepository.Get()
                              .Order(orderBy, sortDir)
                              select new BranchListViewModel()
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                                  OpenDate = c.OpenDate,
                                  IsHeadOffice = c.IsHeadOffice,
                                  IsActive = c.IsActive,
                                  OrgCount = c.Organizations.Count(o => o.IsActive),
                                  COCount = c.Employees.Count(e => e.IsActive && e.IsCreditOfficer),
                                  UserCount = c.UserBranches.Count(u => u.UserProfile.IsActive),
                                  ComponentCount = c.Components.Count(p => p.IsActive)
                              }).Page(pageNumber, pageSize, out psettings);

            return branchList.ToPList(psettings);
        }

        public void SaveBranch(BranchEditViewModel vm)
        {
            Branch branch = new Branch
            {
                Name = vm.Name,
                OpenDate = vm.OpenDate,
                IsHeadOffice = vm.IsHeadOffice,
                IsActive = vm.IsActive
            };

            _uow.BranchRepository.Insert(branch);
            _uow.Save();
        }

        public void UpdateBranch(BranchEditViewModel vm) 
        {
            Branch branch = new Branch
            {
                Id = vm.Id,
                Name = vm.Name,
                OpenDate = vm.OpenDate,
                IsHeadOffice = vm.IsHeadOffice,
                IsActive = vm.IsActive
            };

            _uow.BranchRepository.Update(branch);
            _uow.Save();
        }

        public BranchEditViewModel GetBranchById(int branchId) 
        {
            return (from c in _uow.BranchRepository.Get()
                    where c.Id == branchId
                    select new BranchEditViewModel {
                        Id = c.Id,
                        IsHeadOffice = c.IsHeadOffice,
                        Name = c.Name,
                        OpenDate = c.OpenDate,
                        IsActive = c.IsActive
                    }).SingleOrDefault();
        }
    }
}
