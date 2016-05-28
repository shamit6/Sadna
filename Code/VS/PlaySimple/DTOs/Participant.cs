using System;

namespace DTOs
{
    public class Participant : Entity<DTOs.Participant ,Participant>
    {
        public virtual User User { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual Decode Status { get; set; }

        public override Participant Initialize(Participant domain)
        {
            throw new NotImplementedException();
        }
    }
}
