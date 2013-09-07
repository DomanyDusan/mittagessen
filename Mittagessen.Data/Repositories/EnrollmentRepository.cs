using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using System.Transactions;

namespace Mittagessen.Data.Repositories
{
    public class EnrollmentRepository : SimpleRepository<Enrollment>, IEnrollmentRepository
    {
        public override void Insert(Enrollment enrollment)
        {
            using (var tr = new TransactionScope())
            {
                base.Insert(enrollment);
                var lunch = Session.Lunches.Find(enrollment.EnrolledForLunchId);
                lunch.NumberOfEnrollments = lunch.Enrollments.Count;                
                Session.SaveChanges();
                tr.Complete();
            }
        }
    }
}
