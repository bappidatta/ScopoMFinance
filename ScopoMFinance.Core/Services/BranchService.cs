using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IBranchService
    {
        List<DropDownHelper> GetBranchDropDown();
        List<BranchListViewModel> GetBranchList();
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

        public List<BranchListViewModel> GetBranchList()
        {
            List<BranchListViewModel> branchList = (from c in _uow.BranchRepository.Get()
                                                   select new BranchListViewModel()
                                                   {
                                                       Id = c.Id,
                                                       Name = c.Name,
                                                       OpenDate = c.OpenDate,
                                                       IsHeadOffice = c.IsHeadOffice,
                                                       Status = c.Status,
                                                       OrgCount = c.Organizations.Count(o=>o.IsActive && !o.IsDeleted),
                                                       COCount = c.Employees.Count(e=>e.IsActive && !e.IsDeleted && e.IsCreditOfficer),
                                                       UserCount = c.UserBranches.Count(u=>u.UserProfile.IsActive && !u.UserProfile.IsDeleted),
                                                       ProjectCount = c.BranchWiseProjectMappings.Count(p=>p.Project.IsActive && !p.Project.IsDeleted)
                                                   }).ToList();

            return branchList;
        }
    }
}
