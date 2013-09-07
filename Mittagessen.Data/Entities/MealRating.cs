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
        public virtual User RankedBy { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("RankedBy")]
        public Guid RankedById { get; set; }
        public virtual Meal RankedMeal { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("RankedMeal")]
        public Guid RankedMealId { get; set; }
        public int Ranking { get; set; }
        public string Comment { get; set; }
    }
}
