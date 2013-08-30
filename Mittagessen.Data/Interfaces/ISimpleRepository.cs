using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mittagessen.Data.Interfaces
{
    public interface ISimpleRepository<T> : ISimpleReadRepository<T>
    {
        void Insert(T entity);
    }
}
