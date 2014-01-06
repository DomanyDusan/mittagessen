using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mittagessen.Data.Entities
{
    public class MealVariation : EntityBase
    {
        public virtual Meal Meal { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("Meal")]
        public Guid MealId { get; set; }

        public string Name { get; set; }
        public MealVariationCategory Category { get; set; }
        public bool RequiresDeadLine { get; set; }
    }
}
