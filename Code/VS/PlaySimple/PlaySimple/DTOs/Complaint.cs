using System;
using Domain;
using System.ComponentModel.DataAnnotations;
using PlaySimple.Validators;

namespace PlaySimple.DTOs
{
    public class Complaint : Entity<DTOs.Complaint, Domain.Complaint>
    {
        [MaxLength(1000)]
        public virtual string Description { get; set; }

        [IsEnumOfType(typeof(ComplaintTypeDecode))]
        public virtual int Type { get; set; }
	
	    [ExistsInDB(typeof(Domain.Customer))]
        public virtual Customer OffendingCustomer { get; set; }

        [ExistsInDB(typeof(Domain.Customer))]
        public virtual Customer OffendedCustomer { get; set; }

        public override Complaint Initialize(Domain.Complaint domain)
        {
            throw new NotImplementedException();
        }
    }
}
