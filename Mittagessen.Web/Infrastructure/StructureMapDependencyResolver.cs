using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;

namespace Mittagessen.Web.Infrastructure
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            try
            {
                var service = serviceType.IsAbstract || serviceType.IsInterface
                         ? ObjectFactory.TryGetInstance(serviceType)
                         : ObjectFactory.GetInstance(serviceType);

                return service;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ObjectFactory.GetAllInstances(serviceType).Cast<object>();
        }
    }
}