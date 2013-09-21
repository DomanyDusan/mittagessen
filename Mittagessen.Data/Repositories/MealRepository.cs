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
                meal.AverageRating = Session.MealRatings.Where(r => r.RatedMealId == mealId).Average(r => r.Rating);
                Session.SaveChanges();
                tr.Complete();
            }
        }

        public IEnumerable<MealRating> GetUserRatings(Guid userId)
        {
            return Session.MealRatings.AsNoTracking().Where(r => r.RatedById == userId).ToList();
        }
    }
}
