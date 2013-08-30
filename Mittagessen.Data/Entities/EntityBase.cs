using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Mittagessen.Data.Entities
{
    public abstract class EntityBase
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }
    }
}
