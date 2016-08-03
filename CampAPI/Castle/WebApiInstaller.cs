using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Owin.Security.OAuth;
using CampBusinessLogic.Services;
using System.Web.Http;

namespace CampAPI.Castle
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IOAuthAuthorizationServerProvider>().ImplementedBy<OAuthService>());
            container.Register( Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleScoped());
        }

    }
}