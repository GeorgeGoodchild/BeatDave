using System.Web.Mvc;
using System.Xml.Linq;

namespace BeatDave.Web.Infrastructure
{
    public class FatController : Controller
    {
        // Friendly ActionResults
        protected ActionResult Xml(XDocument xml, string etag)
        {
            return new XmlResult(xml, etag);
        }
    }
}
