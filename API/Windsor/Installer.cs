using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Owin.Security.OAuth;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.Services;
using System.Web.Http;
using API.App_Start;
using System.Web.Http.Controllers;
using API.Controllers;

namespace API.Windsor
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifestylePerWebRequest());
            container.Register(Component.For<IProfileService>().ImplementedBy<ProfileService>().LifestylePerWebRequest());
            container.Register(Component.For<ICampPlaceService>().ImplementedBy<CampPlaceService>().LifestylePerWebRequest());
            container.Register(Component.For<IPostService>().ImplementedBy<PostService>().LifestylePerWebRequest());
            container.Register(Component.For<IMessageService>().ImplementedBy<MessageService>().LifestylePerWebRequest());
            container.Register(Component.For<IGroupService>().ImplementedBy<GroupService>().LifestylePerWebRequest());
            container.Register(Component.For<IMediaService>().ImplementedBy<MediaService>().LifestylePerWebRequest());
            container.Register(Component.For<IOAuthAuthorizationServerProvider>().ImplementedBy<OAuthService>());
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());
        }
    }
}
