using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mittagessen.Web.Infrastructure;
using Mittagessen.Web.Bootstrap;
using System.Web.Security;
using System.Configuration;
using Mittagessen.Data;

namespace Mittagessen.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            IocConfig.RegisterComponents();
            AppConfig.Initialize();
        }

        protected void Application_EndRequest()
        {
            var dataContext = (DataContext)HttpContext.Current.Items[DbContextManager.DATA_CONTEXT_KEY];
            if (dataContext != null && !dataContext.IsDisposed)
                dataContext.Dispose();
        }

        protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            string username;
#if DEBUG
            if (e.Context.Request.IsLocal)
                username = ConfigurationManager.AppSettings["AdminName"];
            else
#endif
            if (FormsAuthentication.CookiesSupported == false || Request.Cookies[FormsAuthentication.FormsCookieName] == null)
                return;
            else
                username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            try
            {
                string roles = username == ConfigurationManager.AppSettings["AdminName"] ? "admin" : string.Empty;

                //Let us set the Pricipal with our user specific details
                e.User = new System.Security.Principal.GenericPrincipal(
                    new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
            }
            catch (Exception exception)
            {
                Logger.ErrorException("Unhandled exception occured", exception);
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Logger.FatalException("Unhandled exception occured", exception);

            Server.ClearError();
        }
    }
}