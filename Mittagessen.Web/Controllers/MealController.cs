using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using StructureMap.Attributes;

namespace Mittagessen.Web.Controllers
{
    public class MealController : Controller
    {
        [SetterProperty]
        public ISimpleRepository<Meal> MealRepository { get; set; }

        public ActionResult Index()
        {
            var meals = MealRepository.GetAll();
            return View(meals);
        }
    }
}
