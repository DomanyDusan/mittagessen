using System.Web.Mvc;

namespace Mittagessen.Web.Areas.Sprava
{
    public class SpravaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sprava";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Sprava_default",
                "Sprava/{controller}/{action}/{id}",
                new { action = "Index", controller = "Lunch", id = UrlParameter.Optional }
            );
        }
    }
}
