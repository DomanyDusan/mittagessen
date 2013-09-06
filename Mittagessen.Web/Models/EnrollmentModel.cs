using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mittagessen.Data.Entities;

namespace Mittagessen.Web.Models
{
    public class EnrollmentModel
    {
        public Guid UserId { get; set; }

        public IList<Lunch> Lunches { get; set; }

        public bool EnrolledByUser(Lunch lunch)
        {
            return lunch.Enrollments != null && lunch.Enrollments.Count(e => e.EnrolledById == UserId) > 0;
        }
    }
}