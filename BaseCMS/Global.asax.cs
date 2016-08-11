using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using BaseCMS.Models.Repositories;
using BaseCMS.Providers;
using Ninject;
using Ninject.Web.Common;

namespace BaseCMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class BaseCMSMvc4 : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new RepositoryModule());

            kernel.Bind<IUserInfoRepo>().To<UserInfoRepo>();

            kernel.Bind<MembershipProvider>().To<BaseCMSMembershipProvider>();
            kernel.Bind<IMembershipService>().To<AccountMembershipService>();

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            base.OnApplicationStarted();
        }
    }
}