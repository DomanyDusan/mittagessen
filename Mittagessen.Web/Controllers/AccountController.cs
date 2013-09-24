using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mittagessen.Web.Helpers;
using Mittagessen.Web.Models;
using System.Configuration;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using StructureMap.Attributes;

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
            return View(new RegistrationModel());
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
                var password = registration.UseDefaultPassword
                    ? ConfigurationManager.AppSettings["UserPassword"]
                    : registration.NewPassword;
                user.PasswordSalt = PasswordHelper.CreateSalt();
                user.Password = PasswordHelper.GeneratePassword(password, user.PasswordSalt);
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
            var user = UserRepository.GetUserByNameOrEmail(model.LoginName);
            if(user == null)
            {
                ModelState.AddModelError("LoginName", "Der Benutzername ist falsch.");
                return View(model);
            }
            var specifiedPasswordHash = PasswordHelper.GeneratePassword(model.LoginPassword, user.PasswordSalt);

            if(PasswordHelper.PasswordsMatch(specifiedPasswordHash, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Name, model.RememberLogin);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("LoginPassword", "Das Passwort ist falsch.");
                return View(model);
            }            
        }

        public ActionResult LogOff()
        {

            FormsAuthentication.SignOut();

            return RedirectToAction("LogOn");
        }

        public JsonResult UserNameAvailable(string RegistrationName)
        {
            var userNameAvailable = UserRepository.UserNameAvailable(RegistrationName);

            return Json(userNameAvailable, JsonRequestBehavior.AllowGet);           
        }

        public JsonResult EmailAddressAvailable(string Email)
        {
            var emailAvailable = UserRepository.EmailAddressAvailable(Email);

            return Json(emailAvailable, JsonRequestBehavior.AllowGet);
        }
    }
}
