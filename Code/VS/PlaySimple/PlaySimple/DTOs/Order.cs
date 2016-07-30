using System;
using System.Collections.Generic;
using Domain;
using PlaySimple.Validators;

namespace PlaySimple.DTOs
{
    public class Order : Entity<DTOs.Order, Domain.Order>
    {
        [NotInPast]
        public virtual DateTime StartDate { get; set; }

        [ExistsInDB(typeof(Domain.Customer))]
        public virtual Customer Owner { get; set; }

        [Above(0)]
        public virtual int PlayersNumber { get; set; }

        [IsEnumOfType(typeof(OrderStatusDecode))]
        public virtual int Status { get; set; }

        [IsEnumOfType(typeof(OrderStatusDecode))]
        public virtual Field Field { get; set; }

        [ListExistsInDb(typeof(Domain.Participant))]
        public virtual IList<int> Participants { get; set; }

        public override Order Initialize(Domain.Order domain)
        {
            throw new NotImplementedException();
        }
    }
}
