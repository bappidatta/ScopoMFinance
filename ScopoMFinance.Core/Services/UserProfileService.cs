using ScopoMFinance.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Core.Services
{
    public class UserProfileService
    {
        private UnitOfWork _uow;

        public UserProfileService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public bool IsValidUser(int branchId, string username)
        {
            var validUser = from c in _uow.UserProfileRepository.Get()
                            where c.IsActive == true && c.UserBranches.All(x=>x.BranchId == branchId) && c.AspNetUser.UserName == username
                            select c;

            return validUser.SingleOrDefault() != null;
        }
    }
}
