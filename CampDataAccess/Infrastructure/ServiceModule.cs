﻿using Ninject.Modules;
using CampDataAccess.Interfaces;
using CampDataAccess.Repositories;

namespace CampBusinessLogic.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}