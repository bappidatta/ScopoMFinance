using NtitasCommon.Core.Common;
using ScopoMFinance.Core.Helpers;
using ScopoMFinance.Core.Services;
using ScopoMFinance.Domain.Models;
using ScopoMFinance.Domain.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            #region === DB ===

            container.Register<ScopoMFinanceEntities>(Lifestyle.Scoped);
            container.Register<UnitOfWork>();

            #endregion === DB ===
            #region === Services ===

            container.Register<IBranchService, BranchService>();
            container.Register<IUserProfileService, UserProfileService>();
            container.Register<IUserLoginAuditService, UserLoginAuditService>();
            container.Register<IOrgCategoryService, OrgCategoryService>();
            container.Register<IGenderService, GenderService>();
            container.Register<IColcOptionService, ColcOptionService>();
            container.Register<IDayOpenCloseService, DayOpenCloseService>();
            container.Register<IOrganizationService, OrganizationService>();
            container.Register<IEmployeeTypeService, EmployeeTypeService>();
            container.Register<IComponentTypeService, ComponentTypeService>();
            container.Register<IComponentService, ComponentService>();
            container.Register<ILoanProductService, LoanProductService>();
            container.Register<ISavingsProductService, SavingsProductService>();
            container.Register<IEmployeeService, EmployeeService>();
            container.Register<IDonorService, DonorService>();

            container.Register<IUserHelper, UserHelper>();
            container.Register<IConfig, Config>();
            container.Register<ICookieAccessor, CookieAccessor>();

            UserHelper.Instance = container.GetInstance<IUserHelper>();

            #endregion === Services ===
        }
    }
}