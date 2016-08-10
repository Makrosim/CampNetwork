using CampDataAccess.Interfaces;
using CampDataAccess.Repositories;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBusinessLogic.Castle
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        private string connectionString;

        public BusinessLogicInstaller(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().DynamicParameters((r, k) => { k["connectionString"] = connectionString; }).LifeStyle.Singleton);
        }
    }
}
