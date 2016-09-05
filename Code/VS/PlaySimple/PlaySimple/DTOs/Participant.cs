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
        public virtual int? Status { get; set; }

        public override Participant Initialize(Domain.Participant domain)
        {
            Participant newParticipant = new Participant();

            newParticipant.Id = domain.Id;
            newParticipant.Customer = new DTOs.Customer().Initialize(domain.Customer);
            newParticipant.Date = domain.Date;
            newParticipant.Order = new DTOs.Order().Initialize(domain.Order);
            newParticipant.Status = domain.Status.Id;

            return newParticipant;
        }
    }
}
