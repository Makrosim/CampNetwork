﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Owin.Security.OAuth;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.Services;
using System.Web.Http;
using CampAPI.App_Start;
using System.Web.Http.Controllers;
using CampAPI.Controllers;

namespace CampAPI.Windsor
{
    public class ApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>()); // Зарегистрировать следующие компоненты
            container.Register(Component.For<IOAuthAuthorizationServerProvider>().ImplementedBy<OAuthService>());
            container.Register(Component.For<Startup>());

            var start = container.Resolve<Startup>();
            start.OAuthservice = container.Resolve<IOAuthAuthorizationServerProvider>();

            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());
        }
    }
}
