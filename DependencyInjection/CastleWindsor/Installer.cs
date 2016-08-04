using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Owin.Security.OAuth;
using CampBusinessLogic.Services;
using System.Web.Http;
using CampAPI.App_Start;
using CampDataAccess.Interfaces;
using CampDataAccess.Repositories;
using CampAPI.Tests.Controllers;

namespace DependencyInjection.CastleWindsor
{
    public class Installer : IWindsorInstaller
    {
        private string ConnectionString { get; set; }

        public Installer(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().DynamicParameters((r, k) => { k["connectionString"] = ConnectionString; }));
            container.Register(Component.For<IOAuthAuthorizationServerProvider>().ImplementedBy<OAuthService>());
            container.Register(Component.For<Startup>());
            container.Register(Component.For<AccountControllerTest>());

            var start = container.Resolve<Startup>();

            start.OAuthservice = container.Resolve<IOAuthAuthorizationServerProvider>();

            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleScoped());
        }
    }
}
