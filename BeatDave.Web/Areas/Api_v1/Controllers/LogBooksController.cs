using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;
using BeatDave.Web.Models;
using Raven.Client.Linq;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    [BasicAuthorize]
    public class LogBooksController : FatApiController
    {
        // GET /Api/v1/LogBooks
        public HttpResponseMessage Get(string q = "", int skip = DefaultSkip, int take = DefaultTake, string fields = "")
        {
            var stats = new RavenQueryStatistics();

            var logBooks = base.RavenSession.Query<LogBook>()
                                            .Statistics(out stats)
                                            .Skip(skip)
                                            .Take(take)
                                            .ToArray();

            var fieldsArray = fields.Split(new[] { ' ', '+' });

            var logBookViews = from ds in logBooks
                               select ds.MapTo<LogBookView>()
                                        .Squash(fieldsArray);

            return Ok(logBookViews);
        }

        // GET /Api/v1/LogBooks/33
        public HttpResponseMessage Get(int logBookId)
        {
            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            if (logBook.OwnerId != HttpContext.Current.User.Identity.Name)
                return Forbidden();

            var logBookView = logBook.MapTo<LogBookView>();

            return Ok(logBookView);
        }



        // POST /Api/v1/LogBooks
        public HttpResponseMessage<LogBookView> Post(LogBookInput logBookInput)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var logBook = new LogBook() { OwnerId = HttpContext.Current.User.Identity.Name };
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
            if (logBookId == null) logBookId = logBookInput.LogBookId;

            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var logBook = base.RavenSession.Load<LogBook>(logBookId);
            logBookInput.MapToInstance(logBook);
           
            var logBookView = logBook.MapTo<LogBookView>();

            return Ok(logBookView);
        }



        // DELETE /Api/v1/LogBooks/5
        public HttpResponseMessage Delete(int logBookId)
        {
            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            base.RavenSession.Delete(logBook);

            return Ok();
        }
    }
}
