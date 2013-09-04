﻿using System;
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

        public ISimpleRepository<Meal> MealRepository { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Meals()
        {
            var meals = MealRepository.GetAll();
            foreach (var meal in meals)
                meal.ImageName = this.AdaptImageUrl(meal.ImageName);
            return View(meals);
        }
    }
}
