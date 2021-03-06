﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using Mittagessen.Web.Helpers;
using Mittagessen.Web.Models;
using StructureMap.Attributes;
using System.Threading;

namespace Mittagessen.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [SetterProperty]
        public ILunchRepository LunchRepository { get; set; }

        [SetterProperty]
        public IUserRepository UserRepository { get; set; }

        [SetterProperty]
        public IMealRepository MealRepository { get; set; }

        [SetterProperty]
        public IEnrollmentRepository EnrollmentRepository { get; set; }

        public ActionResult Index()
        {
            var thisWeekLunches = LunchRepository.GetLunchesForThisWeek().ToList();
            var user = UserRepository.GetUserByName(User.Identity.Name);
            if (user == null)
                return RedirectToAction("LogOff", "Account");
            var model = new EnrollmentModel()
                            {
                                MyLunches = new HashSet<Guid>(user.Enrollments.Select(e => e.EnrolledForLunchId)),
                                Lunches = thisWeekLunches
                            };

            var userRatings = MealRepository.GetUserRatings(user.Id);
            var meals = MealRepository.GetAll();
            var ratingModel = new RatingModel()
            {
                Meals = meals,
                UserRatings = userRatings.ToDictionary(m => m.RatedMealId, m => m.Rating)
            };
            model.RatingModel = ratingModel;

            return View(model);
        }

        public ActionResult InfoPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnrollUser(Guid lunchId, Guid? variation = null)
        {
            var user = UserRepository.GetUserByName(User.Identity.Name);
            var enrollment = new Enrollment()
            {
                EnrolledById = user.Id,
                EnrolledForLunchId = lunchId,
                EnrollmentDate = DateTime.Now,
                MealVariationId = variation
            };
            var success = EnrollmentRepository.TryInsert(enrollment);
            var lunch = LunchRepository.Get(lunchId);
            UpdateLunchInfoOnClients(lunch);

            return Json(new
            {
                userEnrolled = success,
                numberOfEnrollments = lunch.NumberOfEnrollments,
                numberOfPortions = lunch.NumberOfPortions
            });
        }

        [HttpPost]
        public ActionResult DisenrollUser(Guid lunchId)
        {
            var user = UserRepository.GetUserByName(User.Identity.Name);
            var enrollment = EnrollmentRepository.Get(user.Id, lunchId);
            var success = EnrollmentRepository.TryDelete(enrollment);
            var lunch = LunchRepository.Get(lunchId);
            UpdateLunchInfoOnClients(lunch);

            return Json(new
            {
                userEnrolled = !success,
                numberOfEnrollments = lunch.NumberOfEnrollments,
                numberOfPortions = lunch.NumberOfPortions
            });
        }

        [NonAction]
        private void UpdateLunchInfoOnClients(Lunch lunch)
        {
            var hub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Hubs.EnrollmentHub>();
            hub.Clients.All.lunchInfoUpdated(lunch.Id, lunch.NumberOfEnrollments, lunch.NumberOfPortions);
        }
    }
}
