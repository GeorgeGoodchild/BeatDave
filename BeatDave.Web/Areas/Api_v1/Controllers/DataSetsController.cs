using System.Collections.Generic;
using BeatDave.Web.Infrastructure;

namespace BeatDave.Web.Areas.Api_v1.Controllers
{
    public class DataSetsController : FatApiController
    {
        // GET /api/default1
        public IEnumerable<string> Get(string q, string fields)
        {
            return new string[] { "value1", "value2" };
        }

        // GET /api/default1/5
        public string Get(string id)
        {
            return id;
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
