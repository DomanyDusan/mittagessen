using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mittagessen.Data.Interfaces
{
    public interface ISimpleReadRepository<T>
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
    }
}
