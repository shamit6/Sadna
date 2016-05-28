namespace Domain
{
    public class Employee : Entity
    {
        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual int Salary { get; set; }

        public virtual string Email { get; set; }
    }
}
