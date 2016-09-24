using System;
using Domain;
using System.ComponentModel.DataAnnotations;
using PlaySimple.Validators;

namespace PlaySimple.DTOs
{
    public class Employee : Entity<DTOs.Employee, Domain.Employee>
    {
        [MaxLength(20)]
        public virtual string Username { get; set; }

        // TODO enable
        //[RegularExpression(@"^[a-zA-Z0-9]+$")]
        //[MinLength(8)]
        public virtual string Password { get; set; }

        [MaxLength(20)]
        public virtual string FirstName { get; set; }

        [MaxLength(20)]
        public virtual string LastName { get; set; }

        [Above(0)]
        public virtual int Salary { get; set; }

        [EmailAddress]
        public virtual string Email { get; set; }

        public override Employee Initialize(Domain.Employee domain)
        {
            Employee newEmployee = new Employee();

            newEmployee.Id = domain.Id;
            newEmployee.Username = domain.Username;
            newEmployee.Password = domain.Password;
            newEmployee.FirstName = domain.FirstName;
            newEmployee.LastName = domain.LastName;
            newEmployee.Salary = domain.Salary;
            newEmployee.Email = domain.Email;

            return newEmployee;
        }
    }
}
