//using System.Web.Http;
//using System.Web.Http.Filters;
//using System.Web.Http.Controllers;

//namespace BeatDave.Web.Infrastructure
//{
//    public class RemoteRequireHttpsAttribute : AuthorizeAttribute
//    {        
//        // C'tor
//        public RemoteRequireHttpsAttribute()
//        { }


//        // AuthorizeAttribute Overrides
//        public override void OnAuthorization(HttpActionContext actionContext)
//        {            
//            if (filterContext.HttpContext.Request.IsLocal)
//                return;
         
//            base.OnAuthorization(actionContext);
//        }
//    }
//}