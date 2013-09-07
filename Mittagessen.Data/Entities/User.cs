using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mittagessen.Data.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastAccessDate { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
