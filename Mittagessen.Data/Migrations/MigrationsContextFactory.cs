using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;

namespace Mittagessen.Data.Migrations
{
    public class MigrationsContextFactory : IDbContextFactory<DataContext>
    {
        private const string CONNECTION_STRING_NAME = "ReadModels";

        public DataContext Create()
        {
            return new DataContext(CONNECTION_STRING_NAME);
        }
    }
}
