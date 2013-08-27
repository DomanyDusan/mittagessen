using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mittagessen.Data.Entities;

namespace Mittagessen.Web.Models
{
    public class EnrollmentsModel
    {
        public IEnumerable<EnrollmentEntry> Entries { get; set; }

        public EnrollmentsModel(IEnumerable<Enrollment> enrollments)
        {            
        }
    }
}