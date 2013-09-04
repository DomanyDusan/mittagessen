using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using Mittagessen.Web.Helpers;
using StructureMap.Attributes;

namespace Mittagessen.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [SetterProperty]
        public ISimpleRepository<Meal> MealRepository { get; set; }

        public ActionResult Index()
        {
            return View();
        }


    }
}
