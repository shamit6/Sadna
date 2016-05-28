using Domain;
using PlaySimple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class OrdersController : ApiController
    {
        // GET: api/Orders/5
        [HttpGet]
        public string Get(int id)
        {
            return "hi";
        }

        // POST: api/Orders
        [HttpPost]
        [Authorize(Roles = Consts.Roles.User)]
        public void Save([FromBody]string value)
        {
        }

        // PUT: api/Orders/5
        [HttpPut]
        public void Update(int id, [FromBody]string value)
        {
        }

        [HttpPut]
        [Authorize(Roles = Consts.Roles.Employee + "," + Consts.Roles.User)]
        public void ChangeOrderStatus(int id, [FromBody]int statusId)
        {
            // user can cancel employee can reject/accept
        }

        [HttpGet]
        public void SubmitRequestToJoin(int id)
        {
            // needs to change name because it clashes with get request
        }
    }
}
