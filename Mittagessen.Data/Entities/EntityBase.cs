using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Mittagessen.Data.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
