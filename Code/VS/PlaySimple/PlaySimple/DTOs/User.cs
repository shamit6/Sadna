﻿using System;
using System.Collections.Generic;
using Domain;
using System.ComponentModel.DataAnnotations;
using PlaySimple.Validators;

namespace PlaySimple.DTOs
{
    public class User : Entity<DTOs.User, Domain.Customer>
    {
        [MaxLength(20)]
        public virtual string Username { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        [MinLength(8)]
        public virtual string Password { get; set; }

        [MaxLength(20)]
        public virtual string FirstName { get; set; }

        [MaxLength(20)]
        public virtual string LastName { get; set; }

        [NotInFuture]
        public virtual DateTime BirthDate { get; set; }
        
        [EmailAddress]
        public virtual string Email { get; set; }

        [IsEnumOfType(typeof(RegionDecode))]
        public virtual int Region { get; set; }

        public override User Initialize(Domain.Customer domain)
        {
            throw new NotImplementedException();
        }
    }
}
