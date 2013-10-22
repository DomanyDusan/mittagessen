using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Repositories;
using Mittagessen.Web.Infrastructure;
using StructureMap;

namespace Mittagessen.Tests.Infrastructure
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

            exp.For<IAuthentication>()
                .Use<Authentication>();

            exp.FillAllPropertiesOfType<IAuthentication>()
                .Use<Authentication>();

            exp.Scan(cfg =>
            {
                cfg.AssemblyContainingType<RepositoryBase>();
                cfg.IncludeNamespaceContainingType<RepositoryBase>();
                cfg.SingleImplementationsOfInterface();
            });
        }
    }
}
