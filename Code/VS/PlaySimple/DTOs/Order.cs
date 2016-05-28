using System;
using System.Collections.Generic;
using Domain;

namespace DTOs
{
    public class Order : Entity<DTOs.Order, Domain.Order>
    {
        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual User Owner { get; set; }

        public virtual int PlayersNumber { get; set; }

        public virtual Decode Status { get; set; }

        public virtual Field Field { get; set; }

        public virtual IList<int> Participants { get; set; }

        public override Order Initialize(Domain.Order domain)
        {
            throw new NotImplementedException();
        }
    }
}
