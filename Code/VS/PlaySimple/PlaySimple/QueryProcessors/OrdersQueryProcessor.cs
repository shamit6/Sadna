using Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public interface IOrdersQueryProcessor
    {
        IEnumerable<DTOs.Order> Search(int? orderId, int? ownerId, string orderStatus, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate);

        DTOs.Order GetOrder(int id);

        DTOs.Order Save(DTOs.Order order);

        DTOs.Order Update(int id, DTOs.Order order);
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

        public IEnumerable<DTOs.Order> Search(int? orderId, int? ownerId, string orderStatus, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate)
        {
            var query = Query();
            
            if (orderId.HasValue)
            {
                query.Where(x => x.Id == orderId);
            }

            if (ownerId.HasValue)
            {
                query.Where(x => x.Owner.Id == ownerId);
            }

            if (orderStatus != null)
            {
                query.Where(x => x.Status.Name == orderStatus);
            }

            if (fieldId.HasValue)
            {
                query.Where(x => x.Field.Id == fieldId);
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                query.Where(x => x.Field.Name.Contains(fieldName));
            }

            if (startDate.HasValue)
            {
                query.Where(x => x.StartDate >= startDate);
            }

            if (startDate.HasValue)
            {
                query.Where(x => x.StartDate <= endDate);
            }

            return query.Select(x => new DTOs.Order().Initialize(x));
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

            Order persistedOrder = SaveOrUpdate(newOrder);

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

            Order newField = SaveOrUpdate(existingOrder);

            return new DTOs.Order().Initialize(newField);
        }
    }
}