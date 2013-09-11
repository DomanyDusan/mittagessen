using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using StructureMap;

namespace Mittagessen.Web.Infrastructure
{
    public class StructureMapValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return ObjectFactory.TryGetInstance(validatorType) as IValidator;
        }
    }
}