using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Entities;

namespace Mittagessen.Data.Repositories
{
    public class SimpleReadRepository<T> : RepositoryBase, ISimpleReadRepository<T>
        where T : EntityBase
    {
        public SimpleReadRepository(IDbContextManager dbContextManager)
            : base(dbContextManager)
        { }

        public T Get(Guid id)
        {
            return Session.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Session.Set<T>().AsNoTracking().ToList();
        }
    }
}
