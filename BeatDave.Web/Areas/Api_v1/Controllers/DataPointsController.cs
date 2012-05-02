using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BeatDave.Web.Areas.Api_v1.Models;
using BeatDave.Web.Infrastructure;
using BeatDave.Web.Models;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class DataPointsController : FatApiController
    {
        // GET /Api/v1/DataSets/33/DataPoints
        public HttpResponseMessage<List<DataSetView.DataPointView>> Get(int dataSetId)
        {
            if (dataSetId <= 0)
                return BadRequest<List<DataSetView.DataPointView>>(null, "DataSet Id is missing");

            var dataSet = base.RavenSession.Load<DataSet>(dataSetId);

            if (dataSet == null)
                return NotFound<List<DataSetView.DataPointView>>(null);

            var dataPointViews = from dp in dataSet.DataPoints
                                 select dp.MapTo<DataSetView.DataPointView>();

            return Ok(dataPointViews.ToList());
        }

        // GET /Api/v1/DataSets/33/DataPoints/1
        public HttpResponseMessage<DataSetView.DataPointView> Get(int dataSetId, int dataPointId)
        {
            if (dataSetId <= 0)
                return BadRequest<DataSetView.DataPointView>(null, "Data Set Id is missing");

            if (dataPointId <= 0)
                return BadRequest<DataSetView.DataPointView>(null, "Data Point Id is missing");

            var dataSet = base.RavenSession.Load<DataSet>(dataSetId);

            if (dataSet == null)
                return NotFound<DataSetView.DataPointView>(null);

            var dataPoints = from dp in dataSet.DataPoints
                             where dp.Id == dataPointId
                             select dp;

            var dataPoint = dataPoints.SingleOrDefault();

            if (dataPoint == null)
                return NotFound<DataSetView.DataPointView>(null);

            var dataPointView = dataPoint.MapTo<DataSetView.DataPointView>();

            return Ok(dataPointView);
        }


        // POST /Api/v1/DataSets/33/DataPoints
        public HttpResponseMessage<DataSetView.DataPointView> Post([FromUri]int? dataSetId, DataPointInput dataPointInput)
        {
            if (dataSetId == null) dataSetId = dataPointInput.DataSetId;

            if (dataSetId <= 0)
                return BadRequest<DataSetView.DataPointView>(null, "Data Set Id is missing");

            if (ModelState.IsValid == false)
                return BadRequest<DataSetView.DataPointView>(null, ModelState.FirstErrorMessage());

            var dataSet = base.RavenSession.Load<DataSet>(dataSetId);

            if (dataSet == null)
                return NotFound<DataSetView.DataPointView>(null);

            var dataPoint = new DataPoint();
            dataPointInput.MapToInstance(dataPoint);
            dataSet.AddDataPoint(dataPoint);

            base.RavenSession.Store(dataSet);

            var dataPointView = dataPoint.MapTo<DataSetView.DataPointView>();

            return Created(dataPointView);
        }


        //// PUT /Api/v1/DataSets
        //public HttpResponseMessage Put(int dataSetId, DataPointInput dataSetInput)
        //{
        //    if (dataSetInput.IsNewDataSet())
        //        ModelState.AddModelError("Id", "DataSet Id is missing");

        //    if (ModelState.IsValid == false)
        //        return BadRequest(ModelState.FirstErrorMessage());

        //    var dataSet = base.RavenSession.Load<DataSet>(dataSetInput.Id);
        //    dataSetInput.MapToInstance(dataSet);

        //    var dataSetView = dataSet.MapTo<DataSetView>();

        //    return Ok(dataSetView);
        //}

        //// DELETE /Api/v1/DataSets/5
        //public HttpResponseMessage Delete(int dataSetId, int dataPointId)
        //{
        //    if (id <= 0)
        //        return BadRequest("DataSet Id is missing");

        //    var dataSet = base.RavenSession.Load<DataSet>(id);

        //    base.RavenSession.Delete(dataSet);

        //    return Ok();
        //}
    }
}
