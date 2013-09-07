using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using System.Data;
using System.Data.Entity;

namespace Mittagessen.Data.Repositories
{
    public class LunchRepository : SimpleRepository<Lunch>, ILunchRepository
    {
        public override Lunch Get(Guid id)
        {
            return Session.Lunches.Include(x => x.CookedMeal).Include(x => x.Enrollments).SingleOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Lunch> GetAll()
        {
            return Session.Lunches
                .AsNoTracking()
                .Include(x => x.CookedMeal).Include(x => x.Enrollments)
                .OrderByDescending(l => l.LunchDate).ToList();
        }

        public IEnumerable<Lunch> GetLunchesForThisWeek()
        {
            var weekStart = DateTime.Today.Subtract(TimeSpan.FromDays((double)DateTime.Today.DayOfWeek));
            var weekEnd = weekStart.AddDays(7);
            return Session.Lunches.Include(x => x.CookedMeal).Where(l => l.LunchDate > weekStart && l.LunchDate < weekEnd);
        }
    }
}
