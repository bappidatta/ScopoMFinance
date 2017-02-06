using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IUserLoginAuditService
    {
        void insert(string username, int branchId);
        void clearAllLogin(string username, int branchId = 0);
        UserLoginAuditViewModel GetCurrentLoggedIn(string username);
        UserCacheViewModel GetUserCache(string username);
    }

    public class UserLoginAuditService : IUserLoginAuditService
    {
        private UnitOfWork _uow;

        public UserLoginAuditService(UnitOfWork uow)
        {
            _uow = uow;
        }

        private string GetUserId(string username)
        {
            string userId = (from c in _uow.UserProfileRepository.Get()
                             where c.AspNetUser.UserName == username
                             select c.UserId).SingleOrDefault();

            return userId;
        }

        public void clearAllLogin(string username, int branchId = 0)
        {
            List<UserLoginAudit> userLogins = null;
            if (branchId > 0)
            {
                userLogins = (from c in _uow.UserLoginAuditRepository.Get()
                              where c.BranchId == branchId
                              && c.UserProfile.AspNetUser.UserName == username
                              && c.LoggedOutTime == null
                              select c).ToList();
            }
            else 
            {
                userLogins = (from c in _uow.UserLoginAuditRepository.Get()
                              where c.UserProfile.AspNetUser.UserName == username
                              && c.LoggedOutTime == null
                              select c).ToList();
            }

            foreach (var userLogin in userLogins)
            {
                userLogin.LoggedOutTime = DateTime.Now;

                _uow.UserLoginAuditRepository.Update(userLogin);
            }

            _uow.Save();
        }

        public void insert(string username, int branchId)
        {
            clearAllLogin(username);
            UserLoginAudit model = new UserLoginAudit()
            {
                BranchId = branchId,
                UserId = GetUserId(username),
                LoggedInTime = DateTime.Now
            };

            _uow.UserLoginAuditRepository.Insert(model);
            _uow.Save();
        }

        public UserLoginAuditViewModel GetCurrentLoggedIn(string username)
        {
            UserLoginAuditViewModel userLogin = (from c in _uow.UserLoginAuditRepository.Get()
                                                 where c.UserProfile.AspNetUser.UserName == username
                                                 && c.LoggedOutTime == null
                                                 orderby c.Id
                                                 select new UserLoginAuditViewModel()
                                                 {
                                                     Id = c.Id,
                                                     BranchId = c.BranchId,
                                                     UserId = c.UserId,
                                                     LoggedInTime = c.LoggedInTime
                                                 }).FirstOrDefault();

            return userLogin;
        }

        public UserCacheViewModel GetUserCache(string username)
        {
            UserCacheViewModel userCache = (from c in _uow.UserLoginAuditRepository.Get()
                                            where c.UserProfile.AspNetUser.UserName == username
                                            && c.LoggedOutTime == null
                                            orderby c.Id
                                            select new UserCacheViewModel()
                                            {
                                                BranchId = c.BranchId,
                                                BranchName = c.Branch.Name,
                                                FirstName = c.UserProfile.FirstName,
                                                LastName = c.UserProfile.LastName,
                                                LoggedInTime = c.LoggedInTime
                                            }).FirstOrDefault();

            return userCache;
        }
    }
}
