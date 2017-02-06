using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Policy
{
    public class BranchListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public bool IsHeadOffice { get; set; }
        public bool Status { get; set; }
        public int OrgCount { get; set; }
        public int COCount { get; set; }
        public int UserCount { get; set; }
        public int ProjectCount { get; set; }
    }
}
