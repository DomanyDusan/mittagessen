using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap.Attributes;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Entities;
using Mittagessen.Web.Helpers;
using Mittagessen.Web.Models;

namespace Mittagessen.Web.Controllers
{
    [Authorize]
    public class OverviewController : Controller
    {
        [SetterProperty]
        public IMealRepository MealRepository { get; set; }

        [SetterProperty]
        public IUserRepository UserRepository { get; set; }

        [HttpGet]
        public ActionResult Meals()
        {
            var user = UserRepository.GetUserByName(User.Identity.Name);
            var userRatings = MealRepository.GetUserRatings(user.Id);
            var meals = MealRepository.GetAll();
            var ratingModel = new RatingModel()
            {
                Meals = meals,
                UserRatings = userRatings.ToDictionary(m => m.RatedMealId, m => m.Rating)
            };
            return View(ratingModel);
        }

        [HttpGet]
        public ActionResult MealDetails(Guid mealId)
        {
            var meal = MealRepository.Get(mealId);
            return View(meal);
        }

        [HttpPost]
        public JsonResult RateMeal(Guid mealId, double value)
        {
            var user = UserRepository.GetUserByName(User.Identity.Name);
            MealRepository.RateMeal(user.Id, mealId, value);
            var meal = MealRepository.Get(mealId);
            return Json(new { rating = meal.AverageRatingRounded });
        }
    }
}
