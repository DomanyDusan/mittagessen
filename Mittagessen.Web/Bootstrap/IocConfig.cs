using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Repositories;
using Mittagessen.Web.Infrastructure;

namespace Mittagessen.Web.Bootstrap
{
    public static class IocConfig
    {
        public static void RegisterComponents()
        {
            ObjectFactory.Initialize(InitializeContainer);
        }

        private static void InitializeContainer(IInitializationExpression exp)
        {
            exp.For<IDbContextManager>()
                .Use<DbContextManager>();

            exp.FillAllPropertiesOfType<IDbContextManager>()
                .Use<DbContextManager>();

            exp.For(typeof(ISimpleRepository<>))
                .Use(typeof(SimpleRepository<>));
        }       
    }
}