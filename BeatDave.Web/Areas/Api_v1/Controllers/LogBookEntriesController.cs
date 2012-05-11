using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BeatDave.Domain;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class LogBookEntriesController : FatApiController
    {
        // GET /Api/v1/LogBooks/33/Entries
        public HttpResponseMessage Get(int logBookId)
        {
            Func<HttpResponseMessage> response;

            var logBook = GetVisibleLogBook(logBookId, () => base.RavenSession.Include<LogBook>(x => x.OwnerId).Load<LogBook>(logBookId), out response);

            if (logBook == null)
                return response();

            var entryViews = logBook.GetEntries()
                                    .Select(x => x.MapTo<LogBookView.EntryView>())
                                    .ToList();

            return Ok(entryViews);
        }

        // GET /Api/v1/LogBookss/33/Entries/1
        public HttpResponseMessage Get(int logBookId, int entryId)
        {
            Func<HttpResponseMessage> response;

            var logBook = GetVisibleLogBook(logBookId, () => base.RavenSession.Include<LogBook>(x => x.OwnerId).Load<LogBook>(logBookId), out response);

            if (logBook == null)
                return response();

            var entry = logBook.GetEntries()
                               .SingleOrDefault(x => x.Id == entryId);
            
            if (entry == null)
                return NotFound();

            var entryView = entry.MapTo<LogBookView.EntryView>();

            return Ok(entryView);
        }



        // POST /Api/v1/LogBooks/33/Entries
        public HttpResponseMessage Post([FromUri]int? logBookId, EntryInput entryInput)
        {
            // HACK: Once out of beta the logBookId parameter should be bound from the Url rather than the request body
            //       Stop the parameter being nullable too
            if (logBookId.HasValue == false) logBookId = entryInput.LogBookId;

            Func<HttpResponseMessage> response;

            var logBook = GetOwnedLogBook(logBookId.Value, () => base.RavenSession.Load<LogBook>(logBookId), out response);

            if (logBook == null)
                return response();

            var entry = new Entry();
            entryInput.MapToInstance(entry);
            logBook.LogEntry(entry);

            base.RavenSession.Store(logBook);

            var entryView = entry.MapTo<LogBookView.EntryView>();

            return Created(entryView);
        }



        // PUT /Api/v1/LogBooks/33/Entries
        public HttpResponseMessage Put([FromUri]int? logBookId, EntryInput entryInput)
        {
            // HACK: Once out of beta the logBookId parameter should be bound from the Url rather than the request body
            //       Stop the parameter being nullable too
            if (logBookId.HasValue == false) logBookId = entryInput.LogBookId;

            Func<HttpResponseMessage> response;

            var logBook = GetOwnedLogBook(logBookId.Value, () => base.RavenSession.Load<LogBook>(logBookId), out response);

            if (logBook == null)
                return response();

            var entry = logBook.GetEntries()
                               .SingleOrDefault(x=> x.Id == entryInput.Id);

            if (entry == null)
                return NotFound();

            entryInput.MapToInstance(entry);
            
            var entryView = entry.MapTo<LogBookView.EntryView>();

            return Created(entryView);
        }



        // DELETE /Api/v1/LogBooks/5
        public HttpResponseMessage Delete(int logBookId, int entryId)
        {
            if (entryId <= 0)
                return BadRequest("Entry Id is missing");
            
            Func<HttpResponseMessage> response;

            var logBook = GetOwnedLogBook(logBookId, () => base.RavenSession.Load<LogBook>(logBookId), out response);

            if (logBook == null)
                return response();
                                                
            var entry = logBook.GetEntries()
                               .SingleOrDefault(x => x.Id == entryId);

            if (entry == null)
                return NotFound();

            logBook.DeleteEntry(entry);

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
