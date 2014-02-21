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
using ImageResizer;

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
            var emails = UserRepository.GetEmailAddresses();

            var myMessage = SendGrid.GetInstance();
            myMessage.To = new [] { new MailAddress("einladung@mittagessen.net", "Undisclosed recipients") };
            foreach (var email in emails)
            {
                myMessage.AddBcc(email);
            }

            AddContentAndSend(myMessage);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SendMailAboutLunchesTest()
        {
            var myMessage = SendGrid.GetInstance();
            myMessage.AddTo("ddomany@sba-research.org");

            AddContentAndSend(myMessage);

            return RedirectToAction("Index");
        }

        private void AddContentAndSend(SendGrid myMessage)
        {
            try
            {
                var lunches = LunchRepository.GetLunchesForThisWeek().ToList();

                myMessage.From = new MailAddress("einladung@mittagessen.net", "Mittagessen Service");
                myMessage.Subject = "Herzliche Einladung zum Mittagessen";
                myMessage.AddAttachment(GetImageFile(lunches[0].CookedMeal.ImageName), "dienstag.jpg");
                myMessage.AddAttachment(GetImageFile(lunches[1].CookedMeal.ImageName), "mittwoch.jpg");
                myMessage.AddAttachment(GetImageFile(lunches[2].CookedMeal.ImageName), "donnerstag.jpg");
                myMessage.Html = string.Format(MAIL_TEMPLATE,
                    lunches[0].CookedMeal.Name, "cid:dienstag.jpg", GetImageUrl(lunches[0].CookedMeal.ImageName),
                    lunches[1].CookedMeal.Name, "cid:mittwoch.jpg", GetImageUrl(lunches[1].CookedMeal.ImageName),
                    lunches[2].CookedMeal.Name, "cid:donnerstag.jpg", GetImageUrl(lunches[2].CookedMeal.ImageName));

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
        }

        private string GetImageUrl(string imageName)
        {
            var path = VirtualPathUtility.ToAbsolute(imageName);
            return new Uri(Request.Url, path).AbsoluteUri;
        }

        private System.IO.Stream GetImageFile(string imageName)
        {
            return ResizeImage(System.IO.File.OpenRead(Server.MapPath(VirtualPathUtility.ToAbsolute(imageName))));
        }

        private System.IO.Stream ResizeImage(System.IO.Stream image)
        {
            var settings = new ResizeSettings
                               {
                                   MaxWidth = 200,
                                   Format = "jpg"
                               };
            var target = new System.IO.MemoryStream();
            ImageBuilder.Current.Build(image, target, settings);
            target.Position = 0;
            return target;
        }

        private const string MAIL_TEMPLATE = @"
<p>
Liebe Gäste,
</p>
<p>
wir möchten euch auch diese Woche ganz herzlich zum Mittagessen einladen:
</p>
<p>https://mittagessen.azurewebsites.net</p>
<p>
<table>
<tr>
<td>Dienstag:</td>
<td><b>{0}</b></td>
</tr>
<tr>
<td />
<td>
<!--[if mso]>
    <img width='200' src='{1}' />
<![endif]-->   
<!--[if !mso]><!-->
  <img width='200' style='width:200px;height:200px' src='{2}' />
<!--<![endif]-->    
</td>
</tr>
<tr>
<td>Mittwoch:</td>
<td><b>{3}</b></td>
</tr>
<tr>
<td />
<td>
<!--[if mso]>
    <img width='200' src='{4}' />
<![endif]-->   
<!--[if !mso]><!-->
  <img width='200' style='width:200px;height:200px' src='{5}' />
<!--<![endif]-->    
</td>
</tr>
<tr>
<td>Donnerstag:</td>
<td><b>{6}</b></td>
</tr>
<tr>
<td />
<td>
<!--[if mso]>
    <img width='200' src='{7}' />
<![endif]-->   
<!--[if !mso]><!-->
  <img width='200' style='width:200px;height:200px' src='{8}' />
<!--<![endif]-->    
</td>
</tr>
</table>
</p>
<p>
Liebe Grüße,
</p>
<p>
Mittagessen Service
</p>
</p>";
    }
}
