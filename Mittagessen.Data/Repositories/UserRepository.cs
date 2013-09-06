using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Entities;

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
    }
}
