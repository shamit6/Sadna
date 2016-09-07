using PlaySimple.Filters;
using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
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

        [Route("api/orders/search")]
        [HttpGet]
        public List<DTOs.Order> Search(int? orderId = null, int? orderStatusId = null, int? fieldId = null, string fieldName = null, DateTime? fromDate = null, DateTime? untilDate = null)
        {
            var currPrincipal = HttpContext.Current.User as ClaimsPrincipal;
            var currIdentity = currPrincipal.Identity as BasicAuthenticationIdentity;
            int usrId = currIdentity.UserId;
            int?[] statuses = null;
            if (orderStatusId.HasValue)
                statuses = new int?[] { orderStatusId };
            return _ordersQueryProcessor.Search(orderId, usrId, statuses, fieldId, fieldName, fromDate, untilDate);
        }

        // GET: api/Orders/5
        [HttpGet]
        public DTOs.Order Get(int id)
        {
            return _ordersQueryProcessor.GetOrder(id);
        }

        // POST: api/Orders
        //[Authorize(Roles = Consts.Roles.Customer)]
        [HttpPost]
        [TransactionFilter]
        public DTOs.Order Save(DTOs.Order order)
        {
            return _ordersQueryProcessor.Save(order);
        }


        //[Authorize(Roles = Consts.Roles.Employee + "," + Consts.Roles.Customer)]
        [HttpPut]
        [TransactionFilter]
        public DTOs.Order Update(int id, DTOs.Order statusId)
        {
            return _ordersQueryProcessor.Save(statusId);
        }

        [HttpGet]
        public IEnumerable<DTOs.Participant> SearchParticipants(int orderId, int? customerId)
        {
            return null;
        }

        [HttpGet]
        [Route("api/orders/availables")]
        public List<DTOs.Order> SearchAvailableOrders(int? fieldId = null, string fieldName = null, int? fieldType = null, DateTime? date = null)
        {
            return _ordersQueryProcessor.GetAvailbleOrders(fieldId, fieldName, fieldType, date??DateTime.Today);
        }
    }
}
