using System.Collections.Generic;
using System.Web.Http;
using VersioningRouteContraint.Models;

namespace VersioningRouteContraint.Controllers
{
    [VersionedRoute("api/values", 2)]
    public class Values2Controller : ApiController
    {
        // GET api/values
        public IEnumerable<Value2> Get()
        {
            return new[] {new Value2 {Name = "value1", Id = 1}, new Value2 {Name = "value2", Id = 2}};
        }

        // GET api/values/5
        public Value2 Get(int id)
        {
            return new Value2 { Name = "value1", Id = id};
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
