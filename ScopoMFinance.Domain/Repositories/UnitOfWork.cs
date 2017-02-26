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

        private IRepository<SysDivision> divisionRepo;
        public IRepository<SysDivision> DivisionRepository
        {
            get
            {
                if (this.divisionRepo == null)
                {
                    this.divisionRepo = new Repository<SysDivision>(db);
                }
                return divisionRepo;
            }
        }

        private IRepository<SysDistrict> districtRepo;
        public IRepository<SysDistrict> DistrictRepository
        {
            get
            {
                if (this.districtRepo == null)
                {
                    this.districtRepo = new Repository<SysDistrict>(db);
                }
                return districtRepo;
            }
        }

        private IRepository<SysThana> thanaRepo;
        public IRepository<SysThana> ThanaRepository
        {
            get
            {
                if (this.thanaRepo == null)
                {
                    this.thanaRepo = new Repository<SysThana>(db);
                }
                return thanaRepo;
            }
        }

        private IRepository<SysUpazila> upazilaRepo;
        public IRepository<SysUpazila> UpazilaRepository
        {
            get
            {
                if (this.upazilaRepo == null)
                {
                    this.upazilaRepo = new Repository<SysUpazila>(db);
                }
                return upazilaRepo;
            }
        }

        private IRepository<SysUnion> unionRepo;
        public IRepository<SysUnion> UnionRepository
        {
            get
            {
                if (this.unionRepo == null)
                {
                    this.unionRepo = new Repository<SysUnion>(db);
                }
                return unionRepo;
            }
        }

        private IRepository<SysDonor> donorRepo;
        public IRepository<SysDonor> DonorRepository
        {
            get
            {
                if (this.donorRepo == null)
                {
                    this.donorRepo = new Repository<SysDonor>(db);
                }
                return donorRepo;
            }
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

        private IRepository<OrgCreditOfficer> orgCORepo;
        public IRepository<OrgCreditOfficer> OrgCORepository
        {
            get
            {
                if (this.orgCORepo == null)
                {
                    this.orgCORepo = new Repository<OrgCreditOfficer>(db);
                }
                return orgCORepo;
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

        private IRepository<EmployeeType> employeeTypeRepo;
        public IRepository<EmployeeType> EmployeeTypeRepository
        {
            get
            {
                if (this.employeeTypeRepo == null)
                {
                    this.employeeTypeRepo = new Repository<EmployeeType>(db);
                }
                return employeeTypeRepo;
            }
        }

        private IRepository<Employee> employeeRepo;
        public IRepository<Employee> EmployeeRepository
        {
            get
            {
                if (this.employeeRepo == null)
                {
                    this.employeeRepo = new Repository<Employee>(db);
                }
                return employeeRepo;
            }
        }

        private IRepository<ComponentType> componentTypeRepo;
        public IRepository<ComponentType> ComponentTypeRepository
        {
            get
            {
                if (this.componentTypeRepo == null)
                {
                    this.componentTypeRepo = new Repository<ComponentType>(db);
                }
                return componentTypeRepo;
            }
        }

        private IRepository<Component> componentRepo;
        public IRepository<Component> ComponentRepository
        {
            get
            {
                if (this.componentRepo == null)
                {
                    this.componentRepo = new Repository<Component>(db);
                }
                return componentRepo;
            }
        }

        private IRepository<SavingsProduct> savingsProductRepo;
        public IRepository<SavingsProduct> SavingsProductRepository
        {
            get
            {
                if (this.savingsProductRepo == null)
                {
                    this.savingsProductRepo = new Repository<SavingsProduct>(db);
                }
                return savingsProductRepo;
            }
        }

        private IRepository<LoanProduct> loanProductRepo;
        public IRepository<LoanProduct> LoanProductRepository
        {
            get
            {
                if (this.loanProductRepo == null)
                {
                    this.loanProductRepo = new Repository<LoanProduct>(db);
                }
                return loanProductRepo;
            }
        }

    }
}
