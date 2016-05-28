using System;
using Domain;

namespace DTOs
{
    public class Employee : Entity<DTOs.Employee, Domain.Employee>
    {
        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual int Salary { get; set; }

        public virtual string Email { get; set; }

        public override Employee Initialize(Domain.Employee domain)
        {
            throw new NotImplementedException();
        }
    }
}
