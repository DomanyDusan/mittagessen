using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Interfaces;
using System.Net;
using System.Net.Mail;
using SendGridMail;
using SendGridMail.Transport;
using StructureMap.Attributes;
using System.Configuration;

namespace Mittagessen.Web.Areas.Sprava.Controllers
{
    [Authorize(Roles="admin")]
    public class NotificationController : Controller
    {
        private NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        [SetterProperty]
        public IUserRepository UserRepository { get; set; }
        [SetterProperty]
        public ILunchRepository LunchRepository { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SendMailAboutLunches()
        {
            try
            {
                var emails = UserRepository.GetEmailAddresses();
                var lunches = LunchRepository.GetLunchesForThisWeek().ToList();

                var myMessage = SendGrid.GetInstance();
                myMessage.AddTo("ddomany@sba-research.org");
                myMessage.From = new MailAddress("einladung@mittagessen.net", "Mittagessen Service");
                myMessage.Subject = "Herzliche Einladung zum Mittagessen";
                myMessage.Html = string.Format(MAIL_TEMPLATE,
                    lunches[0].CookedMeal.Name, GetImageUrl(lunches[0].CookedMeal.ImageName),
                    lunches[1].CookedMeal.Name, GetImageUrl(lunches[1].CookedMeal.ImageName),
                    lunches[2].CookedMeal.Name, GetImageUrl(lunches[2].CookedMeal.ImageName));

                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["MailLogin"], ConfigurationManager.AppSettings["MailPassword"]);

                // Create an SMTP transport for sending email.
                var transportSMTP = SMTP.GetInstance(credentials);

                // Send the email.
                transportSMTP.Deliver(myMessage);
            }
            catch (Exception e)
            {
                Logger.FatalException("Email exception occured", e);
            }

            return RedirectToAction("Index");
        }

        private string GetImageUrl(string imageName)
        {
            var path = VirtualPathUtility.ToAbsolute(imageName);
            return new Uri(Request.Url, path).AbsoluteUri;
        }

        private const string MAIL_TEMPLATE = @"
<p>
Hallo,
</p>
<p>
auch diese Woche möchten wir euch ganz herzlich zum Mittagessen einladen:
</p>
<p>
<table>
<tr>
<td>Dienstag:</td>
<td><b>{0}</b></td>
</tr>
<tr>
<td />
<td><img style='width:120px;height:120px' src='{1}' /></td>
</tr>
<tr>
<td>Mittwoch:</td>
<td><b>{2}</b></td>
</tr>
<tr>
<td />
<td><img style='width:120px;height:120px' src='{3}' /></td>
</tr>
<tr>
<td>Donnerstag:</td>
<td><b>{4}</b></td>
</tr>
<tr>
<td />
<td><img style='width:120px;height:120px' src='{5}' /></td>
</tr>
</table>
</p>
<p>
LG,
</p>
<p>
Dusan
</p>
</p>";
    }
}
