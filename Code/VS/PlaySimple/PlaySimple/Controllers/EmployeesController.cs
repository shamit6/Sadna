﻿using PlaySimple.Common;
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
        public DTOs.Employee Save([FromBody]DTOs.Employee employee)
        {
            return _employessQueryProcessor.Save(employee);
        }

        [HttpPut]
        [TransactionFilter]
        public DTOs.Employee Update([FromUri]int id, [FromBody]DTOs.Employee employee)
        {
            return _employessQueryProcessor.Update(id, employee);
        }

        [HttpDelete]
        [TransactionFilter]
        public void Delete([FromUri]int id)
        {
            _employessQueryProcessor.Delete(id);
        }
    }
}
