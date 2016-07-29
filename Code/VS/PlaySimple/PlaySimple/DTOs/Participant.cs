using Domain;
using PlaySimple.Validators;
using System;

namespace PlaySimple.DTOs
{
    public class Participant : Entity<DTOs.Participant ,Participant>
    {
        [ExistsInDB(typeof(Domain.Customer))]
        public virtual User User { get; set; }

        public virtual DateTime Date { get; set; }

        [IsEnumOfType(typeof(InvitationStatusDecode))]
        public virtual int Status { get; set; }

        public override Participant Initialize(Participant domain)
        {
            throw new NotImplementedException();
        }
    }
}
