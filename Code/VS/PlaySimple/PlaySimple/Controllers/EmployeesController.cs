using PlaySimple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    [Authorize(Roles = Consts.Roles.Admin)]
    public class EmployeesController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Search()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Save([FromBody]string value)
        {
        }

        [HttpPut]
        public void Update(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
