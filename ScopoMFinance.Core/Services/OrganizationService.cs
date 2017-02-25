using NtitasCommon.Core.Common;
using NtitasCommon.Core.Helpers;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.Org;
using ScopoMFinance.Localization;
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

        List<MappedOrganizationViewModel> GetMappedOrganizationList(int creditOfficerId);
        bool MapCreditOfficer(OrgCOMappingViewModel vm, out string validationMessage);
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
                                      .Get(x => x.Id == id && x.BranchId == branchId)
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
                               MeetingFrequencyId = c.MeetingFrequency,
                               MeetingFrequency = c.SysColcOption.Name,
                               MeetingDate = c.MeetingDate,
                               VillageId = c.VillageId,
                               Village = c.SysVillage.Name,
                               IsActive = c.IsActive,
                               CreditOfficer = c.Employee.EmployeeName,
                               SystemDate = c.SystemDate,
                               UserId = c.UserId,
                               SetDate = c.SetDate
                           }).Page(pageNumber, pageSize, out psettings);

            return orgList.ToPList(psettings);
        }

        public OrganizationEditViewModel GetOrganizationById(int orgId)
        {
            int branchId = _userHelper.Get().BranchId;

            return (from c in _uow.OrganizationRepository.Get(x => x.Id == orgId && x.BranchId == branchId)
                    select new OrganizationEditViewModel
                    {
                        Id = c.Id,
                        BranchId = c.BranchId,
                        OrganizationNo = c.OrganizationNo,
                        OrganizationName = c.OrganizationName,
                        OrgCategoryId = c.OrgCategoryId,
                        GenderId = c.GenderId,
                        SetupDate = c.SetupDate,
                        MeetingFrequencyId = c.MeetingFrequency,
                        MeetingDate = c.MeetingDate,
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
                MeetingFrequency = vm.MeetingFrequencyId,
                MeetingDate = vm.MeetingDate,
                IsActive = vm.IsActive,
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
            model.MeetingFrequency = vm.MeetingFrequencyId;
            model.MeetingDate = vm.MeetingDate;
            model.UserId = _userHelper.Get().UserId;
            model.SetDate = DateTime.Now;
            model.SystemDate = _userHelper.Get().DayOpenClose.SystemDate;

            _uow.OrganizationRepository.Update(model);
            _uow.Save();
        }

        public void DeleteOrganization(int orgId)
        {
            Organization model = GetOrganization(orgId);

            _uow.OrganizationRepository.Delete(model);
            _uow.Save();
        }

        public bool IsOrgNoAvailable(string orgNo, int orgId)
        {
            int branchId = _userHelper.Get().BranchId;

            try
            {
                if (string.IsNullOrWhiteSpace(orgNo))
                    return false;

                return !_uow.OrganizationRepository.Get().Any(x => x.Id != orgId && x.BranchId == branchId && x.OrganizationNo == orgNo);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<MappedOrganizationViewModel> GetMappedOrganizationList(int creditOfficerId)
        {
            int branchId = _userHelper.Get().BranchId;

            try
            {
                //var mappedList = (from c in _uow.OrganizationRepository.Get(x => x.BranchId == branchId && x.OrgCreditOfficers.All(i => i.BranchId == branchId && i.EmployeeId == creditOfficerId))
                //                    .Order(x => x.OrganizationNo, SortDirection.Asc)
                //                  select new MappedOrganizationViewModel
                //                  {
                //                      OrganizationId = c.Id,
                //                      OrganizationName = "(" + c.OrganizationNo + ") " + c.OrganizationName,
                //                      Checked = c.OrgCreditOfficers.Any(i => i.BranchId == branchId && i.EmployeeId == creditOfficerId)
                //                  }).ToList();

                var mappedList = (from c in _uow.OrganizationRepository.Get(x => x.BranchId == branchId && x.IsActive && (x.CreditOfficerId == creditOfficerId || x.CreditOfficerId == null))
                                    .Order(x => x.OrganizationNo, SortDirection.Asc)
                                  select new MappedOrganizationViewModel
                                  {
                                      OrganizationId = c.Id,
                                      OrganizationName = "(" + c.OrganizationNo + ") " + c.OrganizationName,
                                      Checked = c.CreditOfficerId != null
                                  }).ToList();

                return mappedList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool MapCreditOfficer(OrgCOMappingViewModel vm, out string validationMessage)
        {
            int branchId = _userHelper.Get().BranchId;
            validationMessage = String.Empty;
            
            foreach(var i in vm.MappedOrganizationList)
            {
                Organization org = GetOrganization(i.OrganizationId);

                if (org == null || !org.IsActive)
                {
                    validationMessage = String.Format(OrganizationStrings.Organization_CO_Validation_InvalidOrg, org.OrganizationName);
                    return false;
                }

                if (i.Checked)
                {
                    if (org.CreditOfficerId != null && org.CreditOfficerId != vm.CreditOfficerId)
                    {
                        validationMessage = String.Format(OrganizationStrings.Organization_CO_Validation_OrgHasCO, org.OrganizationName);
                        return false;
                    }

                    org.CreditOfficerId = vm.CreditOfficerId;
                }
                else
                {
                    org.CreditOfficerId = null;
                }

                OrgCreditOfficer orgCO = (from c in _uow.OrgCORepository.Get(x => x.BranchId == branchId
                                              && x.EmployeeId == vm.CreditOfficerId
                                              && x.OrganizationId == i.OrganizationId
                                              && x.ReleaseDate == null)
                                          select c).SingleOrDefault();

                if (orgCO != null)
                {
                    if (!i.Checked)
                    {
                        orgCO.ReleaseDate = _userHelper.Get().DayOpenClose.SystemDate;
                        orgCO.UserId = _userHelper.Get().UserId;
                        orgCO.SystemDate = _userHelper.Get().DayOpenClose.SystemDate;
                        orgCO.SetDate = DateTime.Now;

                        _uow.OrgCORepository.Update(orgCO);
                    }
                }
                else
                {
                    if (i.Checked)
                    {
                        _uow.OrgCORepository.Insert(new OrgCreditOfficer
                        {
                            BranchId = branchId,
                            EmployeeId = vm.CreditOfficerId,
                            OrganizationId = i.OrganizationId,
                            AssignedDate = _userHelper.Get().DayOpenClose.SystemDate,
                            UserId = _userHelper.Get().UserId,
                            SystemDate = _userHelper.Get().DayOpenClose.SystemDate,
                            SetDate = DateTime.Now
                        });
                    }
                }
                _uow.OrganizationRepository.Update(org);
            }

            _uow.Save();

            return true;
        }
    }
}
