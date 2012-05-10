using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;
using BeatDave.Web.Models;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class LogBookEntriesController : FatApiController
    {
        // GET /Api/v1/LogBooks/33/Entries
        public HttpResponseMessage Get(int logBookId)
        {
            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            var entryViews = from r in logBook.GetEntries()
                             select r.MapTo<LogBookView.EntryView>();

            return Ok(entryViews.ToList());
        }

        // GET /Api/v1/LogBookss/33/Entries/1
        public HttpResponseMessage Get(int logBookId, int entryId)
        {
            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            if (entryId <= 0)
                return BadRequest("Entry Id is missing");

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null) 
                return NotFound();

            var entries = from r in logBook.GetEntries()
                          where r.Id == entryId
                          select r;

            var entry = entries.SingleOrDefault();

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
            if (logBookId == null) logBookId = entryInput.LogBookId;

            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            var entry = new Entry();
            entryInput.MapToInstance(entry);
            logBook.AddEntry(entry);

            base.RavenSession.Store(logBook);

            var entryView = entry.MapTo<LogBookView.EntryView>();

            return Created(entryView);
        }



        // PUT /Api/v1/LogBooks/33/Entries
        public HttpResponseMessage Put([FromUri]int? logBookId, EntryInput entryInput)
        {
            // HACK: Once out of beta the logBookId parameter should be bound from the Url rather than the request body
            //       Stop the parameter being nullable too
            if (logBookId == null) logBookId = entryInput.LogBookId;

            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            var entry = logBook.GetEntries().SingleOrDefault(x=> x.Id == entryInput.Id);

            if (entry == null)
                return NotFound();

            entryInput.MapToInstance(entry);
            
            var entryView = entry.MapTo<LogBookView.EntryView>();

            return Created(entryView);
        }



        // DELETE /Api/v1/LogBooks/5
        public HttpResponseMessage Delete(int logBookId, int entryId)
        {
            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            if (entryId <= 0)
                return BadRequest("Entry Id is missing");

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            var entry = logBook.GetEntries().SingleOrDefault(x => x.Id == entryId);

            if (entry == null)
                return NotFound();

            logBook.RemoveEntry(entry);

            return Ok();
        }
    }
}
