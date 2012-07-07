using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BeatDave.Domain;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class EntriesController : FatApiController
    {
        // GET /Api/v1/LogBooks/33/Entries
        public HttpResponseMessage Get(int logBookId)
        {   
            var logBook = base.RavenSession.Include<LogBook>(x => x.OwnerId)
                                           .Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            if (logBook.IsVisibleTo(base.User.Identity.Name, (ownerId) => base.RavenSession.Load<User>(ownerId).Friends) == false)
                return Forbidden();
            
            var entryViews = logBook.GetEntries()
                                    .Select(x => x.MapTo<LogBookView.EntryView>())
                                    .ToList();

            return Ok(entryViews);
        }

        // GET /Api/v1/LogBookss/33/Entries/1
        public HttpResponseMessage Get(int logBookId, int entryId)
        {
            var logBook = base.RavenSession.Include<LogBook>(x => x.OwnerId)
                                           .Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            if (logBook.IsVisibleTo(base.User.Identity.Name, (ownerId) => base.RavenSession.Load<User>(ownerId).Friends) == false)
                return Forbidden();

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

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            if (logBook.IsOwnedBy(base.User.Identity.Name) == false)
                return Forbidden();

            var entry = new Entry();
            entryInput.MapToInstance(entry);
            logBook.AddEntry(entry);

            base.RavenSession.Store(logBook);

            var entryView = entry.MapTo<LogBookView.EntryView>();

            return Created(entryView);
        }



        // PUT /Api/v1/LogBooks/33/Entries/1
        public HttpResponseMessage Put([FromUri]int? logBookId, [FromUri]int? entryId, EntryInput entryInput)
        {
            // HACK: Once out of beta the logBookId parameter should be bound from the Url rather than the request body
            //       Stop the parameter being nullable too
            if (logBookId.HasValue == false) logBookId = entryInput.LogBookId;
            if (entryId.HasValue == false) entryId = entryInput.Id;

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            if (logBook.IsOwnedBy(base.User.Identity.Name) == false)
                return Forbidden();

            var entry = logBook.GetEntries()
                               .SingleOrDefault(x=> x.Id == entryId);

            if (entry == null)
                return NotFound();

            entryInput.MapToInstance(entry);
            
            var entryView = entry.MapTo<LogBookView.EntryView>();

            return Created(entryView);
        }



        // DELETE /Api/v1/LogBooks/5/Entries/27
        public HttpResponseMessage Delete(int logBookId, int entryId)
        {
            if (entryId <= 0)
                return BadRequest("Entry Id is missing");

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            if (logBook.IsOwnedBy(base.User.Identity.Name) == false)
                return Forbidden();
                                                
            var entry = logBook.GetEntries()
                               .SingleOrDefault(x => x.Id == entryId);

            if (entry == null)
                return NotFound();

            logBook.RemoveEntry(entry);

            return Ok();
        }
    }
}
