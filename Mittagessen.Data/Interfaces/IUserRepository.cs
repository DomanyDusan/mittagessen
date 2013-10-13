using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Entities;

namespace Mittagessen.Data.Interfaces
{
    public interface IUserRepository : ISimpleRepository<User>
    {
        User GetUserByName(string name);
        User GetUserByNameOrEmail(string nameOrEmail);
        bool UserNameAvailable(string name);
        bool UserNameAvailable(string name, Guid userId);
        bool EmailAddressAvailable(string email);
        bool EmailAddressAvailable(string email, Guid userId);
        IEnumerable<string> GetEmailAddresses();
    }
}
