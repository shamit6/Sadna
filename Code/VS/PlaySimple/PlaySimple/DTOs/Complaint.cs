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

        //[IsEnumOfType(typeof(ComplaintTypeDecode))]
        public virtual int? Type { get; set; }

        public virtual DateTime Date { get; set; }

        //[ExistsInDB(typeof(Domain.Customer))]
        public virtual Customer OffendingCustomer { get; set; }

        //[ExistsInDB(typeof(Domain.Customer))]
        public virtual Customer OffendedCustomer { get; set; }

        public override Complaint Initialize(Domain.Complaint domain)
        {
            Complaint newComplaint = new Complaint();

            newComplaint.Id = domain.Id;
            newComplaint.Description = domain.Description;
            newComplaint.Type = domain.Type.Id;
            newComplaint.Date = domain.Date;
            newComplaint.OffendingCustomer = new DTOs.Customer().Initialize(domain.OffendingCustomer);
            newComplaint.OffendedCustomer = new DTOs.Customer().Initialize(domain.OffendedCustomer);

            return newComplaint;
        }
    }
}
