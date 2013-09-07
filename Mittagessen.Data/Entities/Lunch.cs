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
        public virtual Meal CookedMeal { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("CookedMeal")]
        public Guid CookedMealId { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [DisplayName("Pocet porcii")]
        public int NumberOfPortions { get; set; }
        [HiddenInput(DisplayValue=false)]
        public int NumberOfEnrollments { get; set; }
    }
}
