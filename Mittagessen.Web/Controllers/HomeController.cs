using System;
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
        public IEnrollmentRepository EnrollmentRepository { get; set; }

        public ActionResult Index()
        {
            var thisWeekLunches = LunchRepository.GetLunchesForThisWeek().ToList();
            var user = UserRepository.GetUserByName(User.Identity.Name);
            var model = new EnrollmentModel()
                            {
                                MyLunches = user.Enrollments.Select(e => e.EnrolledForLunchId).ToList(),
                                Lunches = thisWeekLunches
                            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EnrollUser(Guid lunchId)
        {
            var user = UserRepository.GetUserByName(User.Identity.Name);
            var enrollment = new Enrollment()
            {
                EnrolledById = user.Id,
                EnrolledForLunchId = lunchId,
                EnrollmentDate = DateTime.Now
            };
            var success = EnrollmentRepository.TryInsert(enrollment);
            UpdateLunchInfoOnClients(lunchId);

            return Json(new { success = success });
        }

        [HttpPost]
        public ActionResult DisenrollUser(Guid lunchId)
        {
            var user = UserRepository.GetUserByName(User.Identity.Name);
            var enrollment = EnrollmentRepository.Get(user.Id, lunchId);
            EnrollmentRepository.Delete(enrollment);
            UpdateLunchInfoOnClients(lunchId);

            return Json(new { success = true });
        }

        [NonAction]
        private void UpdateLunchInfoOnClients(Guid lunchId)
        {
            var lunch = LunchRepository.Get(lunchId);
            var hub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Hubs.EnrollmentHub>();
            hub.Clients.All.lunchInfoUpdated(lunchId, lunch.NumberOfEnrollments, lunch.NumberOfPortions);
        }
    }
}
