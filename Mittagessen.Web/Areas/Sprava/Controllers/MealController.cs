using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap.Attributes;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Entities;

namespace Mittagessen.Web.Areas.Sprava.Controllers
{
    [Authorize(Roles="admin")]
    public class MealController : Controller
    {
        [SetterProperty]
        public ISimpleRepository<Meal> MealRepository { get; set; }

        public ActionResult Index()
        {
            var meals = MealRepository.GetAll();
            return View(meals);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Meal());
        }

        [HttpPost]
        public ActionResult Create(Meal meal)
        {
            MealRepository.Insert(meal);
            return RedirectToAction("Index");
        }

    }
}
