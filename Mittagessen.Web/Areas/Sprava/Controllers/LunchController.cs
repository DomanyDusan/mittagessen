using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Entities;
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
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CookedMeal = MealRepository.GetAll().Select(x => new SelectListItem() 
                { 
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

            return View();
        }

        [HttpPost]
        public ActionResult Create(Lunch lunch)
        {
            return View(lunch);
        }
    }
}
