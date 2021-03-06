using System;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using BeatDave.Domain;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;
using Raven.Client;
using Raven.Client.Linq;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    [BasicAuthorize]
    public class LogBooksController : FatApiController
    {
        // C'tor
        public LogBooksController(IDocumentSession documentSession, Func<IPrincipal> user)
            : base(documentSession, user)
        { }


        // GET /Api/v1/LogBooks
        public HttpResponseMessage Get(string q = "", 
                                       int skip = AppConstants.DefaultSkip, 
                                       int take = AppConstants.DefaultTake, 
                                       string fields = "")
        {
            if (take > AppConstants.MaxTake)
                return BadRequest(string.Concat("Maximum take value is ", take));

            var stats = new RavenQueryStatistics();            

            //
            // TODO: Create an index that includes the user friends so we can verify it's visible to the current user without all the ToArray() calls.  
            //       Also, Skip and Take will return the wrong number of results at the moment
            //
            var logBooks = base.RavenSession.Query<LogBook>()
                                            .Statistics(out stats)
                                            .Skip(skip)
                                            .Take(take)
                                            .ToArray()
                                            .Where(x => x.IsVisibleTo(base.User.Identity.Name, (ownerId) => base.RavenSession.Load<User>(ownerId).GetFriends()))
                                            .ToArray();

            var fieldsArray = fields.Split(new[] { ' ', '+' })
                                    .Where(x => string.IsNullOrWhiteSpace(x) == false);

            var logBookViews = from lb in logBooks
                               select lb.MapTo<LogBookView>();

            if (fieldsArray.Count() == 0)
                return Ok(logBookViews);

            var squashedLogBooks = logBookViews.Select(x => x.SquashTo(fieldsArray));

            return Ok(squashedLogBooks);
        }

        // GET /Api/v1/LogBooks/33
        public HttpResponseMessage Get(int logBookId)
        {
            var logBook = base.RavenSession.Include<LogBook>(x => x.OwnerId)
                                           .Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            if (logBook.IsVisibleTo(base.User.Identity.Name, (ownerId) => base.RavenSession.Load<User>(ownerId).GetFriends()) == false)
                return Forbidden();

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
            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var logBook = base.RavenSession.Load<LogBook>(logBookId);
            
            if (logBook == null)
                return NotFound();
            
            if (logBook.IsOwnedBy(base.User.Identity.Name) == false)
                return Forbidden();

            logBookInput.MapToInstance(logBook);            

            var logBookView = logBook.MapTo<LogBookView>();

            return Ok(logBookView);
        }



        // DELETE /Api/v1/LogBooks/5
        public HttpResponseMessage Delete(int logBookId)
        {
            var logBook = base.RavenSession.Load<LogBook>(logBookId);
            
            if (logBook == null)
                return NotFound();
            
            if (logBook.IsOwnedBy(base.User.Identity.Name) == false)
                return Forbidden();

            base.RavenSession.Delete(logBook);

            return Ok();
        }
    }
}
