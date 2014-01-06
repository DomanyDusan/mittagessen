using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mittagessen.Data.Entities
{
    public class Enrollment : EntityBase
    {
        public DateTime EnrollmentDate { get; set; }
        public virtual User EnrolledBy { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("EnrolledBy")]
        public Guid EnrolledById { get; set; }
        public virtual Lunch EnrolledForLunch { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("EnrolledForLunch")]
        public Guid EnrolledForLunchId { get; set; }
        public string Comment { get; set; }
        public virtual MealVariation MealVariation { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("MealVariation")]
        public Guid? MealVariationId { get; set; }
    }
}
