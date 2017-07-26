using NtitasCommon.Localization;
using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopoMFinance.Domain.ViewModels.Common
{
    public class CoreListViewModel
    {
        public int Id { get; set; }
        [Display(Name = "List_DN_IsActive", ResourceType = typeof(CommonStrings))]
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public System.DateTime SystemDate { get; set; }
        public System.DateTime SetDate { get; set; }
    }
}
