using System.Web.Http.Filters;

namespace BeatDave.Web.Infrastructure
{
    public class SaveChangesOnExit : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
                return;

            var fatApiController = actionExecutedContext.ActionContext.ControllerContext.Controller as FatApiController;

            if (fatApiController == null)
                return;

            fatApiController.RavenSession.SaveChanges();
        }
    }
}