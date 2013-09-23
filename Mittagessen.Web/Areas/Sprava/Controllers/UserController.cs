using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Interfaces;
using StructureMap.Attributes;

namespace Mittagessen.Web.Areas.Sprava.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        [SetterProperty]
        public IUserRepository UserRepository { get; set; }

        public ActionResult Index()
        {
            var users = UserRepository.GetAll();
            return View(users);
        }

    }
}
