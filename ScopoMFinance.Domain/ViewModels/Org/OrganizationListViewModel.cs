using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Org
{
    public class OrganizationListViewModel
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string OrganizationNo { get; set; }
        public string OrganizationName { get; set; }
        public int OrgCategoryId { get; set; }
        public string OrgCategoryName { get; set; }
        public int GenderId { get; set; }
        public string Gender { get; set; }
        public System.DateTime SetupDate { get; set; }
        public int LoanColcOptionId { get; set; }
        public string LoanColcOption { get; set; }
        public int SavColcOptionId { get; set; }
        public string SavColcOption { get; set; }
        public System.DateTime FirstLoanColcDate { get; set; }
        public System.DateTime FirstSavColcDate { get; set; }
        public Nullable<int> VillageId { get; set; }
        public string Village { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime SystemDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
