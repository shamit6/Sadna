using PlaySimple.Filters;
using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class ReviewsController : ApiController
    {
        private readonly IReviewsQueryProcessor _reviewsQueryProcessor;

        public ReviewsController(IReviewsQueryProcessor ReviewsQueryProcessor)
        {
            _reviewsQueryProcessor = ReviewsQueryProcessor;
        }

        [HttpGet]
        [Route("api/reviews/search")]
        public List<DTOs.Review> Search(int? customerId = null)
        {
            return _reviewsQueryProcessor.Search(customerId ?? 0).ToList();
        }

        [HttpGet]
        public DTOs.Review Get(int id)
        {
            return _reviewsQueryProcessor.GetReview(id);
        }

        [HttpPost]
        [TransactionFilter]
        public DTOs.Review Save([FromBody]DTOs.Review Review)
        {
            return _reviewsQueryProcessor.Save(Review);
        }

        [HttpPut]
        [TransactionFilter]
        public DTOs.Review Update([FromUri]int id, [FromBody]DTOs.Review Review)
        {
            return _reviewsQueryProcessor.Update(id, Review);
        }
    }
}