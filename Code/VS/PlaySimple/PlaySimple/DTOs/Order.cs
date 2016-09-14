using System;
using System.Collections.Generic;
using Domain;
using PlaySimple.Validators;
using PlaySimple.Common;

namespace PlaySimple.DTOs
{
    public class Order : Entity<DTOs.Order, Domain.Order>
    {
        //[NotInPast]
        public virtual long StartDate { get; set; }

        //[ExistsInDB(typeof(Domain.Customer))]
        public virtual Customer Owner { get; set; }

        [Above(-1)]
        public virtual int PlayersNumber { get; set; }

        //[IsEnumOfType(typeof(OrderStatusDecode))]
        public virtual int? Status { get; set; }

        public virtual Field Field { get; set; }

        //[ListExistsInDb(typeof(Domain.Participant))]
        public virtual IList<DTOs.Participant> Participants { get; set; }

        public override Order Initialize(Domain.Order domain)
        {
            Order newOrder = new Order();

            newOrder.Id = domain.Id;
            newOrder.StartDate = DateUtils.ConvertToJavaScript(domain.StartDate);
            newOrder.Owner = new DTOs.Customer().Initialize(domain.Owner);
            newOrder.PlayersNumber = domain.PlayersNumber;
            newOrder.Status = domain.Status.Id;
            newOrder.Field = new DTOs.Field().Initialize(domain.Field);

            // TODO add Participants.
            return newOrder;
        }
    }
}
