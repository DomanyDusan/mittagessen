using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Entities;

namespace Mittagessen.Data.Interfaces
{
    public interface IMealRepository : ISimpleRepository<Meal>
    {
        void RateMeal(Guid userId, Guid mealId, double value);
    }
}
