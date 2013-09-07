using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap.Attributes;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Entities;
using Mittagessen.Web.Helpers;

namespace Mittagessen.Web.Controllers
{
    public class OverviewController : Controller
    {
        [SetterProperty]
        public IMealRepository MealRepository { get; set; }

        [SetterProperty]
        public IUserRepository UserRepository { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Meals()
        {
            var meals = MealRepository.GetAll();
            return View(meals);
        }

        [HttpGet]
        public ActionResult MealDetails(Guid mealId)
        {
            var meal = MealRepository.Get(mealId);
            return View(meal);
        }

        [HttpPost]
        public void RateMeal(Guid mealId, double value)
        {
            var user = UserRepository.GetUserByName(User.Identity.Name);
            MealRepository.RateMeal(user.Id, mealId, value);
        }
    }
}
