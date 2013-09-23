using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Mittagessen.Web.Areas.Sprava.Models;

namespace Mittagessen.Web.Areas.Sprava.Validation
{
    public class LunchModelValidation : AbstractValidator<LunchModel>
    {
        public LunchModelValidation()
        {
            RuleFor(r => r.LunchDate).NotEqual(default(DateTime));
            RuleFor(r => r.LunchTime).NotEqual(default(TimeSpan));
            RuleFor(r => r.CookedMealId).NotEqual(default(Guid)).WithMessage("Milacik, vyber prosim co sa bude jest");
        }
    }
}