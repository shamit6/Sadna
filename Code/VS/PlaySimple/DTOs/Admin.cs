using System;
using Domain;

namespace DTOs
{
    public class Admin : Entity<DTOs.Admin, Domain.Admin>
    {
        public string Username { get; set; }

        public override DTOs.Admin Initialize(Domain.Admin domain)
        {
            throw new NotImplementedException();
        }
    }
}
