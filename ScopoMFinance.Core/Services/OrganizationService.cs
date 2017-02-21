﻿using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using ScopoMFinance.Core.Helpers;
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
            int pageNumber,
            Expression<Func<Organization, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<Organization, bool>> filter = null);

        OrganizationEditViewModel GetOrganizationById(int orgId);

        void CreateOrganization(OrganizationEditViewModel vm);
        void UpdateOrganization(OrganizationEditViewModel vm);
        void DeleteOrganization(int orgId);
        bool IsOrgNoAvailable(string orgNo, int orgId);
    }

    public class OrganizationService : IOrganizationService
    {
        private UnitOfWork _uow;
        private IUserHelper _userHelper;

        public OrganizationService(UnitOfWork uow, IUserHelper userHelper)
        {
            _uow = uow;
            _userHelper = userHelper;
        }

        private Organization GetOrganization(int id)
        {
            int branchId = _userHelper.Get().BranchId;

            return (from c in _uow.OrganizationRepository
                                      .Get(x => x.Id == id && x.BranchId == branchId && x.IsDeleted == false)
                    select c).SingleOrDefault();
        }

        public PList<OrganizationListViewModel> GetOrganizationList(
            int pageNumber,
            Expression<Func<Organization, object>> orderBy = null,
            SortDirection sortDir = SortDirection.Asc,
            Expression<Func<Organization, bool>> filter = null)
        {
            PagerSettings psettings = null;
            int pageSize = _userHelper.PagerSize;

            var orgList = (from c in _uow.OrganizationRepository.Get(filter)
                               .Order(orderBy, sortDir)
                           select new OrganizationListViewModel
                           {
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
                               UserId = c.UserId,
                               SetDate = c.SetDate
                           }).Page(pageNumber, pageSize, out psettings);

            return orgList.ToPList(psettings);
        }

        public OrganizationEditViewModel GetOrganizationById(int orgId)
        {
            int branchId = _userHelper.Get().BranchId;

            return (from c in _uow.OrganizationRepository.Get(x => x.Id == orgId && x.BranchId == branchId && x.IsDeleted == false)
                    select new OrganizationEditViewModel
                    {
                        Id = c.Id,
                        BranchId = c.BranchId,
                        OrganizationNo = c.OrganizationNo,
                        OrganizationName = c.OrganizationName,
                        OrgCategoryId = c.OrgCategoryId,
                        GenderId = c.GenderId,
                        SetupDate = c.SetupDate,
                        LoanColcOptionId = c.LoanColcOption,
                        SavColcOptionId = c.SavColcOption,
                        FirstLoanColcDate = c.FirstLoanColcDate,
                        FirstSavColcDate = c.FirstSavColcDate,
                        VillageId = c.VillageId,
                        IsActive = c.IsActive
                    }).SingleOrDefault();
        }

        public void CreateOrganization(OrganizationEditViewModel vm)
        {
            Organization org = new Organization
            {
                BranchId = _userHelper.Get().BranchId,
                OrganizationNo = vm.OrganizationNo,
                OrganizationName = vm.OrganizationName,
                OrgCategoryId = vm.OrgCategoryId,
                GenderId = vm.GenderId,
                SetupDate = _userHelper.Get().DayOpenClose.SystemDate,
                LoanColcOption = vm.LoanColcOptionId,
                SavColcOption = vm.SavColcOptionId,
                FirstLoanColcDate = vm.FirstLoanColcDate,
                FirstSavColcDate = vm.FirstSavColcDate,
                IsActive = vm.IsActive,
                IsDeleted = false,
                SystemDate = _userHelper.Get().DayOpenClose.SystemDate,
                UserId = _userHelper.Get().UserId,
                SetDate = DateTime.Now
            };

            _uow.OrganizationRepository.Insert(org);
            _uow.Save();
        }

        public void UpdateOrganization(OrganizationEditViewModel vm)
        {
            Organization model = GetOrganization(vm.Id);

            model.OrgCategoryId = vm.OrgCategoryId;
            model.GenderId = vm.GenderId;
            model.OrganizationNo = vm.OrganizationNo;
            model.OrganizationName = vm.OrganizationName;
            model.IsActive = vm.IsActive;
            model.LoanColcOption = vm.LoanColcOptionId;
            model.SavColcOption = vm.SavColcOptionId;
            model.FirstSavColcDate = vm.FirstSavColcDate;
            model.FirstLoanColcDate = vm.FirstLoanColcDate;
            model.UserId = _userHelper.Get().UserId;
            model.SetDate = DateTime.Now;
            model.SystemDate = _userHelper.Get().DayOpenClose.SystemDate;

            _uow.OrganizationRepository.Update(model);
            _uow.Save();
        }

        public void DeleteOrganization(int orgId)
        {
            Organization model = GetOrganization(orgId);

            model.IsDeleted = true;

            _uow.OrganizationRepository.Update(model);
            _uow.Save();
        }

        public bool IsOrgNoAvailable(string orgNo, int orgId)
        {
            int branchId = _userHelper.Get().BranchId;

            try
            {
                if (string.IsNullOrWhiteSpace(orgNo))
                    return false;

                return !_uow.OrganizationRepository.Get().Any(x => x.Id != orgId && x.BranchId == branchId && x.OrganizationNo == orgNo && x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
