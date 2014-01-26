using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using System.Transactions;
using Mittagessen.Data.ValueObjects;

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

        public void AddVariation(Guid mealId, string variationName, bool requiresDeadLine, ValueObjects.MealVariationCategory variationCategory = MealVariationCategory.Default)
        {
            var variation = new MealVariation()
            {
                Id = Guid.NewGuid(),
                MealId = mealId,
                Name = variationName,
                RequiresDeadLine = requiresDeadLine,
                Category = variationCategory
            };
            Session.MealVariations.Add(variation);
            Session.SaveChanges();
        }

        public void RemoveVariation(Guid variationId)
        {
            var variation = Session.MealVariations.Find(variationId);
            Session.MealVariations.Remove(variation);
            Session.SaveChanges();
        }

        public void UpdateVariation(Guid variationId, bool requiresDeadline)
        {
            var variation = Session.MealVariations.Find(variationId);
            variation.RequiresDeadLine = requiresDeadline;
            Session.SaveChanges();
        }
    }
}
