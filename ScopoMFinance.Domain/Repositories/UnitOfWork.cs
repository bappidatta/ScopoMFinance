using ScopoMFinance.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.Repositories
{
    public class UnitOfWork
    {
        private ScopoMFinanceEntities db;

        public UnitOfWork(ScopoMFinanceEntities db)
        {
            this.db = db;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private IRepository<Branch> branchRepo;
        public IRepository<Branch> BranchRepository
        {
            get
            {
                if (this.branchRepo == null)
                {
                    this.branchRepo = new Repository<Branch>(db);
                }
                return branchRepo;
            }
        }

        private IRepository<UserProfile> userProfileRepo;
        public IRepository<UserProfile> UserProfileRepository
        {
            get
            {
                if (this.userProfileRepo == null)
                {
                    this.userProfileRepo = new Repository<UserProfile>(db);
                }
                return userProfileRepo;
            }
        }

        private IRepository<UserLoginAudit> userLoginAuditRepo;
        public IRepository<UserLoginAudit> UserLoginAuditRepository
        {
            get
            {
                if (this.userLoginAuditRepo == null)
                {
                    this.userLoginAuditRepo = new Repository<UserLoginAudit>(db);
                }
                return userLoginAuditRepo;
            }
        }

        private IRepository<OrgCategory> orgCategoryRepo;
        public IRepository<OrgCategory> OrgCategoryRepository
        {
            get
            {
                if (this.orgCategoryRepo == null)
                {
                    this.orgCategoryRepo = new Repository<OrgCategory>(db);
                }
                return orgCategoryRepo;
            }
        }

        private IRepository<SysGender> genderRepo;
        public IRepository<SysGender> GenderRepository
        {
            get
            {
                if (this.genderRepo == null)
                {
                    this.genderRepo = new Repository<SysGender>(db);
                }
                return genderRepo;
            }
        }

        private IRepository<SysColcOption> colcOptionRepo;
        public IRepository<SysColcOption> ColcOptionRepository
        {
            get
            {
                if (this.colcOptionRepo == null)
                {
                    this.colcOptionRepo = new Repository<SysColcOption>(db);
                }
                return colcOptionRepo;
            }
        }

        private IRepository<AccDayOpenClose> dayOpenCloseRepo;
        public IRepository<AccDayOpenClose> DayOpenCloseRepository
        {
            get
            {
                if (this.dayOpenCloseRepo == null)
                {
                    this.dayOpenCloseRepo = new Repository<AccDayOpenClose>(db);
                }
                return dayOpenCloseRepo;
            }
        }

        private IRepository<Organization> organizationRepo;
        public IRepository<Organization> OrganizationRepository
        {
            get
            {
                if (this.organizationRepo == null)
                {
                    this.organizationRepo = new Repository<Organization>(db);
                }
                return organizationRepo;
            }
        }
    }
}
