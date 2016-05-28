using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Search()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Authorize(Roles = Consts.Roles.User)]
        public void Save([FromBody]string value)
        {
        }

        [HttpPut]
        [Authorize(Roles = Consts.Roles.User)]
        public void Update(int id, [FromBody]string value)
        {
        }

        [HttpPost]
        [Authorize(Roles = Consts.Roles.User)]
        public void AddComplaint()
        {

        }

        [HttpPost]
        [Authorize(Roles = Consts.Roles.User)]
        public void AddReview()
        {

        }

        [HttpPut]
        [Authorize(Roles = Consts.Roles.Admin)]
        public void FreezeUser()
        {

        }
    }
}
