using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;
using BeatDave.Web.Models;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class RecordsController : FatApiController
    {
        // GET /Api/v1/LogBooks/33/Records
        public HttpResponseMessage Get(int logBookId)
        {
            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            var recordViews = from r in logBook.GetRecords()
                              select r.MapTo<LogBookView.RecordView>();

            return Ok(recordViews.ToList());
        }

        // GET /Api/v1/LogBookss/33/Records/1
        public HttpResponseMessage Get(int logBookId, int recordId)
        {
            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            if (recordId <= 0)
                return BadRequest("Record Id is missing");

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null) 
                return NotFound();

            var records = from r in logBook.GetRecords()
                          where r.Id == recordId
                          select r;

            var record = records.SingleOrDefault();

            if (record == null)
                return NotFound();

            var recordView = record.MapTo<LogBookView.RecordView>();

            return Ok(recordView);
        }



        // POST /Api/v1/LogBooks/33/Records
        public HttpResponseMessage Post([FromUri]int? logBookId, RecordInput recordInput)
        {
            // HACK: Once out of beta the logBookId parameter should be bound from the Url rather than the request body
            //       Stop the parameter being nullable too
            if (logBookId == null) logBookId = recordInput.LogBookId;

            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            var record = new Record();
            recordInput.MapToInstance(record);
            logBook.AddRecord(record);

            base.RavenSession.Store(logBook);

            var recordView = record.MapTo<LogBookView.RecordView>();

            return Created(recordView);
        }



        // PUT /Api/v1/LogBooks/33/Records
        public HttpResponseMessage Put([FromUri]int? logBookId, RecordInput recordInput)
        {
            // HACK: Once out of beta the logBookId parameter should be bound from the Url rather than the request body
            //       Stop the parameter being nullable too
            if (logBookId == null) logBookId = recordInput.LogBookId;

            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            var record = logBook.GetRecords().SingleOrDefault(x=> x.Id == recordInput.Id);

            if (record == null)
                return NotFound();

            recordInput.MapToInstance(record);
            
            var recordView = record.MapTo<LogBookView.RecordView>();

            return Created(recordView);
        }



        // DELETE /Api/v1/LogBooks/5
        public HttpResponseMessage Delete(int logBookId, int recordId)
        {
            if (logBookId <= 0)
                return BadRequest("Log Book Id is missing");

            if (recordId <= 0)
                return BadRequest("Record Id is missing");

            var logBook = base.RavenSession.Load<LogBook>(logBookId);

            if (logBook == null)
                return NotFound();

            var record = logBook.GetRecords().SingleOrDefault(x => x.Id == recordId);

            if (record == null)
                return NotFound();

            logBook.RemoveRecord(record);

            return Ok();
        }
    }
}
