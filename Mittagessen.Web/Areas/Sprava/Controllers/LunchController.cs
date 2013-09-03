using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Entities;
using Mittagessen.Web.Helpers;
using StructureMap.Attributes;
using Mittagessen.Data.Interfaces;

namespace Mittagessen.Web.Areas.Sprava.Controllers
{
    [Authorize(Roles = "admin")]
    public class LunchController : Controller
    {
        [SetterProperty]
        public ISimpleRepository<Meal> MealRepository { get; set; }

        [SetterProperty]
        public ISimpleRepository<Lunch> LunchRepository { get; set; }

        public ActionResult Index()
        {
            var lunches = LunchRepository.GetAll();

            return View(lunches);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var lunch = new Lunch()
                            {
                                LunchDate = DateTime.Today,
                                NumberOfPortions = 10
                            };
            return View(lunch);
        }

        [HttpPost]
        public ActionResult Create(Lunch lunch)
        {
            if (ModelState.IsValid)
            {
                lunch.CookedMeal = MealRepository.Get(lunch.CookedMeal.Id);
                LunchRepository.Insert(lunch);

                return RedirectToAction("Index");
            }
            else
            {
                return View(lunch);
            }
        }

        [HttpGet]
        public ActionResult MealList()
        {
            var meals = MealRepository.GetAll();
            foreach (var meal in meals)
                meal.ImageName = this.AdaptImageUrl(meal.ImageName);
            return View(meals);
        }
    }
}
