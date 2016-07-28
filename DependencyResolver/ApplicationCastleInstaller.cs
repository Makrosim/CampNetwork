using CampBusinessLogic.Interfaces;
using CampBusinessLogic.Services;
using CampDataAccess.Repositories;
using CampDataAccess.Interfaces;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace DependencyResolver
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        private string connectionString;

        public ApplicationCastleInstaller(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // регистрируем компоненты приложения
            container.Register(Component.For<IUserProfileService>().ImplementedBy<UserProfileService>());
            container.Register(Component.For<IPostService>().ImplementedBy<PostService>());
            container.Register(Component.For<IMessageService>().ImplementedBy<MessageService>());

            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().DynamicParameters((r, k) => { k["connectionString"] = connectionString; }))
            // регистрируем каждый контроллер по отдельности
            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();

            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}