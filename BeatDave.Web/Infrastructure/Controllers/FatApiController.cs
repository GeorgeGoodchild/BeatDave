using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Raven.Client;

namespace BeatDave.Web.Infrastructure
{
    public class FatApiController : ApiController
    {
        // Properties
        public static IDocumentStore DocumentStore { get; set; }
        public IDocumentSession RavenSession { get; set; }


        // Event Overrides
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            RavenSession = (IDocumentSession)HttpContext.Current.Items["CurrentRequestRavenSession"];
        }
    }    
}