using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Raven.Client;

namespace BeatDave.Web.Infrastructure
{
    public class FatController : Controller
    {
        // Properties
        public static IDocumentStore DocumentStore { get; set; }
        public IDocumentSession RavenSession { get; set; }


        // Event Overrides
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = (IDocumentSession)HttpContext.Items["CurrentRequestRavenSession"];
        }


        // Friendly ActionResults
        protected ActionResult Xml(XDocument xml, string etag)
        {
            return new XmlResult(xml, etag);
        }
    }
}
