﻿using System;
using Domain;
using System.ComponentModel.DataAnnotations;
using PlaySimple.Validators;

namespace PlaySimple.DTOs
{
    public class Review : Entity<DTOs.Review, Domain.Review>
    {
        [MaxLength(20)]
        public virtual string Title { get; set; }

        [MaxLength(1000)]
        public virtual string Description { get; set; }

        public virtual DateTime Date { get; set; }

        [ExistsInDB(typeof(Domain.Customer))]
        public virtual User Reviewer { get; set; }

        [ExistsInDB(typeof(Domain.Customer))]
        public virtual User ReviewedCustomer { get; set; }

        public override Review Initialize(Domain.Review domain)
        {
            throw new NotImplementedException();
        }
    }
}
