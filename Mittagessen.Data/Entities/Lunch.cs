using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Mittagessen.Data.Entities
{
    public class Lunch : EntityBase
    {
        public DateTime LunchDate { get; set; }
        public virtual Meal CookedMeal { get; set; }
        [ForeignKey("CookedMeal")]
        public Guid CookedMealId { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public int NumberOfPortions { get; set; }
        public int NumberOfEnrollments { get; set; }
    }
}
