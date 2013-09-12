using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Entities;
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
        public ActionResult Create()
        {
            var lunch = new Lunch()
                            {
                                LunchDate = DateTime.Today.AddHours(12).AddMinutes(30),
                                NumberOfPortions = 10
                            };
            return View(lunch);
        }

        [HttpPost]
        public ActionResult Create(Lunch lunch)
        {
            if (ModelState.IsValid)
            {
                LunchRepository.Insert(lunch);

                return RedirectToAction("Index");
            }
            else
            {
                return View(lunch);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var lunch = LunchRepository.Get(id);

            return View(lunch);
        }

        [HttpPost]
        public ActionResult Edit(Lunch lunch)
        {
            if (ModelState.IsValid)
            {
                LunchRepository.Update(lunch);

                UpdateLunchInfoOnClients(lunch);

                return RedirectToAction("Index");
            }
            else
            {
                return View(lunch);
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
            hub.Clients.All.lunchInfoUpdated(lunch.Id, lunch.NumberOfEnrollments, lunch.NumberOfPortions, lunch.IsFull);
        }
    }
}
