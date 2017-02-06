using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.User
{
    public class UserLoginAuditViewModel
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string UserId { get; set; }
        public DateTime LoggedInTime { get; set; }
        public DateTime LoggedOutTime { get; set; }
    }
}
