using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Entities;
using Mittagessen.Web.Areas.Sprava.Models;
using Mittagessen.Web.Helpers;
using Mittagessen.Web.Infrastructure;
using StructureMap.Attributes;
using Mittagessen.Data.Interfaces;

namespace Mittagessen.Web.Areas.Sprava.Controllers
{
    [Authorize(Roles = "admin")]
    public class LunchController : Controller
    {
        [SetterProperty]
        public IMealRepository MealRepository { get; set; }

        [SetterProperty]
        public ILunchRepository LunchRepository { get; set; }

        public ActionResult Index()
        {
            var lunches = LunchRepository.GetAll();

            return View(lunches);
        }

        [HttpGet]
        public ActionResult Create(Guid? mealId = null)
        {           
            var lunchModel = new LunchModel()
            {
                LunchDate = LunchRepository.NextLunchDate(),
                LunchTime = TimeSpan.FromHours(12) + TimeSpan.FromMinutes(30),
                NumberOfPortions = 10
            };

            if (mealId.HasValue)
            {
                var meal = MealRepository.Get(mealId.Value);
                lunchModel.CookedMealId = meal.Id;
                lunchModel.CookedMealName = meal.Name;
            }

            return View(lunchModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult CreatePartial()
        {
            var lunchModel = new LunchModel()
            {
                LunchDate = LunchRepository.NextLunchDate(),
                LunchTime = TimeSpan.FromHours(12) + TimeSpan.FromMinutes(30),
                NumberOfPortions = 10
            };
            return View(lunchModel);
        }

        [HttpPost]
        public ActionResult Create(LunchModel lunchModel)
        {
            if (ModelState.IsValid)
            {
                var lunch = new Lunch();
                Mapper.Map(lunchModel, lunch);
                LunchRepository.Insert(lunch);

                return RedirectToAction("Index");
            }
            else
            {
                return View(lunchModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var lunch = LunchRepository.Get(id);
            var lunchModel = Mapper.Map(lunch, new LunchModel());

            return View(lunchModel);
        }

        [HttpPost]
        public ActionResult Edit(LunchModel lunchModel)
        {
            if (ModelState.IsValid)
            {
                var lunch = LunchRepository.Get(lunchModel.LunchId);
                Mapper.Map(lunchModel, lunch);
                LunchRepository.Update(lunch);

                UpdateLunchInfoOnClients(lunch);

                return RedirectToAction("Index");
            }
            else
            {
                return View(lunchModel);
            }
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var lunch = LunchRepository.Get(id);
            LunchRepository.Delete(lunch);
            return RedirectToAction("Index");
        }

        public ActionResult MealList()
        {
            var meals = MealRepository.GetAll();
            return View(meals);
        }

        [NonAction]
        private void UpdateLunchInfoOnClients(Lunch lunch)
        {
            var hub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Hubs.EnrollmentHub>();
            hub.Clients.All.lunchInfoUpdated(lunch.Id, lunch.NumberOfEnrollments, lunch.NumberOfPortions);
        }
    }
}
