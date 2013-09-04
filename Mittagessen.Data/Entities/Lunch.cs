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
        [UIHint("DateTime")]
        [DisplayName("Datum obeda")]
        public DateTime LunchDate { get; set; }
        public Meal CookedMeal { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("CookedMeal")]
        public Guid CookedMealId { get; set; }
        public IList<Enrollment> Enrollments { get; set; }
        [DisplayName("Pocet porcii")]
        public int NumberOfPortions { get; set; }
    }
}
