using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BeatDave.Domain;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;
using Raven.Client.Linq;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    [BasicAuthorize]
    public class LogBooksController : FatApiController
    {
        // GET /Api/v1/LogBooks
        public HttpResponseMessage Get(string q = "", int skip = DefaultSkip, int take = DefaultTake, string fields = "")
        {
            if (take > MaxTake)
                return BadRequest(string.Concat("Maximum take value is ", take));

            var stats = new RavenQueryStatistics();

            //
            // TODO: Create an index that includes the user friends so we can verify it's visible to the current user
            //
            var logBooks = base.RavenSession.Query<LogBook>()
                                            .Statistics(out stats)
                                            .Skip(skip)
                                            .Take(take)
                                            .ToArray();

            var fieldsArray = fields.Split(new[] { ' ', '+' });

            var logBookViews = from lb in logBooks
                               select lb.MapTo<LogBookView>()
                                        .Squash(fieldsArray);

            return Ok(logBookViews);
        }

        // GET /Api/v1/LogBooks/33
        public HttpResponseMessage Get(int logBookId)
        {
            Func<HttpResponseMessage> response;

            var logBook = GetVisibleLogBook(logBookId, () => base.RavenSession.Include<LogBook>(x => x.OwnerId).Load<LogBook>(logBookId), out response);

            if (logBook == null)
                return response();

            var logBookView = logBook.MapTo<LogBookView>();

            return Ok(logBookView);
        }



        // POST /Api/v1/LogBooks
        public HttpResponseMessage Post(LogBookInput logBookInput)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var logBook = new LogBook() { OwnerId = base.User.Identity.Name };
            logBookInput.MapToInstance(logBook);

            base.RavenSession.Store(logBook);

            var logBookView = logBook.MapTo<LogBookView>();

            return Created(logBookView);
        }



        // PUT /Api/v1/LogBooks/33
        public HttpResponseMessage Put([FromUri]int? logBookId, LogBookInput logBookInput)
        {
            // HACK: Once out of beta the logBookId parameter should be bound from the Url rather than the request body
            //       Stop the parameter being nullable too
            if (logBookId.HasValue == false) logBookId = logBookInput.LogBookId;

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            Func<HttpResponseMessage> response;

            var logBook = GetOwnedLogBook(logBookId.Value, () => base.RavenSession.Load<LogBook>(logBookId), out response);

            if (logBook == null)
                return response();
            
            logBookInput.MapToInstance(logBook);
           
            var logBookView = logBook.MapTo<LogBookView>();

            return Ok(logBookView);
        }



        // DELETE /Api/v1/LogBooks/5
        public HttpResponseMessage Delete(int logBookId)
        {
            Func<HttpResponseMessage> response;

            var logBook = GetOwnedLogBook(logBookId, () => base.RavenSession.Load<LogBook>(logBookId), out response);

            if (logBook == null)
                return response();

            base.RavenSession.Delete(logBook);

            return Ok();
        }



        // Private Members        
        [NonAction]
        private LogBook GetOwnedLogBook(int logBookId, Func<LogBook> getLogBook, out Func<HttpResponseMessage> response)
        {
            if (logBookId <= 0)
            {
                response = () => BadRequest("Log Book Id is missing");
                return null;
            }

            var logBook = getLogBook();

            if (logBook == null)
            {
                response = () => NotFound();
                return null;
            }

            if (logBook.IsOwnedBy(base.User.Identity.Name) == false)
            {
                response = () => Forbidden();
                return null;
            }

            response = null;
            return logBook;
        }

        [NonAction]
        private LogBook GetVisibleLogBook(int logBookId, Func<LogBook> getLogBook, out Func<HttpResponseMessage> response)
        {
            if (logBookId <= 0)
            {
                response = () => BadRequest("Log Book Id is missing");
                return null;
            }

            var logBook = getLogBook();

            if (logBook == null)
            {
                response = () => NotFound();
                return null;
            }

            if (logBook.IsVisibleTo(base.User.Identity.Name, (ownerId) => base.RavenSession.Load<User>(ownerId).Friends) == false)
            {
                response = () => Forbidden();
                return null;
            }

            response = null;
            return logBook;
        }
    }
}
