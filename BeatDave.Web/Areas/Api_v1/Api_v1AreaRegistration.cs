using System.Web.Http;
using System.Web.Mvc;

namespace BeatDave.Web.Areas.Api_v1
{
    public class Api_v1AreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Api_v1"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {            
            context.Routes.MapHttpRoute(name: "Api_v1_LogBooks",              routeTemplate: "api/v1/LogBooks/{logBookId}",                                        defaults: new { controller = "LogBooks", logBookId = RouteParameter.Optional });
            context.Routes.MapHttpRoute(name: "Api_v1_LogBookEntries",        routeTemplate: "api/v1/LogBooks/{logBookId}/Entries/{entryId}",                      defaults: new { controller = "Entries",  entryId = RouteParameter.Optional });
            context.Routes.MapHttpRoute(name: "Api_v1_LogBookEntryComments",  routeTemplate: "api/v1/LogBooks/{logBookId}/Entries/{entryId}/Comments/{commentId}", defaults: new { controller = "Comments", commentId = RouteParameter.Optional });
            context.Routes.MapHttpRoute(name: "Api_v1_Users",                 routeTemplate: "api/v1/Users/{username}",                                            defaults: new { controller = "Users",    username = RouteParameter.Optional });
        }
    }
}