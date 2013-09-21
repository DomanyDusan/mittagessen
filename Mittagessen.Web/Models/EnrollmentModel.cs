using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mittagessen.Data.Entities;

namespace Mittagessen.Web.Models
{
    public class EnrollmentModel
    {
        public HashSet<Guid> MyLunches { get; set; }

        public IList<Lunch> Lunches { get; set; }

        public bool EnrolledByUser(Lunch lunch)
        {
            return MyLunches.Contains(lunch.Id);
        }
    }
}