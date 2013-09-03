using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mittagessen.Web.Helpers
{
    public static class ControllerExtensions
    {
        public static string AdaptImageUrl(this Controller controller, string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return null;
            else
                return controller.Url.Content("~" + ConfigurationManager.AppSettings["UploadsPath"] + "/" + imageName);
        }
    }
}