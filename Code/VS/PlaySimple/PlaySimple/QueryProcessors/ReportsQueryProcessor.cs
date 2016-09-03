using PlaySimple.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PlaySimple.QueryProcessors
{
    public interface IReportsQueryProcessor
    {
        OffendingCustomersReport GetOffendingCustomersReport(DateTime? fromDate, DateTime? untilDate, int? complaintType);
        CustomersActivityReport GetCustomersActivityReport(string firstName, string lastName, DateTime? fromDate, DateTime? untilDate);
        UsingFieldsReport GetUsingFieldsReport(int fieldId, string fieldName, DateTime? fromDate, DateTime? untilDate);
    }

    public class ReportsQueryProcessor : IReportsQueryProcessor
    {
        private readonly ICustomersQueryProcessor _customerQueryProcessor;
        private readonly IFieldsQueryProcessor _fieldsQueryProcesoor;

        public ReportsQueryProcessor(ICustomersQueryProcessor customerQueryProcessor, IFieldsQueryProcessor fieldsQueryProcessor)
        {
            _customerQueryProcessor = customerQueryProcessor;
            _fieldsQueryProcesoor = fieldsQueryProcessor;
        }

        public OffendingCustomersReport GetOffendingCustomersReport(DateTime? fromDate, DateTime? untilDate, int? complaintType)
        {
            return null;
        }
        public CustomersActivityReport GetCustomersActivityReport(string firstName, string lastName, DateTime? fromDate, DateTime? untilDate)
        {
            return null;
        }

        public UsingFieldsReport GetUsingFieldsReport(int fieldId, string fieldName, DateTime? fromDate, DateTime? untilDate)
        {
            return null;
        }
    }
}