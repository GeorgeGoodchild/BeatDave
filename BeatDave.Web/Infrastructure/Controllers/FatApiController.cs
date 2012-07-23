using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using Raven.Client;

namespace BeatDave.Web.Infrastructure
{
    [SaveChangesOnExit]
    public class FatApiController : ApiController
    {
        // Instance Variables
        private readonly Func<IPrincipal> _getUser;

        // Constants
        
        // Properties
        public new IPrincipal User { get { return _getUser(); } }  // HACK: Why isn't the ApiController.User field returning the correct value?
        public IDocumentSession RavenSession { get; private set; }


        // C'tor
        public FatApiController(IDocumentSession documentSession, Func<IPrincipal> getUser)
        {
            this.RavenSession = documentSession;
            _getUser = getUser;
        }


        // Http Status Responses        
        protected HttpResponseMessage Created()
        {
            return base.Request.CreateResponse(HttpStatusCode.Created);
        }

        protected HttpResponseMessage Created<T>(T model)
        {
            return base.Request.CreateResponse(HttpStatusCode.Created, model);
        }

        protected HttpResponseMessage Ok()
        {
            return base.Request.CreateResponse(HttpStatusCode.OK);
        }

        protected HttpResponseMessage Ok<T>(T model)
        {
            return base.Request.CreateResponse(HttpStatusCode.OK, model);
        }
        
        protected HttpResponseMessage NotFound()
        {
            return base.Request.CreateResponse(HttpStatusCode.NotFound);
        }

        protected HttpResponseMessage Forbidden()
        {
            return base.Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        protected HttpResponseMessage BadRequest(string reasonPhrase)
        {
            var response = base.Request.CreateResponse(HttpStatusCode.BadRequest);
            response.ReasonPhrase = reasonPhrase;

            return response;
        }
    }
}