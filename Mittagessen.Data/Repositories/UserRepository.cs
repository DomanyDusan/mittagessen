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
        public User GetUserByName(string name)
        {
            return Session.Users.SingleOrDefault(u => u.Name == name);
        }

        public User GetUserByNameOrEmail(string nameOrEmail)
        {
            return Session.Users.SingleOrDefault(u => u.Name == nameOrEmail || u.Email == nameOrEmail);
        }

        public bool UserNameAvailable(string name)
        {
            return !Session.Users.Any(u => u.Name == name);
        }

        public bool UserNameAvailable(string name, Guid userId)
        {
            return !Session.Users.Any(u => u.Name == name && u.Id != userId);
        }

        public bool EmailAddressAvailable(string email)
        {
            return !Session.Users.Any(u => u.Email == email);
        }

        public bool EmailAddressAvailable(string email, Guid userId)
        {
            return !Session.Users.Any(u => u.Email == email && u.Id != userId);
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

        public IEnumerable<string> GetEmailAddresses()
        {
            return Session.Users.AsNoTracking().Select(u => u.Email).ToList();
        }
    }
}
