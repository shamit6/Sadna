using System;
using Domain;

namespace DTOs
{
    public class Field : Entity<DTOs.Field, Domain.Field>
    {
        public virtual int Name { get; set; }

        public virtual Decode Type { get; set; }

        public virtual Decode Size { get; set; }

        public override Field Initialize(Domain.Field domain)
        {
            throw new NotImplementedException();
        }
    }
}
