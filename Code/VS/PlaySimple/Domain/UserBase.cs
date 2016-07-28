namespace Domain
{
    public abstract class UserBase : Entity
    {
        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }
    }
}
