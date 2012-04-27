using System.Web.Http;
using System.Web.Mvc;

namespace BeatDave.Web.Areas.Api_v1
{
    public class Api_v1AreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Api_v1"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(name: "Api_v1_Default",
                                        routeTemplate: "api/v1/{controller}/{*id}",
                                        defaults: new { id = RouteParameter.Optional });
        }
    }
}