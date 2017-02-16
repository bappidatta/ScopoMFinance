using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Acc
{
    public class DayOpenCloseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "System Open Date")]
        public DateTime SystemDate { get; set; }
        public bool IsCloseRequest { get; set; }
        public bool IsClosed { get; set; }
        public string CloseRequestBy { get; set; }
        public Nullable<DateTime> CloseRequestAt { get; set; }
        public DateTime OpenedAt { get; set; }
        public Nullable<DateTime> ClosedAt { get; set; }
    }
}
