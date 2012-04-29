using System.Collections.Generic;
using BeatDave.Web.Infrastructure;
using BeatDave.Web.Models;
using System;
using BeatDave.Web.Areas.Api_v1.Models;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class DataSetsController : FatApiController
    {
        // GET /api/default1
        public IEnumerable<DataSetView> Get()
        {
            var dataSets = new List<DataSetView>
            {
                new DataSetView
                {                    
                    Id = "datasets/1",
                    Title = "My Weight",
                    Description = "4 Hour Body weight loss tracking",
                    Tags = new List<string>
                    {
                       "Weight Loss",
                       "4 Hour Body"
                    },

                    Units = new DataSetView.UnitsView
                    {
                        Precision = 2,
                        Symbol = "kgs",
                        SymbolPosition = SymbolPosition.After
                    },

                    DataPoints = new List<DataSetView.DataPointView>
                    {
                        new DataSetView.DataPointView
                        {
                            Id = 1,
                            OccurredOn = DateTime.Now.AddDays(-10),
                            Value = 90
                        },
                        new DataSetView.DataPointView
                        {
                            Id = 2,
                            OccurredOn = DateTime.Now.AddDays(-5),
                            Value = 85
                        }
                    },

                    Visibility = Visibility.PublicAnonymous,
                    AutoShareOn = new List<DataSetView.SocialNetworkAccountView>
                    {
                        new DataSetView.SocialNetworkAccountView
                        {
                            SocialNetworkName = "Facebook",
                            SocialNetoworkUserName = "GeorgeGoodchild"
                        },
                        new DataSetView.SocialNetworkAccountView
                        {
                            SocialNetworkName = "Twitter",
                            SocialNetoworkUserName = "GGoodchild"
                        }
                    },
                    Owner = new DataSetView.OwnerView 
                    {
                        OwnerId = "users/georgegoodchild",
                        OwnerName = "George Goodchild"
                    },
                    
                }
            };

            return dataSets;
        }

        // GET /api/default1/5
        public DataSetView Get(string id)
        {
            var ds = base.RavenSession.Load<DataSet>(id);

            return null;
        }

        // POST /api/default1
        public void Post(DataSetInput dataSet)
        {

        }

        // PUT /api/default1/5
        public void Put(DataSetInput dataSet)
        {
        }

        // DELETE /api/default1/5
        public void Delete(string dataSetId)
        {
        }
    }
}
