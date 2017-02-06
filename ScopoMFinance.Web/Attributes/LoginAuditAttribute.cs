using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.Attributes
{
    public class LoginAuditAttribute : AuthorizeAttribute
    {
        private IUserLoginAuditService _loginAuditService
        {
            get
            {
                return DependencyResolver.Current.GetService<IUserLoginAuditService>();
            }
        }

        private IUserHelper _userHelper
        {
            get
            {
                return DependencyResolver.Current.GetService<IUserHelper>();
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            if (_userHelper.Get() == null)
                return false;

            if (_loginAuditService.GetCurrentLoggedIn(httpContext.User.Identity.Name) == null)
                return false;

            return true;
        }
    }
}