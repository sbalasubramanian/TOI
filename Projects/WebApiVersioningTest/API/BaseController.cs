using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiVersioningTest.API
{
    public abstract class BaseController : ApiController
    {
        protected abstract string Version { get; }

        protected string ApplyVersion(string value)
        {
            return string.Format("{0}_{1}", value, Version);
        }
    }
}
