using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Mittagessen.Data.Entities
{
    public class Lunch : EntityBase
    {
        public DateTime LunchDate { get; set; }
        [UIHint("DropDown")]
        public Meal CookedMeal { get; set; }
        public IList<Enrollment> Enrollments { get; set; }
        public int NumberOfPortions { get; set; }
    }
}
