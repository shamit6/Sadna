using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrdersQueryProcessor _ordersQueryProcessor;
        // participant

        public OrdersController(IOrdersQueryProcessor ordersQueryProcessor)
        {
            _ordersQueryProcessor = ordersQueryProcessor;
        }

        public IEnumerable<DTOs.Order> Search(int? orderId, int? orderStatusId, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate)
        {
            // TODO how to get the id of the current user
            return _ordersQueryProcessor.Search(orderId, 4, new int?[] { orderStatusId }, fieldId, fieldName, startDate, endDate);
        }

        // GET: api/Orders/5
        [HttpGet]
        public DTOs.Order Get(int id)
        {
            return null;
        }

        // POST: api/Orders
        [HttpPost]
        [Authorize(Roles = Consts.Roles.Customer)]
        public void Save(DTOs.Order order)
        {
        }

        [HttpPut]
        [Authorize(Roles = Consts.Roles.Employee + "," + Consts.Roles.Customer)]
        public void Update(int id, DTOs.Order statusId)
        {
            // user can cancel employee can reject/accept
        }

        [HttpGet]
        public IEnumerable<DTOs.Participant> SearchParticipants(int orderId, int? customerId)
        {
            return null;
        }

        [HttpGet]
        public List<DTOs.Order> SearchAvailableOrders(int? fieldId = null, string fieldName = null, int? fieldType = null, DateTime? date = null)
        {
            return _ordersQueryProcessor.GetAvailbleOrders(fieldId, fieldName, fieldType, date??DateTime.Today);
        }
    }
}
