using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Interfaces;

namespace Mittagessen.Data.Repositories
{
    public abstract class RepositoryBase
    {
        private IDbContextManager _contextManager;

        public DataContext Session
        {
            get { return _contextManager.GetCurrentContext(); }
        }

        public RepositoryBase(IDbContextManager contextManager)
        {
            _contextManager = contextManager;
        }
    }
}
