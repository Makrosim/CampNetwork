using CampDataAccess.Repositories;
using CampDataAccess.Interfaces;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace CampBusinessLogic.Castle
{
    public class BusinessLogicCastleInstaller : IWindsorInstaller
    {
        private string connectionString;

        public BusinessLogicCastleInstaller(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().DynamicParameters((r, k) => { k["connectionString"] = connectionString; }));
        }
    }
}