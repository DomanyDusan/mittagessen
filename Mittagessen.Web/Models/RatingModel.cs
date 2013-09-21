using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mittagessen.Data.Entities;

namespace Mittagessen.Web.Models
{
    public class RatingModel
    {
        public IEnumerable<Meal> Meals { get; set; }
        public IDictionary<Guid, double> UserRatings { get; set; }
        public double UserRating(Guid mealId)
        {
            return UserRatings.ContainsKey(mealId) ? UserRatings[mealId] : 0;
        }
    }
}