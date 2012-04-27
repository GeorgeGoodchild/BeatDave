using System.Collections.Generic;
using BeatDave.Web.Infrastructure;
using BeatDave.Web.Models;
using System;
using BeatDave.Web.Areas.Api_v1.Contracts;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class DataSetsController : FatApiController
    {
        // GET /api/default1
        public IEnumerable<DataSet> Get(string q)
        {
            var dataSets = new List<DataSet>
            {
                new DataSet
                {
                    Id = "datasets/1",
                    Title = "My Weight",
                    Description = "4 Hour Body weight loss tracking",
                    Tags = new List<string>
                    {
                       "Weight Loss",
                       "4 Hour Body"
                    },

                    Units = new Units
                    {
                        Precision = 2,
                        Symbol = "kgs",
                        SymbolPosition = SymbolPosition.After
                    },

                    DataPoints = new List<DataPoint>
                    {
                        new DataPoint
                        {
                            Id = 1,
                            OccurredOn = DateTime.Now.AddDays(-10),
                            Value = 90
                        },
                        new DataPoint
                        {
                            Id = 2,
                            OccurredOn = DateTime.Now.AddDays(-5),
                            Value = 85
                        }
                    },

                    Visibility = Visibility.PublicAnonymous,
                    //AutoShareOn = new List<ISocialNetworkAccount>
                    //{
                    //    new FacebookAccount
                    //    {
                    //    },
                    //    new TwitterAccount
                    //    {
                    //    }
                    //},
                    OwnerId = "users/georgegoodchild"
                }
            };

            dataSets[0].DataPoints[0].DataSet = dataSets[0];

            return dataSets;
        }

        // GET /api/default1/5
        public DataSetContract Get(string id)
        {
            var ds = base.RavenSession.Load<DataSet>(id);

            

        }

        // POST /api/default1
        public void Post(string value)
        {

        }

        // PUT /api/default1/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/default1/5
        public void Delete(int id)
        {
        }
    }
}
