using PlaySimple.Common;
using PlaySimple.Filters;
using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    //[Authorize(Roles = Consts.Roles.Admin)]
    public class EmployeesController : ApiController
    {
        private readonly IEmployeesQueryProcessor _employessQueryProcessor;

        public EmployeesController(IEmployeesQueryProcessor employessQueryProcessor)
        {
            _employessQueryProcessor = employessQueryProcessor;
        }

        [HttpGet]
        public IEnumerable<DTOs.Employee> Search(string firstName = null, string lastName = null, string eMail = null, int? id = null)
        {
            return _employessQueryProcessor.Search(firstName, lastName, eMail, id);
        }

        [HttpGet]
        public DTOs.Employee Get(int id)
        {
            return _employessQueryProcessor.GetEmployee(id);
        }

        [HttpPost]
        [TransactionFilter]
        public void Save([FromBody]DTOs.Employee employee)
        {
            _employessQueryProcessor.Save(employee);
        }

        [HttpPut]
        [TransactionFilter]
        public void Update(int id, [FromBody]DTOs.Employee employee)
        {
            _employessQueryProcessor.Update(id, employee);
        }

        [HttpDelete]
        [TransactionFilter]
        public void Delete(int id)
        {
            _employessQueryProcessor.Delete(id);
        }
    }
}
