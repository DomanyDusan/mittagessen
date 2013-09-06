using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mittagessen.Web.Models;
using System.Configuration;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using StructureMap.Attributes;
using Mittagessen.Helpers;
using System.Text;

namespace Mittagessen.Web.Controllers
{
    public class AccountController : Controller
    {
        [SetterProperty]
        public IUserRepository UserRepository { get; set; }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult RegistrationPartial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationModel registration)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Name = registration.RegistrationName,
                    Email = registration.Email,
                    RegistrationDate = DateTime.Now,
                    LastAccessDate = DateTime.Now                    
                };
                user.PasswordSalt = PasswordHelper.CreateSalt();
                user.Password = PasswordHelper.GeneratePassword(registration.NewPassword, user.PasswordSalt);
                UserRepository.Insert(user);
                FormsAuthentication.SetAuthCookie(user.Name, false);

                return RedirectToAction("Index", "Home");
            }
            else
                return View(registration);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            var user = new LoginModel()
            {
                RememberLogin = true
            };
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var user = UserRepository.GetUserByName(model.LoginName);
            if(user == null)
            {
                ModelState.AddModelError("LoginName", "The user name provided is incorrect.");
                return View(model);
            }
            var specifiedPasswordHash = PasswordHelper.GeneratePassword(model.LoginPassword, user.PasswordSalt);

            if(PasswordHelper.PasswordsMatch(specifiedPasswordHash, user.Password))
            {
                FormsAuthentication.SetAuthCookie(model.LoginName, model.RememberLogin);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("LoginPassword", "The password provided is incorrect.");
                return View(model);
            }            
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

    }
}
