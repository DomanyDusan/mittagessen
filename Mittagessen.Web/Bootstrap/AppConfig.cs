using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mittagessen.Web.Infrastructure;
using System.Web.Mvc;

namespace Mittagessen.Web.Bootstrap
{
    public class AppConfig
    {
        public static void Initialize()
        {
            DependencyResolver.SetResolver(new StructureMapDependencyResolver());
        }
    }
}