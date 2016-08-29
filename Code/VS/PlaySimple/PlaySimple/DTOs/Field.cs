using System;
using Domain;
using System.ComponentModel.DataAnnotations;
using PlaySimple.Validators;

namespace PlaySimple.DTOs
{
    public class Field : Entity<DTOs.Field, Domain.Field>
    {
        [MaxLength(20)]
        public virtual string Name { get; set; }

        [IsEnumOfType(typeof(FieldTypeDecode))]
        public virtual string Type { get; set; }

        [IsEnumOfType(typeof(FieldSizeDecode))]
        public virtual string Size { get; set; }

        public override Field Initialize(Domain.Field domain)
        {
            Id = domain.Id;
            Name = domain.Name;
            Size = domain.Size.Name;
            Type = domain.Type.Name;

            return this;
        }
    }
}
