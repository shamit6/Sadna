using Domain;
using PlaySimple.Validators;
using System;

namespace PlaySimple.DTOs
{
    public class Participant : Entity<DTOs.Participant , Domain.Participant>
    {
        [ExistsInDB(typeof(Domain.Customer))]
        public virtual Customer Customer { get; set; }

        public virtual DateTime Date { get; set; }

        [ExistsInDB(typeof(Domain.Order))]
        public virtual Order Order { get; set; }

        [IsEnumOfType(typeof(InvitationStatusDecode))]
        public virtual String Status { get; set; }

        public override Participant Initialize(Domain.Participant domain)
        {
            Id = domain.Id;
            Customer = new DTOs.Customer().Initialize(domain.Customer);
            Date = domain.Date;
            Order = new DTOs.Order().Initialize(domain.Order);
            Status = domain.Status.Name;

            return this;
        }
    }
}
