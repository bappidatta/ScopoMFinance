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
        PList<OrganizationListViewModel> GetOrganizationList(int pageNumber, int pageSize, SortDirection sortDir, int sortCol, Expression<Func<Organization, bool>> filter = null);
    }

    public class OrganizationService : IOrganizationService
    {
        private UnitOfWork _uow;

        public OrganizationService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public PList<OrganizationListViewModel> GetOrganizationList(int pageNumber, int pageSize, SortDirection sortDir, int sortCol, Expression<Func<Organization, bool>> filter = null)
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
                                   LoanColcOption = c.SysColcOption1.Name,
                                   SavColcOptionId = c.SavColcOption,
                                   SavColcOption = c.SysColcOption.Name,
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
    }
}
