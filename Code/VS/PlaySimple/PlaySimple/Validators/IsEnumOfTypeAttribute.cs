using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlaySimple.Validators
{
    public class IsEnumOfTypeAttribute : ValidationAttribute
    {
        private Type _type;

        public IsEnumOfTypeAttribute(Type type)
        {
            _type = type;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!_type.IsEnum)
                throw new ValidationException("type must be an enum");

            if (!Enum.GetValues(_type).Cast<int>().Any(x => x == (int)value))
            {
                return new ValidationResult("Invalid enum");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}