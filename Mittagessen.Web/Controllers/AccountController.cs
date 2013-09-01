using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mittagessen.Web.Models;
using System.Configuration;

namespace Mittagessen.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            var user = new UserModel()
            {
                RememberLogin = true
            };
            return View(user);
        }

        [HttpPost]
        public ActionResult LogOn(UserModel model)
        {
            if(model.Password == ConfigurationManager.AppSettings["UserPassword"])
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberLogin);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "The user name or password provided is incorrect.");
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
