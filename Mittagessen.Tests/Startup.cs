using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Mittagessen.Data;
using Mittagessen.Tests.Infrastructure;
using NUnit.Framework;

namespace Mittagessen.Tests
{
    [SetUpFixture]
    public class Startup
    {
        [SetUp]
        public void Init()
        {
            using (var dbContext = new DataContext("Dummy"))
            {
                dbContext.Database.ExecuteSqlCommand(ConfigurationManager.AppSettings["EnsureDbAccess"]);
                dbContext.Database.ExecuteSqlCommand(ConfigurationManager.AppSettings["DbRestoreCommand"]);

                IocConfig.RegisterComponents();
            }         
        }
    }
}
