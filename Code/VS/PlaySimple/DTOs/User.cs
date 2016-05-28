using System;
using System.Collections.Generic;
using Domain;

namespace DTOs
{
    public class User : Entity<DTOs.User, Domain.User>
    {
        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual DateTime BirthDate { get; set; }

        public virtual string Email { get; set; }

        public virtual DateTime FreezeDate { get; set; }

        public virtual Decode Region { get; set; }

        public override User Initialize(Domain.User domain)
        {
            throw new NotImplementedException();
        }
    }
}
