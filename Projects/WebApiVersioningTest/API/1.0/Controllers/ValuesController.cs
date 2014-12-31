using System.Collections.Generic;
using System.Web.Http;

namespace WebApiVersioningTest.API._1._0.Controllers
{
    public class ValuesController : BaseController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { ApplyVersion("value1"), ApplyVersion("value2") };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return ApplyVersion("value");
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

        protected override string Version
        {
            get { return "1.0"; }
        }
    }
}
