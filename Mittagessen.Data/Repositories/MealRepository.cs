using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using System.Transactions;

namespace Mittagessen.Data.Repositories
{
    public class MealRepository : SimpleRepository<Meal>, IMealRepository
    {
        public void RateMeal(Guid userId, Guid mealId, double value)
        {
            using (var tr = new TransactionScope())
            {
                var rating = Session.MealRatings.SingleOrDefault(r => r.RatedById == userId && r.RatedMealId == mealId);

                if (rating == null)
                {
                    rating = new MealRating()
                    {
                        Id = Guid.NewGuid(),
                        RatedById = userId,
                        RatedMealId = mealId
                    };
                    Session.MealRatings.Add(rating);
                }
                rating.Rating = value;
                Session.SaveChanges();

                var meal = Get(mealId);
                var averageRating = Session.MealRatings.Where(r => r.RatedMealId == mealId).Average(r => r.Rating);
                meal.AverageRating = Math.Ceiling(averageRating);
                Session.SaveChanges();
                tr.Complete();
            }
        }
    }
}
