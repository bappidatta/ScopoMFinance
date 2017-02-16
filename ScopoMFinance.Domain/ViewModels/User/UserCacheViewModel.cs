using ScopoMFinance.Domain.ViewModels.Acc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.User
{
    public class UserCacheViewModel
    {
        public string UserId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LoggedInTime { get; set; }
        public DayOpenCloseViewModel DayOpenClose { get; set; }
    }
}
