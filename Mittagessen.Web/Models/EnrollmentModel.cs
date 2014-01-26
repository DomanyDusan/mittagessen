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

        public bool IsAfterDeadline(Lunch lunch)
        {
            return lunch.LunchDate < DateTime.Now;
        }

        public bool VariationAfterDeadline(Lunch lunch, MealVariation variation)
        {
            return variation.RequiresDeadLine
                && lunch.LunchDate.AddHours(-18) < DateTime.Now;
        }
    }
}