using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Entities;
using System.Transactions;

namespace Mittagessen.Data.Repositories
{
    public class UserRepository : SimpleRepository<User>, IUserRepository
    {
        public UserRepository(IDbContextManager contextManager)
            : base(contextManager)
        { }

        public User GetUserByName(string name)
        {
            return Session.Users.SingleOrDefault(u => u.Name == name);
        }

        public override void Insert(User entity)
        {
            using (var tr = new TransactionScope())
            {
                if (GetUserByName(entity.Name) == null)
                {
                    base.Insert(entity);
                    tr.Complete();
                }
            }
        }
    }
}
