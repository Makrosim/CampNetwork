using CampAPI.Windsor;
using CampBusinessLogic.Castle;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CampAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container = new WindsorContainer();

        protected void Application_Start()
        {
            ConfigureWindsor(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void ConfigureWindsor(HttpConfiguration configuration)
        {
            container = new WindsorContainer();
            container.Install(new BusinessLogicInstaller("DefaultConnection"));
            container.Install(FromAssembly.This());
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            var dependencyResolver = new WindsorDependencyResolver(container);
            configuration.DependencyResolver = dependencyResolver;
        }

        public override void Dispose()
        {
            container.Dispose();
            base.Dispose();
        }

    }
}
