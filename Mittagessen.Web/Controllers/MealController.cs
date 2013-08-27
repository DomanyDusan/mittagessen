using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Entities;

namespace Mittagessen.Web.Controllers
{
    public class MealController : Controller
    {
        //
        // GET: /Meal/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Meal());
        }

        [HttpPost]
        public ActionResult Create(Meal meal)
        {
            return RedirectToAction("Index");
        }
    }
}
