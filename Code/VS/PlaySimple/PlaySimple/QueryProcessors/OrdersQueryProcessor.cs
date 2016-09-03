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
        IEnumerable<DTOs.Order> Search(int? orderId, int? orderStatusId, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate);

        DTOs.Order GetOrder(int id);

        DTOs.Order Save(DTOs.Order order);

        DTOs.Order Update(DTOs.Order order);
    }


    public class OrdersQueryProcessor : DBAccessBase<Order>, IOrdersQueryProcessor
    {
        private DBAccessBase<Customer> _customersQueryProcessor;
        private DBAccessBase<Field> _fieldsQueryProcessor;
        private IDecodesQueryProcessor _decodesQueryProcessor;

        public OrdersQueryProcessor(ISession session, DBAccessBase<Customer> customersQueryProcessor, DBAccessBase<Field> fieldsQueryProcessor,
            IDecodesQueryProcessor decodesQueryProcessor) : base(session)
        {
            _customersQueryProcessor = customersQueryProcessor;
            _fieldsQueryProcessor = fieldsQueryProcessor;
            _decodesQueryProcessor = decodesQueryProcessor;
        }

        public IEnumerable<DTOs.Order> Search(int? orderId, int? orderStatusId, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate)
        {

            return null;
        }

        public DTOs.Order GetOrder(int id) { return null; }

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



            return null;
        }

        public DTOs.Order Update(DTOs.Order order) { return null; }
    }
}