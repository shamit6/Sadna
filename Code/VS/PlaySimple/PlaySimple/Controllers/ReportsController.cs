using PlaySimple.DTOs;
using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class ReportsController : ApiController
    {
        private readonly IReportsQueryProcessor _reportsQueryProcessor;

        public ReportsController(IReportsQueryProcessor reportsQueryProcessor)
        {
            _reportsQueryProcessor = reportsQueryProcessor;
        }

        [HttpGet]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Employee)]
        public OffendingCustomersReport GetOffendingCustomersReport(DateTime? fromDate, DateTime? untilDate, int? complaintType)
        {
            return null;
        }

        [HttpGet]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Employee)]
        public CustomersActivityReport GetCustomersActivityReport(string firstName, string lastName, DateTime? fromDate, DateTime? untilDate)
        {
            return null;
        }

        [HttpGet]
        public List<UsingFieldsReport> GetUsingFieldsReport(int? fieldId, string fieldName, DateTime? fromDate = null, DateTime? untilDate = null)
        {
            return _reportsQueryProcessor.GetUsingFieldsReport(fieldId, fieldName, fromDate, untilDate);
        }
    }
}
