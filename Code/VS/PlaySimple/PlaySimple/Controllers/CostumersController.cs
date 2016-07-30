using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class CostumersController : ApiController
    {
        private readonly ICustomersQueryProcessor _customersQueryProcessor;

        private readonly IReviewsQueryProcessor _reviewsQueryProcessor;

        private readonly IComplaintsQueryProcessor _complaintsQueryProcessor;

        public CostumersController(ICustomersQueryProcessor customerQueryProcessor, IReviewsQueryProcessor reviewsQueryProcessor, 
            IComplaintsQueryProcessor complaintsQueryProcessor)
        {
            _customersQueryProcessor = customerQueryProcessor;
            _reviewsQueryProcessor = reviewsQueryProcessor;
            _complaintsQueryProcessor = complaintsQueryProcessor;
        }

        [HttpGet]
        public IEnumerable<DTOs.Customer> Search(string firstName, string lastName, int minAge, int maxAge, int regionId, int customerId)
        {
            return null;
        }

        [HttpGet]
        public DTOs.Customer Get(int id)
        {
            return null;
        }

        [HttpPost]
        [Authorize(Roles = Consts.Roles.Customer)]
        public void Save(DTOs.Customer customer)
        {
        }

        [HttpPut]
        [Authorize(Roles = Consts.Roles.Customer)]
        public void Update(int id, [FromBody]string value)
        {
        }
    }
}
