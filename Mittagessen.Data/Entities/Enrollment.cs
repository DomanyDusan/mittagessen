using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mittagessen.Data.Entities
{
    public class Enrollment : EntityBase
    {
        public DateTime EnrollmentDate { get; set; }
        public Enroller EnrolledBy { get; set; }
        public Lunch EnrolledForLunch { get; set; }
    }
}
