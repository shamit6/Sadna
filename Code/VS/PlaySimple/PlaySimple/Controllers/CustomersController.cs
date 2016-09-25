using PlaySimple.Filters;
using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Employee + "," + Consts.Roles.Customer)]
        public IEnumerable<DTOs.Customer> Search(string firstName = null, string lastName = null, int? minAge = null, int? maxAge = null, int? regionId = null, int? customerId = null)
        {
            return _customersQueryProcessor.Search(firstName, lastName, minAge, maxAge, regionId, customerId);
        }

        [HttpGet]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Employee + "," + Consts.Roles.Customer)]
        public DTOs.Customer Get(int id)
        {
            return _customersQueryProcessor.GetCustomer(id);
        }

        [HttpPost]
        [TransactionFilter]
        public DTOs.Customer Save([FromBody]DTOs.Customer customer)
        {
            return _customersQueryProcessor.Save(customer);
        }

        [HttpPut]
        [TransactionFilter]
        [Authorize(Roles = Consts.Roles.Admin + "," + Consts.Roles.Customer)]
        public DTOs.CustomerUpdateResponse Update(int id, [FromBody]DTOs.Customer customer)
        {
            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(customer.Username + ":" + customer.Password);
            string authenticationKey = "Basic " + Convert.ToBase64String(toEncodeAsBytes);

            return new DTOs.CustomerUpdateResponse {
                Customer = _customersQueryProcessor.Update(id, customer),
                AuthenticationKey = authenticationKey
            };
        }
    }
}
