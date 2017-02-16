using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
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
    public interface IBranchService
    {
        List<DropDownHelper> GetBranchDropDown();
        PList<BranchListViewModel> GetBranchList(int pageNumber, int pageSize, SortDirection sortDir, int sortCol);
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

        public List<DropDownHelper> GetBranchDropDown()
        {
            var branchDropDown = from c in _uow.BranchRepository.Get()
                                 where c.Status == true
                                 select new DropDownHelper()
                                 {
                                     Value = c.Id,
                                     Text = c.Name
                                 };

            return branchDropDown.ToList();
        }

        public PList<BranchListViewModel> GetBranchList(int pageNumber, int pageSize, SortDirection sortDir, int sortCol)
        {
            Expression<Func<Branch, object>> orderBy = null;
            PagerSettings psettings = null;

            switch (sortCol)
            {
                case 0:
                default:
                    orderBy = x => x.Name;
                    break;
                case 1:
                    orderBy = x => x.OpenDate;
                    break;
                case 2:
                    orderBy = x => x.IsHeadOffice;
                    break;
                case 7:
                    orderBy = x => x.UserBranches.Count(u => u.UserProfile.IsActive && !u.UserProfile.IsDeleted);
                    break;
            }

            var branchList = (from c in _uow.BranchRepository.Get()
                              .Order(orderBy, sortDir)
                              select new BranchListViewModel()
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                                  OpenDate = c.OpenDate,
                                  IsHeadOffice = c.IsHeadOffice,
                                  Status = c.Status,
                                  OrgCount = c.Organizations.Count(o => o.IsActive && !o.IsDeleted),
                                  COCount = c.Employees.Count(e => e.IsActive && !e.IsDeleted && e.IsCreditOfficer),
                                  UserCount = c.UserBranches.Count(u => u.UserProfile.IsActive && !u.UserProfile.IsDeleted),
                                  ProjectCount = c.BranchWiseProjectMappings.Count(p => p.Project.IsActive && !p.Project.IsDeleted)
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
                Status = vm.Status
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
                Status = vm.Status
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
                        Status = c.Status
                    }).SingleOrDefault();
        }
    }
}
