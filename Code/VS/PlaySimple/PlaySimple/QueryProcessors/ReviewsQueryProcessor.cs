using Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public interface IReviewsQueryProcessor
    {
        IEnumerable<DTOs.Review> Search(int reviewedCustomerId);

        DTOs.Review Get(int id);

        DTOs.Review SaveOrUpdate(DTOs.Review review);
    }

    public class ReviewsQueryProcessor : DBAccessBase<Review>, IReviewsQueryProcessor
    {
        public ReviewsQueryProcessor(ISession session) : base(session)
        {

        }

        public IEnumerable<DTOs.Review> Search(int reviewedCustomerId)
        {
            return null;
        }

        public DTOs.Review Get(int id) { return null; }

        public DTOs.Review SaveOrUpdate(DTOs.Review review) { return null; }
    }
}