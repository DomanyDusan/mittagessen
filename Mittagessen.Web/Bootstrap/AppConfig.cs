using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using FluentValidation;
using FluentValidation.Mvc;
using Mittagessen.Web.Infrastructure;
using System.Web.Mvc;
using StructureMap;

namespace Mittagessen.Web.Bootstrap
{
    public class AppConfig
    {
        public static void Initialize()
        {
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            ModelBinders.Binders[typeof(DateTime)] = new DateAndTimeModelBinder() { Date = "Date", Time = "Time" };

            var fvValidationModelProvider = new FluentValidationModelValidatorProvider(new StructureMapValidatorFactory());
            fvValidationModelProvider.AddImplicitRequiredValidator = false;
            ModelValidatorProviders.Providers.Add(fvValidationModelProvider);
        }
    }
}