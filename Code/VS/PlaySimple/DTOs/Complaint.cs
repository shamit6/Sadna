using System;
using Domain;

namespace DTOs
{
    public class Complaint : Entity<DTOs.Complaint, Domain.Complaint>
    {
        public virtual string Description { get; set; }

        public virtual Decode Type { get; set; }

        public virtual User OffendingUser { get; set; }

        public virtual User OffendedUser { get; set; }

        public override Complaint Initialize(Domain.Complaint domain)
        {
            throw new NotImplementedException();
        }
    }
}
