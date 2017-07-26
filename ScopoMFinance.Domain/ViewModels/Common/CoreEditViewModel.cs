using NtitasCommon.Localization;
using ScopoMFinance.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ScopoMFinance.Domain.ViewModels.Common
{
    public class CoreEditViewModel
    {
        [UIHint("Hidden")]
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "List_DN_IsActive", ResourceType = typeof(CommonStrings))]
        public bool IsActive { get; set; }

        [UIHint("Hidden")]
        [HiddenInput]
        public string UserId { get; set; }

        [UIHint("Hidden")]
        [HiddenInput]
        public System.DateTime SystemDate { get; set; }

        [UIHint("Hidden")]
        [HiddenInput]
        public System.DateTime SetDate { get; set; }
    }
}
