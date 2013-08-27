using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mittagessen.Data.Entities
{
    public class Lunch : EntityBase
    {
        public DateTime LunchDate { get; set; }
        public Meal CookedMeal { get; set; }
        public IList<Enrollment> Enrollments { get; set; }
        public int NumberOfPortions { get; set; }
    }
}
