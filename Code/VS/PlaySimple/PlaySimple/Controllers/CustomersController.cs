using PlaySimple.Filters;
using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly ICustomersQueryProcessor _customersQueryProcessor;

        private readonly IReviewsQueryProcessor _reviewsQueryProcessor;

        private readonly IComplaintsQueryProcessor _complaintsQueryProcessor;

        public CustomersController(ICustomersQueryProcessor customerQueryProcessor, IReviewsQueryProcessor reviewsQueryProcessor, 
            IComplaintsQueryProcessor complaintsQueryProcessor)
        {
            _customersQueryProcessor = customerQueryProcessor;
            _reviewsQueryProcessor = reviewsQueryProcessor;
            _complaintsQueryProcessor = complaintsQueryProcessor;
        }

        [HttpGet]
        public IEnumerable<DTOs.Customer> Search(string firstName = null, string lastName = null, int? minAge = null, int? maxAge = null, int? regionId = null, int? customerId = null)
        {
            return _customersQueryProcessor.Search(firstName, lastName, minAge, maxAge, regionId, customerId);
        }

        [HttpGet]
        public DTOs.Customer Get(int id)
        {
            return _customersQueryProcessor.GetCustomer(id);
        }

        [HttpPost]
        [TransactionFilter]
        //[Authorize(Roles = Consts.Roles.Customer)]
        public DTOs.Customer Save([FromBody]DTOs.Customer customer)
        {
            return _customersQueryProcessor.Save(customer);
        }

        [HttpPut]
        //[Authorize(Roles = Consts.Roles.Customer)]
        public DTOs.Customer Update(int id, [FromBody]DTOs.Customer customer)
        {
            return _customersQueryProcessor.Update(id, customer);
        }
    }
}
