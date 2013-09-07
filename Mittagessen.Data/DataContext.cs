using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Mittagessen.Data.Entities;

namespace Mittagessen.Data
{
    public class DataContext : DbContext
    {
        public bool IsDisposed { get; protected set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Lunch> Lunches { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealRating> MealRankings { get; set; }

        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public new void Dispose()
        {
            base.Dispose();
            IsDisposed = true;
        }
    }
}
