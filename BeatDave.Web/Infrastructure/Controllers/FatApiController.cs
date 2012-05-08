using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Raven.Client;

namespace BeatDave.Web.Infrastructure
{
    public class FatApiController : ApiController
    {
        // Constants
        protected const int DefaultSkip = 0;
        protected const int DefaultTake = 25;

        // Properties
        public static IDocumentStore DocumentStore { get; set; }
        public IDocumentSession RavenSession { get; set; }


        // Event Overrides
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            RavenSession = (IDocumentSession)HttpContext.Current.Items["CurrentRequestRavenSession"];
        }


        // Http Status Responses
        protected HttpResponseMessage BadRequest(string reasonPhrase)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = reasonPhrase,
                RequestMessage = this.Request
            };
        }

        protected HttpResponseMessage<T> BadRequest<T>(T model, string reasonPhrase)
        {
            return new HttpResponseMessage<T>(model)
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = reasonPhrase,
                RequestMessage = this.Request
            };
        }


        protected HttpResponseMessage Created()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Created,
                RequestMessage = this.Request
            };
        }

        protected HttpResponseMessage<T> Created<T>(T model)
        {
            return new HttpResponseMessage<T>(model)
            {
                StatusCode = HttpStatusCode.Created,
                RequestMessage = this.Request
            };
        }


        protected HttpResponseMessage Ok()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                RequestMessage = this.Request
            };
        }

        protected HttpResponseMessage<T> Ok<T>(T model)
        {
            return new HttpResponseMessage<T>(model)
            {
                StatusCode = HttpStatusCode.OK,
                RequestMessage = this.Request
            };
        }


        protected HttpResponseMessage NotFound()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                RequestMessage = this.Request
            };
        }

        protected HttpResponseMessage<T> NotFound<T>(T model)
        {
            return new HttpResponseMessage<T>(model)
            {
                StatusCode = HttpStatusCode.NotFound,
                RequestMessage = this.Request
            };
        }


        protected HttpResponseMessage Forbidden()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Forbidden,
                RequestMessage = this.Request
            };
        }

        protected HttpResponseMessage<T> Forbidden<T>(T model)
        {
            return new HttpResponseMessage<T>(model)
            {
                StatusCode = HttpStatusCode.Forbidden,
                RequestMessage = this.Request
            };
        }
    }
}