using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Interfaces;
using StructureMap.Attributes;
using Mittagessen.Web.Areas.Sprava.Models;

namespace Mittagessen.Web.Areas.Sprava.Controllers
{
    [Authorize(Roles = "admin")]
    public class StatisticsController : Controller
    {
        [SetterProperty]
        public IStatisticsRepository StatisticsRepository { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Attendance(StatisticRangeModel model)
        {
            var attendance = StatisticsRepository.GetUserAttendance(model.FromDate, model.ToDate).ToList();
            return View(attendance);
        }
    }
}
