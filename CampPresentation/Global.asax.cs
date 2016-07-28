using CampPresentation.DI;
using Castle.Windsor;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CampBusinessLogic.Castle;

namespace CampPresentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new WindsorContainer();

            container.Install(new PresentationCastleInstaller());
            container.Install(new BusinessLogicCastleInstaller("DefaultConnection"));

            var castleControllerFactory = new CastleControllerFactory(container);

            ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
