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

        //[IsEnumOfType(typeof(Consts.Decodes.FieldType))]
        public virtual int Type { get; set; }

        //[IsEnumOfType(typeof(Consts.Decodes.FieldSize))]
        public virtual int Size { get; set; }

        public override Field Initialize(Domain.Field domain)
        {
            Field newField = new Field();

            newField.Id = domain.Id;
            newField.Name = domain.Name;
            newField.Size = domain.Size.Id;
            newField.Type = domain.Type.Id;

            return newField;
        }
    }
}
