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
        public ILunchRepository LunchRepository { get; set; }

        public ActionResult Index()
        {
            var thisWeekLunches = LunchRepository.GetLunchesForThisWeek();

            return View(thisWeekLunches);
        }
    }
}
