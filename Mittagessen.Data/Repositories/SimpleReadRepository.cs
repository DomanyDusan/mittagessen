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
        public virtual T Get(Guid id)
        {
            var result = Session.Set<T>().Find(id);
            return result;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Session.Set<T>().AsNoTracking().ToList();
        }
    }
}
