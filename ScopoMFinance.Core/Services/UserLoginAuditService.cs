using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels;
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
        void update(string username, int branchId);
        void clearAllLogin(string username, int branchId);
        UserLoginAuditViewModel GetCurrentLoggedIn(string username);
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

        public void clearAllLogin(string username, int branchId)
        {
            List<UserLoginAudit> userLogins = (from c in _uow.UserLoginAuditRepository.Get()
                                               where c.BranchId == branchId
                                               && c.UserProfile.AspNetUser.UserName == username
                                               && c.LoggedOutTime == null
                                               select c).ToList();

            foreach (var userLogin in userLogins)
            {
                userLogin.LoggedOutTime = DateTime.Now;

                _uow.UserLoginAuditRepository.Update(userLogin);
            }

            _uow.Save();
        }

        public void insert(string username, int branchId)
        {
            clearAllLogin(username, branchId);
            UserLoginAudit model = new UserLoginAudit()
            {
                BranchId = branchId,
                UserId = GetUserId(username),
                LoggedInTime = DateTime.Now
            };

            _uow.UserLoginAuditRepository.Insert(model);
            _uow.Save();
        }

        public void update(string username, int branchId)
        {
            UserLoginAudit userLogin = (from c in _uow.UserLoginAuditRepository.Get()
                                        where c.BranchId == branchId
                                        && c.UserProfile.AspNetUser.UserName == username
                                        && c.LoggedOutTime == null
                                        select c).LastOrDefault();

            userLogin.LoggedOutTime = DateTime.Now;
            _uow.UserLoginAuditRepository.Update(userLogin);
            _uow.Save();
        }

        public UserLoginAuditViewModel GetCurrentLoggedIn(string username)
        {
            UserLoginAuditViewModel userLogin = (from c in _uow.UserLoginAuditRepository.Get()
                                                 where c.UserProfile.AspNetUser.UserName == username
                                                 && c.LoggedOutTime == null
                                                 select new UserLoginAuditViewModel()
                                                 {
                                                     Id = c.Id,
                                                     BranchId = c.BranchId,
                                                     UserId = c.UserId,
                                                     LoggedInTime = c.LoggedInTime
                                                 }).LastOrDefault();

            return userLogin;
        }
    }
}
