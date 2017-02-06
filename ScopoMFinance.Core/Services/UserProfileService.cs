using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public interface IUserProfileService
    {
        bool IsValidUser(int branchId, string username);
        UserCacheViewModel GetUserCache(string username);
    }

    public class UserProfileService : IUserProfileService
    {
        private UnitOfWork _uow;

        public UserProfileService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public bool IsValidUser(int branchId, string username)
        {
            var validUser = from c in _uow.UserProfileRepository.Get()
                            where c.IsActive == true && c.UserBranches.Any(x=>x.BranchId == branchId) && c.AspNetUser.UserName == username
                            select c;

            return validUser.SingleOrDefault() != null;
        }

        public UserCacheViewModel GetUserCache(string username)
        {
            var userCache = from c in _uow.UserProfileRepository.Get()
                            where c.IsActive == true && c.IsDeleted == false && c.AspNetUser.UserName == username
                            select new UserCacheViewModel()
                            {
                                BranchId = c.UserBranches.FirstOrDefault().BranchId,
                                BranchName = c.UserBranches.FirstOrDefault().Branch.Name
                            };

            return userCache.SingleOrDefault();
        }
    }
}
