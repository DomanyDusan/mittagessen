using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Entities;
using System.Data;

namespace Mittagessen.Data.Repositories
{
    public class SimpleRepository<T> : SimpleReadRepository<T>, ISimpleRepository<T>
        where T : EntityBase
    {
        public SimpleRepository(IDbContextManager dbContextManager)
            : base(dbContextManager)
        { }

        public virtual void Insert(T entity)
        {
            entity.Id = Guid.NewGuid();
            Session.Set<T>().Add(entity);
            Session.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            Session.Set<T>().Attach(entity);
            Session.Entry(entity).State = EntityState.Modified;
            Session.SaveChanges();
        }
    }
}
