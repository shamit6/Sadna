using Domain;
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
        IEnumerable<CustomersActivityReport> GetCustomersActivityReport(string firstName, string lastName, DateTime? fromDate, DateTime? untilDate);
        IEnumerable<UsingFieldsReport> GetUsingFieldsReport(int? fieldId, string fieldName, DateTime? fromDate, DateTime? untilDate);
    }

    public class ReportsQueryProcessor : IReportsQueryProcessor
    {
        private readonly ICustomersQueryProcessor _customersQueryProcessor;
        private readonly IComplaintsQueryProcessor _complaintsQueryProcessor;
        private readonly IOrdersQueryProcessor _ordersQueryProcessor;
        private readonly IParticipantsQueryProcessor _participantsQueryProcessor;

        public ReportsQueryProcessor(ICustomersQueryProcessor customersQueryProcessor, IOrdersQueryProcessor ordersQueryProcessor, IComplaintsQueryProcessor complaintsQueryProcessor, IParticipantsQueryProcessor participantsQueryProcessor)
        {
            _customersQueryProcessor = customersQueryProcessor;
            _ordersQueryProcessor = ordersQueryProcessor;
            _complaintsQueryProcessor = complaintsQueryProcessor;
            _participantsQueryProcessor = participantsQueryProcessor;
        }

        public IEnumerable<OffendingCustomersReport> GetOffendingCustomersReport(DateTime? fromDate, DateTime? untilDate, int? complaintType)
        {
            return _complaintsQueryProcessor.Search(null, fromDate, untilDate, complaintType).GroupBy(x => x.OffendingCustomer).
                Select(x => new OffendingCustomersReport()
                {
                    CoustomerId = x.First().OffendingCustomer.Id ?? 0,
                    FirstName = x.First().OffendingCustomer.FirstName,
                    LastName = x.First().OffendingCustomer.LastName,
                    NumberOfComplaints = x.Count()
                });
        }

        public IEnumerable<CustomersActivityReport> GetCustomersActivityReport(string firstName, string lastName, DateTime? fromDate, DateTime? untilDate)
        {
            var customers = _customersQueryProcessor.Search(firstName, lastName, null, null, null, null);
            var report = customers.Select(x => new CustomersActivityReport
                {
                    CoustomerId = x.Id ?? 0,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    NumberOfOrders = _ordersQueryProcessor.Search(null, x.Id, new int?[] { (int)Consts.Decodes.OrderStatus.Accepted }, null, null, fromDate, untilDate).Count(),
                    NumberOfCanceledOrders = _ordersQueryProcessor.Search(null, x.Id, new int?[] { (int)Consts.Decodes.OrderStatus.Canceled }, null, null, fromDate, untilDate).Count(),
                    NumberOfJoiningAsGuest = _participantsQueryProcessor.Search(null, x.Id, new int?[] { (int)Consts.Decodes.InvitationStatus.Accepted }).Count()
                });

            return report;
        }
        public IEnumerable<UsingFieldsReport> GetUsingFieldsReport(int? fieldId, string fieldName, DateTime? fromDate, DateTime? untilDate)
        {
            // 1 = oshar
            return _ordersQueryProcessor.Search(null, null, null, fieldId, fieldName, fromDate, untilDate).GroupBy(f => f.Field).
                Select(x => new UsingFieldsReport()
                {
                    FieldId = x.Key.Id ?? 0,
                    FieldName = x.Key.Name,
                    hours16_18Orders = x.Where(f => f.StartDate.Hour == 16).Count(),
                    hours18_20Orders = x.Where(f => f.StartDate.Hour == 18).Count(),
                    hours20_22Orders = x.Where(f => f.StartDate.Hour == 20).Count(),
                    WeekDayOrders = x.Where(f => f.StartDate.DayOfWeek == DayOfWeek.Friday || f.StartDate.DayOfWeek == DayOfWeek.Saturday).Count(),
                    WeekEndOrders = x.Where(f => !(f.StartDate.DayOfWeek == DayOfWeek.Friday || f.StartDate.DayOfWeek == DayOfWeek.Saturday)).Count()
                });
        }
    }
}