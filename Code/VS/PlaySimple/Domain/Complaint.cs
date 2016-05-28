namespace Domain
{
    public class Complaint : Entity
    {
        public virtual string Description { get; set; }

        public virtual ComplaintTypeDecode Type { get; set; }

        public virtual User OffendingUser { get; set; }

        public virtual User OffendedUser { get; set; }
    }
}
