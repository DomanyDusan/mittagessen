using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Interfaces;

namespace Mittagessen.Data.Repositories
{
    public abstract class RepositoryBase
    {
        public IDbContextManager ContextManager { get; set; }

        public DataContext Session
        {
            get { return ContextManager.GetCurrentContext(); }
        }
    }
}
