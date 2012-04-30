using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using AutoMapper;
using BeatDave.Web.Infrastructure;
using BeatDave.Web.Models;
using BeatDave.Web.Areas.Api_v1.Models;
using DataAnnotationsExtensions;
using Raven.Client.Linq;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class DataSetsController : FatApiController
    {
        // GET /Api/v1/DataSets
        public HttpResponseMessage<List<DataSetView>> Get(string q, int skip, int take)
        {
            var dataSets = base.RavenSession.Query<DataSet>()
                                            .Skip(0)
                                            .Take(10)
                                            .ToArray();

            var dataSetViews = from ds in dataSets
                               select ds.MapTo<DataSetView>();

            return Ok(dataSetViews.ToList());
        }

        // GET /Api/v1/DataSets/5
        public HttpResponseMessage<DataSetView> Get(int dataSetId)
        {
            if (dataSetId <= 0)
                return BadRequest<DataSetView>(null, "DataSet Id is missing");

            var dataSet = base.RavenSession.Load<DataSet>(dataSetId);

            if (dataSet == null)
                return NotFound<DataSetView>(null);

            var dataSetView = dataSet.MapTo<DataSetView>();

            return Ok(dataSetView);
        }

        // POST /Api/v1/DataSets
        public HttpResponseMessage Post(DataSetInput dataSetInput)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var dataSet = new DataSet();
            dataSetInput.MapToInstance(dataSet);

            this.RavenSession.Store(dataSet);

            var dataSetView = dataSet.MapTo<DataSetView>();

            return Created(dataSetView);
        }

        // PUT /Api/v1/DataSets
        public HttpResponseMessage Put(DataSetInput dataSetInput)
        {
            if (dataSetInput.IsNewDataSet())
                ModelState.AddModelError("Id", "DataSet Id is missing");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.FirstErrorMessage());

            var dataSet = this.RavenSession.Load<DataSet>(dataSetInput.Id);
            dataSetInput.MapToInstance(dataSet);

            var dataSetView = dataSet.MapTo<DataSetView>();

            return Ok(dataSetView);
        }

        // DELETE /Api/v1/DataSets/5
        public HttpResponseMessage Delete(int dataSetId)
        {
            if (dataSetId <= 0)
                return BadRequest("DataSet Id is missing");

            var dataSet = this.RavenSession.Load<DataSet>(dataSetId);

            this.RavenSession.Delete(dataSet);

            return Ok();
        }
    }
}
