using System.Web.Mvc;

namespace BeatDave.Web.Areas.App
{
    public class AppAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "App"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(name: "App_default",
                             url: "{controller}/{action}/{*id}",
                             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
