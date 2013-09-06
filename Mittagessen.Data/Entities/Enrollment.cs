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
        public User EnrolledBy { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("EnrolledBy")]
        public Guid EnrolledById { get; set; }
        public Lunch EnrolledForLunch { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("EnrolledForLunch")]
        public Guid EnrolledForLunchId { get; set; }
    }
}
