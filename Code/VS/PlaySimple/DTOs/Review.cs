using System;
using Domain;

namespace DTOs
{
    public class Review : Entity<DTOs.Review, Domain.Review>
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual User Reviewer { get; set; }

        public virtual User ReviewedUser { get; set; }

        public override Review Initialize(Domain.Review domain)
        {
            throw new NotImplementedException();
        }
    }
}
