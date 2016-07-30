using PlaySimple.Common;
using PlaySimple.QueryProcessors;
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
        private readonly IEmployeesQueryProcessor _employessQueryProcessor;

        public EmployeesController(IEmployeesQueryProcessor employessQueryProcessor)
        {
            _employessQueryProcessor = employessQueryProcessor;
        }

        [HttpGet]
        public IEnumerable<DTOs.Employee> Search(string firstName, string lastName, int eMail, int id)
        {
            return null;
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
