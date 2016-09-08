using PlaySimple.Filters;
using System.Linq;
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
        private readonly IParticipantsQueryProcessor _participantsQueryProcessor;

        public OrdersController(IOrdersQueryProcessor ordersQueryProcessor, IParticipantsQueryProcessor participantsQueryProcessor)
        {
            _ordersQueryProcessor = ordersQueryProcessor;
            _participantsQueryProcessor = participantsQueryProcessor;
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
            DTOs.Order order = _ordersQueryProcessor.GetOrder(id);
            order.Participants = _participantsQueryProcessor.Search(order.Id, null,
                new int?[] { (int)Consts.Decodes.InvitationStatus.Sent, (int)Consts.Decodes.InvitationStatus.Accepted }).ToList();
            return order;
        }

        // POST: api/Orders
        //[Authorize(Roles = Consts.Roles.Customer)]
        [HttpPost]
        [TransactionFilter]
        public DTOs.Order Save([FromBody]DTOs.Order order)
        {
            return _ordersQueryProcessor.Save(order);
        }


        //[Authorize(Roles = Consts.Roles.Employee + "," + Consts.Roles.Customer)]
        [HttpPut]
        [TransactionFilter]
        public DTOs.Order Update([FromUri]int id, [FromBody]DTOs.Order statusId)
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

        
        [Route("api/orders/optionals")]
        [HttpGet]
        public List<DTOs.Order> SearchOptionalsOrders(int? orderId = null, int? fieldId = null, int? fieldType = null, DateTime? date = null)
        {
            List<DTOs.Order> optionals = _ordersQueryProcessor.GetAvailbleOrders(fieldId, null, fieldType, date??DateTime.Today);

            if (orderId.HasValue)
            {
                DTOs.Order current = _ordersQueryProcessor.GetOrder(orderId ?? 0);

                if (current.Field.Id == fieldId && current.StartDate.Date == date.Value.Date)
                {
                    optionals.Add(current);
                }
                
            }

            return optionals;
        }

        [HttpPut]
        [TransactionFilter]
        [Route("api/orders/updatepraticipant")]
        public DTOs.Order UpdatePraticipant([FromUri]int id, [FromBody]DTOs.Participant participant)
        {
            _participantsQueryProcessor.Update(id, participant);
            return Get(participant.Order.Id??0);
        }

    }
}
