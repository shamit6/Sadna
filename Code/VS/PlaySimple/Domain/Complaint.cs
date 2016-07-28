namespace Domain
{
    public class Complaint : Entity
    {
        public virtual string Description { get; set; }

        public virtual ComplaintTypeDecode Type { get; set; }

        public virtual Customer OffendingUser { get; set; }

        public virtual Customer OffendedUser { get; set; }
    }
}
