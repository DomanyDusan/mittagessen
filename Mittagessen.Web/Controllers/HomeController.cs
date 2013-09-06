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
        public ISimpleRepository<Enrollment> EnrollmentRepository { get; set; }

        public ActionResult Index()
        {
            var thisWeekLunches = LunchRepository.GetLunchesForThisWeek().ToList();
            var user = UserRepository.GetUserByName(User.Identity.Name);
            var model = new EnrollmentModel()
                            {
                                UserId = user.Id,
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
            EnrollmentRepository.Insert(enrollment);

            return Json(new {});
        }
    }
}
