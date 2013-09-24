using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap.Attributes;
using Mittagessen.Data.Interfaces;
using Mittagessen.Web.Models;

namespace Mittagessen.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        [SetterProperty]
        public IUserRepository UserRepository { get; set; }

        [HttpGet]
        public ActionResult Edit()
        {
            var user = UserRepository.GetUserByName(User.Identity.Name);
            var userModel = new UserModel()
            {
                UserId = user.Id,
                UserName = user.Name,
                Email = user.Email
            };
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Edit(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserRepository.GetUserByName(User.Identity.Name);
                user.Email = userModel.Email;
                UserRepository.Update(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(userModel);
            }
        }
    }
}
