using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using FluentValidation;
using StructureMap;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Repositories;
using Mittagessen.Web.Infrastructure;
using Mittagessen.Web.Validation;

namespace Mittagessen.Web.AppStart
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

            exp.Scan(cfg =>
                {
                    cfg.AssemblyContainingType<RepositoryBase>();
                    cfg.IncludeNamespaceContainingType<RepositoryBase>();
                    cfg.SingleImplementationsOfInterface();
                });

            AssemblyScanner.FindValidatorsInAssemblyContaining<RegistrationModelValidation>()
              .ForEach(result => exp.For(result.InterfaceType)
                                     .Singleton()
                                     .Use(result.ValidatorType));
        }       
    }
}