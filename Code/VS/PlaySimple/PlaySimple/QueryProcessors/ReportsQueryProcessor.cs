using Domain;
using PlaySimple.Common;
using PlaySimple.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public interface IReportsQueryProcessor
    {
        IEnumerable<OffendingCustomersReport> GetOffendingCustomersReport(DateTime? fromDate, DateTime? untilDate, int? complaintType);
        IEnumerable<CustomersActivityReport> GetCustomersActivityReport(string firstName, string lastName, int? minAge, int? maxAge, DateTime? fromDate, DateTime? untilDate);
        IEnumerable<UsingFieldsReport> GetUsingFieldsReport(int? fieldId, string fieldName, DateTime? fromDate, DateTime? untilDate);
    }

    public class ReportsQueryProcessor : IReportsQueryProcessor
    {
        private readonly ICustomersQueryProcessor _customersQueryProcessor;
        private readonly IComplaintsQueryProcessor _complaintsQueryProcessor;
        private readonly IOrdersQueryProcessor _ordersQueryProcessor;
        private readonly IParticipantsQueryProcessor _participantsQueryProcessor;
        private readonly IFieldsQueryProcessor _fieldsQueryProcessor;

        public ReportsQueryProcessor(ICustomersQueryProcessor customersQueryProcessor, IOrdersQueryProcessor ordersQueryProcessor, IComplaintsQueryProcessor complaintsQueryProcessor, IParticipantsQueryProcessor participantsQueryProcessor, IFieldsQueryProcessor fieldsQueryProcessor)
        {
            _customersQueryProcessor = customersQueryProcessor;
            _ordersQueryProcessor = ordersQueryProcessor;
            _complaintsQueryProcessor = complaintsQueryProcessor;
            _participantsQueryProcessor = participantsQueryProcessor;
            _fieldsQueryProcessor = fieldsQueryProcessor;
        }

        public IEnumerable<OffendingCustomersReport> GetOffendingCustomersReport(DateTime? fromDate, DateTime? untilDate, int? complaintType)
        {
            return _complaintsQueryProcessor.Search(null, fromDate, untilDate, complaintType).GroupBy(x => x.OffendingCustomer).
                Select(x => new OffendingCustomersReport()
                {
                    CustomerId = x.First().OffendingCustomer.Id ?? 0,
                    FirstName = x.First().OffendingCustomer.FirstName,
                    LastName = x.First().OffendingCustomer.LastName,
                    NumberOfComplaints = x.Count()
                });
        }

        public IEnumerable<CustomersActivityReport> GetCustomersActivityReport(string firstName, string lastName, int? minAge, int? maxAge, DateTime? fromDate, DateTime? untilDate)
        {
            var customers = _customersQueryProcessor.Search(firstName, lastName, minAge, maxAge, null, null);
            var report = customers.Select(x => new CustomersActivityReport
                {
                    CoustomerId = x.Id ?? 0,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = DateUtils.GetAge(x.BirthDate),
                    //LastGameDate = _ordersQueryProcessor.Search(null, x.Id, new int?[] { (int)Consts.Decodes.OrderStatus.Accepted }, null, null, fromDate, untilDate).Max(t => t.StartDate),
                    NumberOfOrders = _ordersQueryProcessor.Search(null, x.Id, new int?[] { (int)Consts.Decodes.OrderStatus.Accepted }, null, null, fromDate, untilDate).Count(),
                    NumberOfCanceledOrders = _ordersQueryProcessor.Search(null, x.Id, new int?[] { (int)Consts.Decodes.OrderStatus.Canceled }, null, null, fromDate, untilDate).Count(),
                    NumberOfJoiningAsGuest = _participantsQueryProcessor.Search(null, x.Id, new int?[] { (int)Consts.Decodes.InvitationStatus.Accepted }).Count()
                });

            return report;
        }
        public IEnumerable<UsingFieldsReport> GetUsingFieldsReport(int? fieldId, string fieldName, DateTime? fromDate, DateTime? untilDate)
        {
            var orders = _ordersQueryProcessor.Search(null, null, new int?[] { (int)Consts.Decodes.OrderStatus.Accepted }, null, null, fromDate, untilDate);
            var report = _fieldsQueryProcessor.Search(null, fieldId, fieldName, null).Select(x => 
             new UsingFieldsReport()
               {
                   FieldId = x.Id ?? 0,
                   FieldName = x.Name,
                   hours16_18Orders = orders.Where(f => x.Id == f.Field.Id && f.StartDate.Hour == 16).Count(),
                   hours18_20Orders = orders.Where(f => x.Id == f.Field.Id && f.StartDate.Hour == 18).Count(),
                   hours20_22Orders = orders.Where(f => x.Id == f.Field.Id && f.StartDate.Hour == 20).Count(),
                   WeekDayOrders = orders.Where(f => x.Id == f.Field.Id && (f.StartDate.DayOfWeek == DayOfWeek.Friday || f.StartDate.DayOfWeek == DayOfWeek.Saturday)).Count(),
                   WeekEndOrders = orders.Where(f => x.Id == f.Field.Id && !(f.StartDate.DayOfWeek == DayOfWeek.Friday || f.StartDate.DayOfWeek == DayOfWeek.Saturday)).Count()
               });


            return report;
        }
    }
}