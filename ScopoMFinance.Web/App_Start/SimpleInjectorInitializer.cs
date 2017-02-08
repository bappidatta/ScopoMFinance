using Microsoft.AspNet.Identity;
using NtitasCommon.Core.Common;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using ScopoMFinance.Web.Models;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ScopoMFinance.Web.App_Start
{
    public static class SimpleInjectorInitializer
    {
        public static void initialize()
        {
            // 1. Create a new Simple Injector container
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            // 3. Optionally verify the container's configuration.
            container.Verify();

            // 4. Register the container as MVC3 IDependencyResolver.
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<ScopoMFinanceEntities>(Lifestyle.Scoped);
            container.Register<UnitOfWork>();

            container.Register<IBranchService, BranchService>();
            container.Register<IUserProfileService, UserProfileService>();
            container.Register<ICookieAccessor, CookieAccessor>();
            container.Register<IUserLoginAuditService, UserLoginAuditService>();
            container.Register<IUserHelper, UserHelper>();
            container.Register<IConfig, Config>();

            UserHelper.Instance = container.GetInstance<IUserHelper>();
        }
    }
}