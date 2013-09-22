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
        public Enrollment Get(Guid userId, Guid lunchId)
        {
            return Session.Enrollments.SingleOrDefault(e => e.EnrolledById == userId && e.EnrolledForLunchId == lunchId);
        }

        public bool TryInsert(Enrollment enrollment)
        {
            using (var tr = new TransactionScope())
            {
                var lunch = Session.Lunches.Find(enrollment.EnrolledForLunchId);
                if (lunch.NumberOfEnrollments >= lunch.NumberOfPortions || lunch.LunchDate < DateTime.Now)
                    return false;
                var oldEnrollment = Session.Enrollments
                    .SingleOrDefault(e => e.EnrolledById == enrollment.EnrolledById && e.EnrolledForLunchId == enrollment.EnrolledForLunchId);
                if (oldEnrollment != null)
                    return false;
                
                base.Insert(enrollment);                
                lunch.NumberOfEnrollments = lunch.Enrollments.Count;                
                Session.SaveChanges();
                tr.Complete();
            }

            return true;
        }

        public bool TryDelete(Enrollment enrollment)
        {
            using (var tr = new TransactionScope())
            {
                var lunch = Session.Lunches.Find(enrollment.EnrolledForLunchId);
                if (lunch.LunchDate < DateTime.Now)
                    return false;
                base.Delete(enrollment);
                lunch.NumberOfEnrollments = lunch.Enrollments.Count;
                Session.SaveChanges();
                tr.Complete();
            }

            return true;
        }
    }
}
