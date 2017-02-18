using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Org;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IOrganizationService
    {
        PList<OrganizationListViewModel> GetOrganizationList(
            int pageNumber, int pageSize, 
            SortDirection sortDir, int sortCol, 
            Expression<Func<Organization, bool>> filter = null);

        OrganizationEditViewModel GetOrganizationById(int orgId);

        void SaveOrganization(OrganizationEditViewModel vm);
        void UpdateOrganization(OrganizationEditViewModel vm);
    }

    public class OrganizationService : IOrganizationService
    {
        private UnitOfWork _uow;

        public OrganizationService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public PList<OrganizationListViewModel> GetOrganizationList(
            int pageNumber, int pageSize, 
            SortDirection sortDir, int sortCol, 
            Expression<Func<Organization, bool>> filter = null)
        {
            Expression<Func<Organization, object>> orderBy = null;
            PagerSettings psettings = null;

            switch (sortCol)
            {
                case 0:
                default:
                    orderBy = x => x.OrgCategory.CategoryName;
                    break;
                case 1:
                    orderBy = x => x.OrganizationNo;
                    break;
                case 2:
                    orderBy = x => x.OrganizationName;
                    break;
                case 3:
                    orderBy = x => x.SysGender.Name;
                    break;
                case 4:
                    orderBy = x => x.SetupDate;
                    break;
                case 5:
                    orderBy = x => x.SysColcOptionLoan.Name;
                    break;
                case 6:
                    orderBy = x => x.SysColcOptionSavings.Name;
                    break;
                case 7:
                    orderBy = x => x.FirstLoanColcDate;
                    break;
                case 8:
                    orderBy = x => x.FirstSavColcDate;
                    break;
            }

            var orgList = (from c in _uow.OrganizationRepository.Get(filter)
                               .Order(orderBy, sortDir)
                               select new OrganizationListViewModel{
                                   Id = c.Id,
                                   BranchId = c.BranchId,
                                   OrganizationNo = c.OrganizationNo,
                                   OrganizationName = c.OrganizationName,
                                   OrgCategoryId = c.OrgCategoryId,
                                   OrgCategoryName = c.OrgCategory.CategoryName,
                                   GenderId = c.GenderId,
                                   Gender = c.SysGender.Name,
                                   SetupDate = c.SetupDate,
                                   LoanColcOptionId = c.LoanColcOption,
                                   LoanColcOption = c.SysColcOptionLoan.Name,
                                   SavColcOptionId = c.SavColcOption,
                                   SavColcOption = c.SysColcOptionSavings.Name,
                                   FirstLoanColcDate = c.FirstLoanColcDate,
                                   FirstSavColcDate = c.FirstSavColcDate,
                                   VillageId = c.VillageId,
                                   Village = c.SysVillage.Name,
                                   IsActive = c.IsActive,
                                   IsDeleted = c.IsDeleted,
                                   SystemDate = c.SystemDate,
                                   CreatedBy = c.CreatedBy,
                                   CreatedOn = c.CreatedOn,
                                   UpdatedBy = c.UpdatedBy,
                                   UpdatedOn = c.UpdatedOn
                               }).Page(pageNumber, pageSize, out psettings);

            return orgList.ToPList(psettings);
        }

        public OrganizationEditViewModel GetOrganizationById(int orgId)
        {
            return (from c in _uow.OrganizationRepository.Get()
                    where c.Id == orgId
                    select new OrganizationEditViewModel
                    {
                        Id = c.Id,
                        OrganizationNo = c.OrganizationNo,
                        OrganizationName = c.OrganizationName,
                        OrgCategoryId = c.OrgCategoryId,
                        GenderId = c.GenderId,
                        SetupDate = c.SetupDate,
                        LoanColcOptionId = c.LoanColcOption,
                        SavColcOptionId = c.SavColcOption,
                        FirstLoanColcDate = c.FirstLoanColcDate,
                        FirstSavColcDate = c.FirstSavColcDate,
                        VillageId = c.VillageId
                    }).SingleOrDefault();
        }

        public void SaveOrganization(OrganizationEditViewModel vm)
        {
            Organization org = new Organization
            {
                BranchId = vm.BranchId,
                OrganizationNo = vm.OrganizationNo,
                OrganizationName = vm.OrganizationName,
                OrgCategoryId = vm.OrgCategoryId,
                GenderId = vm.GenderId,
                SetupDate = vm.SetupDate,
                LoanColcOption = vm.LoanColcOptionId,
                SavColcOption = vm.SavColcOptionId,
                FirstLoanColcDate = vm.FirstLoanColcDate,
                FirstSavColcDate = vm.FirstSavColcDate,
                IsActive = vm.IsActive,
                IsDeleted = false,
                SystemDate = vm.SetupDate,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.Now
            };

            _uow.OrganizationRepository.Insert(org);
            _uow.Save();
        }

        public void UpdateOrganization(OrganizationEditViewModel vm)
        {
 
        }
    }
}
