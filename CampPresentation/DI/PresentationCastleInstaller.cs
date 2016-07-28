using CampBusinessLogic.Interfaces;
using CampBusinessLogic.Services;
using CampBusinessLogic.Castle;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace CampPresentation.DI
{
    public class PresentationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // регистрируем компоненты приложения
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>());
            container.Register(Component.For<IProfileService>().ImplementedBy<ProfileService>());
            container.Register(Component.For<IPostService>().ImplementedBy<PostService>());
            container.Register(Component.For<IMessageService>().ImplementedBy<MessageService>());
            container.Register(Component.For<ICampPlaceService>().ImplementedBy<CampPlaceService>());
            container.Register(Component.For<IGroupService>().ImplementedBy<GroupService>());

            // регистрируем каждый контроллер по отдельности
            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();

            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}