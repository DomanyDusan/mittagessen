﻿using System;
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
                if (lunch.NumberOfEnrollments >= lunch.NumberOfPortions)
                    return false;
                
                base.Insert(enrollment);                
                lunch.NumberOfEnrollments = lunch.Enrollments.Count;                
                Session.SaveChanges();
                tr.Complete();
                return true;
            }
        }

        public override void Delete(Enrollment enrollment)
        {
            using (var tr = new TransactionScope())
            {
                var lunch = Session.Lunches.Find(enrollment.EnrolledForLunchId);
                base.Delete(enrollment);
                lunch.NumberOfEnrollments = lunch.Enrollments.Count;
                Session.SaveChanges();
                tr.Complete();
            }
        }
    }
}
