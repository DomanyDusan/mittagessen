using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Entities;
using Mittagessen.Data.ValueObjects;

namespace Mittagessen.Data.Interfaces
{
    public interface IMealRepository : ISimpleRepository<Meal>
    {
        void RateMeal(Guid userId, Guid mealId, double value);
        IEnumerable<MealRating> GetUserRatings(Guid userId);
        void AddVariation(Guid mealId, string variationName, bool requiresDeadLine, MealVariationCategory variationCategory = MealVariationCategory.Default);
        void RemoveVariation(Guid variationId);
        void UpdateVariation(Guid variationId, bool requiresDeadline);
    }
}
