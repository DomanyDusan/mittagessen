using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Entities;

namespace Mittagessen.Data.Repositories
{
    public class SimpleRepository<T> : SimpleReadRepository<T>, ISimpleRepository<T>
        where T : EntityBase
    {
        public SimpleRepository(IDbContextManager dbContextManager)
            : base(dbContextManager)
        { }

        public void Insert(T entity)
        {
            entity.Id = Guid.NewGuid();
            Session.Set<T>().Add(entity);
            Session.SaveChanges();
        }
    }
}
