using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap.Attributes;
using Mittagessen.Data.Interfaces;
using Mittagessen.Web.Models;
using Mittagessen.Web.Helpers;

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
            var userModel = new UserEditModel()
            {
                UserId = user.Id,
                UserName = user.Name,
                Email = user.Email
            };
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Edit(UserEditModel userModel)
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

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View(new PasswordChangeModel());
        }

        [HttpPost]
        public ActionResult ChangePassword(PasswordChangeModel model)
        {
            var user = UserRepository.GetUserByName(User.Identity.Name);
            var specifiedPasswordHash = PasswordHelper.GeneratePassword(model.OldPassword, user.PasswordSalt);
            if(!PasswordHelper.PasswordsMatch(specifiedPasswordHash, user.Password))
            {
                ModelState.AddModelError("OldPassword", "Das alte Passwort ist falsch.");
            }
            if(ModelState.IsValid)
            {
                user.Password = PasswordHelper.GeneratePassword(model.NewPassword, user.PasswordSalt);
                UserRepository.Update(user);
                return RedirectToAction("Edit", new {passwordChanged = true});
            }
            else
            {
                return View(model);
            }
        }
    }
}
