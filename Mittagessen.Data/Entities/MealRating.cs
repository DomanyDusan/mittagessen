using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mittagessen.Data.Entities
{
    public class MealRating : EntityBase
    {
        public virtual User RatedBy { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("RatedBy")]
        public Guid RatedById { get; set; }
        public virtual Meal RatedMeal { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("RatedMeal")]
        public Guid RatedMealId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}
