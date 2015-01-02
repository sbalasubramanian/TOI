using System.Collections.Generic;
using System.Web.Http;
using VersioningRouteContraint.Models;

namespace VersioningRouteContraint.Controllers
{
    [VersionedRoute("api/values", 1)]
    public class Values1Controller : ApiController
    {
        // GET api/values
        public IEnumerable<Value1> Get()
        {
            return new[] {new Value1 {Name = "value1"}, new Value1 {Name = "value2"}};
        }

        // GET api/values/5
        public Value1 Get(int id)
        {
            return new Value1 { Name = "value1" };
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
