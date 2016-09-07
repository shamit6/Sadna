using Domain;
using LinqKit;
using NHibernate;
using PlaySimple.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaySimple.QueryProcessors
{
    public interface IOrdersQueryProcessor
    {
        IEnumerable<DTOs.Order> Search(int? orderId, int? ownerId, int?[] orderStatusIds, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate);

        DTOs.Order GetOrder(int id);

        DTOs.Order Save(DTOs.Order order);

        DTOs.Order Update(int id, DTOs.Order order);

        List<DTOs.Order> GetAvailbleOrders(int? fieldId, string fieldName, int? fieldTypeId, DateTime date);
    }


    public class OrdersQueryProcessor : DBAccessBase<Order>, IOrdersQueryProcessor
    {
        private CustomersQueryProcessor _customersQueryProcessor;
        private FieldsQueryProcessor _fieldsQueryProcessor;
        private IDecodesQueryProcessor _decodesQueryProcessor;

        public OrdersQueryProcessor(ISession session, CustomersQueryProcessor customersQueryProcessor, FieldsQueryProcessor fieldsQueryProcessor,
            IDecodesQueryProcessor decodesQueryProcessor) : base(session)
        {
            _customersQueryProcessor = customersQueryProcessor;
            _fieldsQueryProcessor = fieldsQueryProcessor;
            _decodesQueryProcessor = decodesQueryProcessor;
        }

        public IEnumerable<DTOs.Order> Search(int? orderId, int? ownerId, int?[] orderStatusIds, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate)
        {
            var filter = PredicateBuilder.New<Order>(x => true);

            if (orderId.HasValue)
            {
                filter.And(x => x.Id == orderId);
            }

            if (ownerId.HasValue)
            {
                filter.And(x => x.Owner.Id == ownerId);
            }

            if (orderStatusIds != null)
            {
                filter.And(x => orderStatusIds.Contains(x.Status.Id));
            }

            if (fieldId.HasValue)
            {
                filter.And(x => x.Field.Id == fieldId);
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                filter.And(x => x.Field.Name.Contains(fieldName));
            }

            if (startDate.HasValue)
            {
                filter.And(x => x.StartDate >= startDate);
            }

            if (endDate.HasValue)
            {
                DateTime? calcEndDate = endDate.Value.AddDays(1);
                filter.And(x => x.StartDate <= calcEndDate);
            }


            var result = Query().Where(filter).Select(x => new DTOs.Order().Initialize(x));

            return result;
        }

        public DTOs.Order GetOrder(int id)
        {
            return new DTOs.Order().Initialize(Get(id));
        }

        public DTOs.Order Save(DTOs.Order order)
        {
            // TODO remove EndDate from Order
            Order newOrder = new Order()
            {
                Owner = _customersQueryProcessor.Get(order.Owner.Id ?? 0),
                StartDate = order.StartDate,
                Field = _fieldsQueryProcessor.Get(order.Field.Id ?? 0),
                PlayersNumber = order.PlayersNumber,
                Status = _decodesQueryProcessor.Get<OrderStatusDecode>(order.Status),
                Participants = new List<Participant>()
            };

            Order persistedOrder = Save(newOrder);

            return new DTOs.Order().Initialize(persistedOrder);
        }

        // Owner can be changed.
        public DTOs.Order Update(int id, DTOs.Order order)
        {
            Order existingOrder = Get(id);

            if (order.Status != null)
                existingOrder.Status = _decodesQueryProcessor.Get<OrderStatusDecode>(order.Status);

            if (order.Field != null)
                existingOrder.Field = _fieldsQueryProcessor.Get(order.Field.Id ?? 0);

            if (order.PlayersNumber != 0)
                existingOrder.PlayersNumber = order.PlayersNumber;

            if (order.StartDate != null)
                existingOrder.StartDate = order.StartDate;

            Update(id, existingOrder);

            return new DTOs.Order().Initialize(existingOrder);
        }

        // TODO add to doc date is mandator
        public List<DTOs.Order> GetAvailbleOrders(int? fieldId, string fieldName, int? fieldTypeId, DateTime date)
        {
            IList<DTOs.Field> fields = _fieldsQueryProcessor.Search(null, fieldId, fieldName, fieldTypeId).ToList();
            IList<DateTime> possibleDate = DateUtils.PossibleDateOrders(date);

            var possibleEvent = from field in _fieldsQueryProcessor.Search(null, fieldId, fieldName, fieldTypeId).ToList()
                                from dateStart in DateUtils.PossibleDateOrders(date)
                                select new { field, dateStart };

            var order = Search(null, null, null, null, null, date, date);

            var possibleOrders = possibleEvent.Where(a => !order.Any(r => r.StartDate == a.dateStart & r.Field.Id == a.field.Id)).
                Select(possibleOrder => new DTOs.Order()
                {
                    Field = possibleOrder.field,
                    StartDate = possibleOrder.dateStart
                });

            return possibleOrders.ToList();
        }
    }
}