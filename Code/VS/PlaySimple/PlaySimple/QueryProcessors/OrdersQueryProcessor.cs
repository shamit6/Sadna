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

        DTOs.Order Get(int id);

        DTOs.Order SaveOrUpdate(DTOs.Order order);
    }


    public class OrdersQueryProcessor : DBAccessBase<Order>, IOrdersQueryProcessor
    {
        public OrdersQueryProcessor(ISession session) : base(session)
        {

        }

        public IEnumerable<DTOs.Order> Search(int? orderId, int? orderStatusId, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate)
        {
            return null;
        }

        public DTOs.Order Get(int id) { return null; }

        public DTOs.Order SaveOrUpdate(DTOs.Order order) { return null; }
    }
}