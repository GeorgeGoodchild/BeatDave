using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;
using BeatDave.Web.Models;
using Raven.Client.Linq;
using System.Web.Http;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class DataSetsController : FatApiController
    {
        // GET /Api/v1/DataSets
        [Authorize]
        public HttpResponseMessage<List<DataSetView>> Get(string q = "", int skip = DefaultSkip, int take = DefaultTake)
        {
            var dataSets = base.RavenSession.Query<DataSet>()
                                            .Skip(skip)
                                            .Take(take)
                                            .ToArray();

            var dataSetViews = from ds in dataSets
                               select ds.MapTo<DataSetView>();

            return Ok(dataSetViews.ToList());
        }

        // GET /Api/v1/DataSets/33
        [Authorize]
        public HttpResponseMessage<DataSetView> Get(int dataSetId)
        {
            if (dataSetId <= 0)
                return BadRequest<DataSetView>(null, "Data Set Id is missing");

            var dataSet = base.RavenSession.Load<DataSet>(dataSetId);

            if (dataSet == null)
                return NotFound<DataSetView>(null);

            var dataSetView = dataSet.MapTo<DataSetView>();

            return Ok(dataSetView);
        }



        // POST /Api/v1/DataSets
        [Authorize]
        public HttpResponseMessage<DataSetView> Post(DataSetInput dataSetInput)
        {
            if (ModelState.IsValid == false)
                return BadRequest<DataSetView>(null, ModelState.FirstErrorMessage());

            var dataSet = new DataSet();
            dataSetInput.MapToInstance(dataSet);

            base.RavenSession.Store(dataSet);

            var dataSetView = dataSet.MapTo<DataSetView>();

            return Created(dataSetView);
        }



        // PUT /Api/v1/DataSets/33
        [Authorize]
        public HttpResponseMessage Put([FromUri]int? dataSetId, DataSetInput dataSetInput)
        {
            // HACK: Once out of beta the dataSetId parameter should be bound from the Url rather than the request body
            //       Stop the parameter being nullable too
            if (dataSetId == null) dataSetId = dataSetInput.DataSetId;

            if (dataSetId <= 0)
                return BadRequest<DataSetView.DataPointView>(null, "Data Set Id is missing");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var dataSet = base.RavenSession.Load<DataSet>(dataSetId);
            dataSetInput.MapToInstance(dataSet);
           
            var dataSetView = dataSet.MapTo<DataSetView>();

            return Ok(dataSetView);
        }



        // DELETE /Api/v1/DataSets/5
        [Authorize]
        public HttpResponseMessage Delete(int dataSetId)
        {
            if (dataSetId <= 0)
                return BadRequest("DataSet Id is missing");

            var dataSet = base.RavenSession.Load<DataSet>(dataSetId);

            base.RavenSession.Delete(dataSet);

            return Ok();
        }
    }
}
