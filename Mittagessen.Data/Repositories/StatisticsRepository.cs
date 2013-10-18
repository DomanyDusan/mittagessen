using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Models;
using System.Data.Entity;
using Mittagessen.Data.Interfaces;

namespace Mittagessen.Data.Repositories
{
    public class StatisticsRepository : RepositoryBase, IStatisticsRepository
    {        
        public IEnumerable<Attendance> GetUserAttendance(DateTime fromDate, DateTime toDate)
        {
            return Session.Enrollments.Where(e => e.EnrollmentDate >= fromDate && e.EnrollmentDate <= toDate).Include(e => e.EnrolledBy)
                .GroupBy(e => e.EnrolledBy).Select(g => new Attendance() { UserName = g.Key.Name, UserAttendance = g.Count()})
                .OrderByDescending(a => a.UserAttendance);
        }
    }
}
