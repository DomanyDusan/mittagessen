using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mittagessen.Data.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
