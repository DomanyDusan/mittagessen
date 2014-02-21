using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mittagessen.Web.Helpers;
using Mittagessen.Web.Infrastructure;
using Mittagessen.Web.Models;
using System.Configuration;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using SendGridMail;
using SendGridMail.Transport;
using StructureMap.Attributes;

namespace Mittagessen.Web.Controllers
{
    public class AccountController : Controller
    {
        [SetterProperty]
        public IUserRepository UserRepository { get; set; }

        [SetterProperty]
        public IAuthentication Authentication { get; set; }

        [SetterProperty]
        public ILunchRepository LunchRepository { get; set; }

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
                //var password = registration.UseDefaultPassword
                //    ? ConfigurationManager.AppSettings["UserPassword"]
                //    : registration.NewPassword;
                var password = registration.NewPassword;
                user.PasswordSalt = PasswordHelper.CreateSalt();
                user.Password = PasswordHelper.GeneratePassword(password, user.PasswordSalt);
                UserRepository.Insert(user);
                Authentication.SaveAuthentication(user.Name, false);

                return RedirectToAction("Index", "Home");
            }
            else
                return View(registration);
        }

        [HttpGet]
        public ActionResult Login()
        {
            var user = new LoginModel()
            {
                RememberLogin = true
            };
            return View(user);
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
                Authentication.SaveAuthentication(user.Name, model.RememberLogin);
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

            Authentication.RemoveAuthentication();

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

        public ActionResult Advertisement()
        {
            var thisWeekLunches = LunchRepository.GetLunchesForThisWeek();
            return View(thisWeekLunches);
        }

        [HttpGet]
        public ActionResult RequestPasswordReset()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RequestPasswordResetConfirmation(string email)
        {
            var user = UserRepository.GetUserByNameOrEmail(email);
            if (user != null)
            {
                var myMessage = SendGrid.GetInstance();
                myMessage.AddTo(user.Email);
                myMessage.From = new MailAddress("passwordreset@mittagessen.net", "Mittagessen Service");
                myMessage.Subject = "Mittagessen Passwort zurücksetzen";
                var passwordResetString = PasswordResetHelper.EncryptString(user.Email);
                myMessage.Html = string.Format(MAIL_TEMPLATE, 
                    Url.Action("ResetPassword", "Account", null, Request.Url.Scheme), 
                    passwordResetString);

                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["MailLogin"], ConfigurationManager.AppSettings["MailPassword"]);

                // Create an SMTP transport for sending email.
                var transportSMTP = SMTP.GetInstance(credentials);

                // Send the email.
                transportSMTP.Deliver(myMessage);
            }
            return View("RequestPasswordResetConfirmation", user);
        }

        [HttpGet]
        public ActionResult ResetPassword(string request)
        {
            var model = new PasswordResetModel()
            {
                PasswordResetString = request
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ResetPasswordConfirmation(PasswordResetModel model)
        {
            User user = null;
            try
            {
                var userEmail = PasswordResetHelper.DecryptString(model.PasswordResetString);
                user = UserRepository.GetUserByEmail(userEmail);
                if (user != null)
                {
                    user.Password = PasswordHelper.GeneratePassword(model.NewPassword, user.PasswordSalt);
                    UserRepository.Update(user);
                }
            }
            catch (Exception)
            {
                user = null;
            }

            return View(user);
        }

        private const string MAIL_TEMPLATE = @"
<p>
Lieber Gast,
</p>
<p>
für dein Benutzerkonto wurde ein Passwort-Reset angefordert.
</p>
<p>
Falls du wünschst dein Passwort zu ändern, fahre bitte unter dem folgenden Link fort. 
</p>
<p>
<a href='{0}?request={1}'>{0}</a>
</p>
<p>
Liebe Grüße,
</p>
<p>
Mittagessen Service
</p>
";
    }
}
