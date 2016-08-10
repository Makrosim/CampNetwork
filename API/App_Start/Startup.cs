using API.Windsor;
using CampBusinessLogic.Castle;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

[assembly: OwinStartup(typeof(API.App_Start.Startup))]
namespace API.App_Start
{
    public class Startup
    {
        private static IWindsorContainer container = new WindsorContainer();

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureWindsor(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, container));

            ConfigureOAuth(app);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = container.Resolve<IOAuthAuthorizationServerProvider>()
            };

            app.UseOAuthBearerTokens(OAuthServerOptions);

        }

        public static void ConfigureWindsor(HttpConfiguration configuration)
        {

            container.Install(new BusinessLogicInstaller("DefaultConnection"));
            container.Install(new Installer());
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));

            var dependencyResolver = new WindsorDependencyResolver(container);
            configuration.DependencyResolver = dependencyResolver;
        }

    }
}